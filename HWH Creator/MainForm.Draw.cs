using SharedWinforms.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void UpdateImage(int index, bool highQuality = false)
        {
            if (index < 0 || index >= ContentsTree.Nodes.Count)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(Options.PageSize.Width, Options.PageSize.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            if (highQuality)
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            }
            else
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            }

            TreeNode topNode = ContentsTree.Nodes[index];
            TagControls.PageTag pageTag = topNode?.Tag as TagControls.PageTag;

            DrawExtension.DrawContext context = new DrawExtension.DrawContext();
            {
                context.redPenList = (pageTag?.RedPenList ?? new List<string>()).ToArray();
                context.redPenNumberDigit = (int)Math.Log10(context.redPenList.Length) + 1;

                context.redBrush = RedBrush;

                context.redPenNumberZeroLeft = Options.RedPenNumberZeroLeft;
                context.redPenTextVisible = Options.RedPenTextVisible;

                context.blackPen = BlackPen;
                context.lineInterval = Options.LineInterval;

                context.saveCharList = Options.SaveCharList;

                context.redPenFont = Options.RedPenFont;
            }

            SizeF characterSize = graphics.MeasureString("0", Options.TextFont);
            float characterWidth = characterSize.Width;

            float leftWidth1 = graphics.MeasureString("～0000", Options.TextFont).Width;
            float leftWidth2 = graphics.MeasureString("00/00", Options.TextFont).Width;
            if (pageTag != null)
            {
                float textHorizontalMargin = characterSize.Width * 2 - graphics.MeasureString("00", Options.TextFont).Width;
                characterWidth -= textHorizontalMargin;
                if (pageTag.HasBC)
                {
                    leftWidth1 += graphics.MeasureString("-", Options.TextFont).Width - textHorizontalMargin;
                }
                if (pageTag.HasAbout)
                {
                    leftWidth1 += graphics.MeasureString("△", Options.TextFont).Width - textHorizontalMargin;
                }
            }

            StringFormat rightAlignment = new StringFormat() { Alignment = StringAlignment.Far, };

            PointF currentPosition = new PointF(Options.PagePadding, Options.PagePadding);

            List<(RectangleF, TreeNode)> floatObjectRects = new List<(RectangleF, TreeNode)>();

            void drawDate(string date, bool red, PointF point)
            {
                if (date.Trim().Length > 1)
                {
                    string month = date.Split('/').ElementAtOrDefault(0).Trim();
                    string dayOfMonth = date.Split('/').ElementAtOrDefault(1).Trim();
                    graphics.DrawString(month, Options.TextFont, red ? RedBrush : BlackBrush, point.X + (2 - month.Length) * characterWidth, point.Y);
                    graphics.DrawString("/", Options.TextFont, red ? RedBrush : BlackBrush, point.X + 2 * characterWidth, point.Y);
                    graphics.DrawString(dayOfMonth, Options.TextFont, red ? RedBrush : BlackBrush, point.X + (5 - dayOfMonth.Length) * characterWidth, point.Y);
                }
            }

            Image drawTextBox(string text, StringAlignment alignment)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                PointF pointF = new PointF(Options.PageSize.Width, Options.PageSize.Height);
                context.font = Options.TextBoxFont;
                context.width = Options.PageSize.Width - Options.PagePadding * 2;
                context.lineInterval = 0f;

                string[] texts = text.Split(new string[] { "\r\n", }, StringSplitOptions.None);
                float[] widthes = new float[texts.Length];
                float height = 0;
                float textHeight = graphics.MeasureString("0", context.font).Height;

                for (int i = 0; i < texts.Length; i++)
                {
                    SizeF sizeF = graphics.DrawScript(string.IsNullOrWhiteSpace(texts[i]) ? "\r\n" : texts[i], ref pointF, ref context);
                    widthes[i] = sizeF.Width;
                    height += sizeF.Height == 0 ? textHeight : sizeF.Height;
                }

                SizeF size = new SizeF(widthes.Max(), height);

                pointF = new PointF();
                Image image = new Bitmap((int)size.Width, (int)size.Height);
                if (image == null)
                {
                    return null;
                }

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.SmoothingMode = graphics.SmoothingMode;
                    g.TextRenderingHint = graphics.TextRenderingHint;

                    switch (alignment)
                    {
                        case StringAlignment.Near:
                            g.DrawScript(text, ref pointF, ref context);
                            break;
                        case StringAlignment.Center:
                            for (int i = 0; i < texts.Length; i++)
                            {
                                pointF.X = (size.Width - widthes[i]) / 2.0f;
                                pointF.Y += g.DrawScript(texts[i], ref pointF, ref context).Height;
                            }
                            break;
                        case StringAlignment.Far:
                            for (int i = 0; i < texts.Length; i++)
                            {
                                pointF.X = size.Width - widthes[i];
                                pointF.Y += g.DrawScript(texts[i], ref pointF, ref context).Height;
                            }
                            break;
                    }
                }

                context.lineInterval = Options.LineInterval;
                return image;
            }

            void addFloatObjectRect(RectangleF rectangle, TreeNode node)
            {
                int rectIndex = floatObjectRects.FindIndex(x => x.Item1.Contains(rectangle));
                if (rectIndex == -1)
                {
                    floatObjectRects.Add((rectangle, node));
                }
                else
                {
                    floatObjectRects.Insert(rectIndex = floatObjectRects.FindIndex(x => x.Item1.Contains(rectangle)), (rectangle, node));
                }
            }

            void drawContents(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                PointF lastCurrentPosition = currentPosition;
                SizeF size = new SizeF();

                TreeNode parent = node.Parent ?? topNode;
                BaseTag tag = node.Tag as BaseTag;

                float addWidth = 0;
                bool parentExplainList = (parent.Tag as BaseTag)?.Type == TagType.ExplainList;

                switch (tag?.Type)
                {
                    case TagType.RedPen:
                        break;
                    case TagType.ExplainList:
                        context.font = Options.TextFont;
                        context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                        size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                        currentPosition.X += characterSize.Width;

                        bool underLine = false;
                        bool hasChildren = false;
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            TagType? childType = (childNode.Tag as BaseTag)?.Type;
                            if (childType == TagType.Element)
                            {
                                drawContents(childNode);
                                hasChildren = true;
                            }
                            else if (childType != TagType.Picture && childType != TagType.TextBox)
                            {
                                underLine = true;
                            }
                        }

                        currentPosition.X = lastCurrentPosition.X;

                        if (underLine)
                        {
                            float y = lastCurrentPosition.Y + characterSize.Height;
                            graphics.DrawLine(BlackPen, new PointF(currentPosition.X, y), new PointF(currentPosition.X + size.Width, y));
                        }

                        if (hasChildren)
                        {
                            PointF startPoint1 = new PointF(currentPosition.X + size.Width, lastCurrentPosition.Y + characterSize.Height / 2.0f);
                            PointF startPoint2 = new PointF(startPoint1.X + characterSize.Width, startPoint1.Y);
                            float y = currentPosition.Y - characterSize.Height * Options.LineInterval;
                            graphics.DrawBezier(BlackPen, startPoint1, startPoint2, new PointF(startPoint1.X, lastCurrentPosition.Y), new PointF(startPoint2.X, lastCurrentPosition.Y));
                            graphics.DrawBezier(BlackPen, startPoint1, startPoint2, new PointF(startPoint1.X, y), new PointF(startPoint2.X, y));

                            size.Width += characterSize.Width;
                            if (size.Height == 0)
                            {
                                size.Height = y - lastCurrentPosition.Y;
                            }
                        }
                        else
                        {
                            currentPosition.Y += characterSize.Height;
                        }

                        currentPosition.X += characterSize.Width;
                        break;
                    case TagType.Element:
                        context.font = Options.TextFont;
                        context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                        if (parentExplainList)
                        {
                            size = graphics.DrawScript($"・{tag.Text}", ref currentPosition, ref context);
                            currentPosition.X = lastCurrentPosition.X + characterSize.Width;
                            currentPosition.Y = lastCurrentPosition.Y + size.Height;
                        }
                        else
                        {
                            size = graphics.DrawScript($"：{tag.Text}", ref currentPosition, ref context);
                        }
                        size.Height = size.Height == 0 ? characterSize.Height : size.Height;
                        break;
                    case TagType.Supplement:
                        {
                            float y = currentPosition.Y + characterSize.Height / 2.0f;

                            if (parentExplainList)
                            {
                                currentPosition.X += addWidth = characterSize.Width;
                                graphics.DrawLine(BlackPen, new PointF(lastCurrentPosition.X - characterSize.Width / 2.0f, y), new PointF(currentPosition.X, y));
                            }
                            else
                            {
                                currentPosition.X += addWidth = characterSize.Width * 2;
                                graphics.DrawLine(BlackPen, new PointF(lastCurrentPosition.X, y), new PointF(currentPosition.X, y));
                            }

                            context.font = Options.TextFont;
                            context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                            size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                            size.Height = size.Height == 0 ? characterSize.Height : size.Height;
                            currentPosition.X = lastCurrentPosition.X + characterSize.Width;
                            currentPosition.Y = lastCurrentPosition.Y + size.Height;
                        }
                        break;
                    case TagType.ChangeAndInfluence:
                        {
                            float y = currentPosition.Y + characterSize.Height / 2.0f;

                            if (parentExplainList)
                            {
                                currentPosition.X += addWidth = characterSize.Width;
                                graphics.DrawLine(BlackPen, new PointF(lastCurrentPosition.X - characterSize.Width / 2.0f, y), new PointF(currentPosition.X, y));
                            }
                            else
                            {
                                currentPosition.X += addWidth = characterSize.Width * 2;
                                graphics.DrawLine(BlackPen, new PointF(lastCurrentPosition.X, y), new PointF(currentPosition.X, y));
                            }

                            float x = currentPosition.X - characterSize.Width / 4.0f;
                            float quarterLineHeight = characterSize.Height / 5.0f;
                            graphics.DrawLine(BlackPen, new PointF(x, y - quarterLineHeight), new PointF(currentPosition.X, y));
                            graphics.DrawLine(BlackPen, new PointF(x, y + quarterLineHeight), new PointF(currentPosition.X, y));

                            context.font = Options.TextFont;
                            context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                            size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                            size.Height = size.Height == 0 ? characterSize.Height : size.Height;
                            currentPosition.X = lastCurrentPosition.X + characterSize.Width;
                            currentPosition.Y = lastCurrentPosition.Y + size.Height;

                            if (parentExplainList)
                            {
                                currentPosition.X = lastCurrentPosition.X + characterSize.Width * 2;
                            }
                            else
                            {
                                currentPosition.X = lastCurrentPosition.X + characterSize.Width;
                            }
                        }
                        break;
                    case TagType.Period:
                        if (tag is TagControls.PeriodTag periodTag)
                        {
                            string text = $"{(pageTag.HasBC && periodTag.IsBC1 ? "-" : string.Empty)}{periodTag.Year1}{(periodTag.IsCentury1 ? "C" : string.Empty)}{(pageTag.HasAbout && periodTag.IsAbout1 ? "△" : string.Empty)}";
                            graphics.DrawString(text, Options.TextFont, periodTag.IsYearRed1 ? RedBrush : BlackBrush, new RectangleF(currentPosition, new SizeF(leftWidth1, characterSize.Height)));
                            text = $"～{(pageTag.HasBC && periodTag.IsBC2 ? "-" : string.Empty)}{periodTag.Year2}{(periodTag.IsCentury2 ? "C" : string.Empty)}{(pageTag.HasAbout && periodTag.IsAbout2 ? "△" : string.Empty)}";
                            graphics.DrawString(text, Options.TextFont, periodTag.IsYearRed2 ? RedBrush : BlackBrush, new RectangleF(new PointF(currentPosition.X, currentPosition.Y + characterSize.Height), new SizeF(leftWidth1, characterSize.Height)), rightAlignment);
                            currentPosition.X += leftWidth1;
                            if (pageTag.HasDate)
                            {
                                addWidth = leftWidth1 + leftWidth2;
                                if (!periodTag.IsCentury1)
                                {
                                    drawDate(periodTag.Date1, periodTag.IsYearRed1, currentPosition);
                                }
                                if (!periodTag.IsCentury2)
                                {
                                    drawDate(periodTag.Date2, periodTag.IsYearRed2, new PointF(currentPosition.X, currentPosition.Y + characterSize.Height));
                                }
                                currentPosition.X += leftWidth2;
                            }
                            else
                            {
                                addWidth = leftWidth1;
                            }

                            context.font = Options.TextFont;
                            context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                            size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                            size.Height = size.Height == 0 ? characterSize.Height : size.Height;
                            currentPosition.X = lastCurrentPosition.X + characterSize.Width + addWidth;
                            currentPosition.Y = lastCurrentPosition.Y + size.Height;
                        }
                        break;
                    case TagType.Event:
                        if (tag is TagControls.EventTag eventTag)
                        {
                            string text = $"{(pageTag.HasBC && eventTag.IsBC ? "-" : string.Empty)}{eventTag.Year}{(eventTag.IsCentury ? "C" : string.Empty)}{(pageTag.HasAbout && eventTag.IsAbout ? "△" : string.Empty)}";
                            graphics.DrawString(text, Options.TextFont, eventTag.IsYearRed ? RedBrush : BlackBrush, new RectangleF(currentPosition, new SizeF(leftWidth1, characterSize.Height)), rightAlignment);
                            currentPosition.X += leftWidth1;
                            if (pageTag.HasDate)
                            {
                                addWidth = leftWidth1 + leftWidth2;
                                if (!eventTag.IsCentury)
                                {
                                    drawDate(eventTag.Date, eventTag.IsYearRed, currentPosition);
                                }
                                currentPosition.X += leftWidth2;
                            }
                            else
                            {
                                addWidth = leftWidth1;
                            }

                            context.font = Options.TextFont;
                            context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                            size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                            currentPosition.X = lastCurrentPosition.X + characterSize.Width + addWidth;
                            currentPosition.Y = lastCurrentPosition.Y + size.Height;
                        }
                        break;
                    case TagType.Block:
                        break;
                    case TagType.Headline:
                        context.font = Options.HeadlineFont;
                        context.width = Options.PageSize.Width - currentPosition.X - Options.PagePadding;
                        context.lineInterval = 0f;
                        size = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                        currentPosition.X = lastCurrentPosition.X;
                        currentPosition.Y = lastCurrentPosition.Y + size.Height;
                        context.lineInterval = Options.LineInterval;
                        graphics.DrawLine(BlackPen, currentPosition, new PointF(currentPosition.X + size.Width, currentPosition.Y));
                        break;
                    case TagType.Page:
                        {
                            string pageNumber = $"No.{(index + 1):00}\r\nHWH記法 ver.{Properties.Resources.FormatVersion}";
                            SizeF pageNumberSize = graphics.MeasureString(pageNumber, Options.PageNumberFont);

                            context.font = Options.PageFont;
                            context.width = Options.PageSize.Width - Options.PagePadding * 2 - pageNumberSize.Width;
                            context.lineInterval = 0f;
                            SizeF headerSize = graphics.DrawScript(tag.Text, ref currentPosition, ref context);
                            currentPosition.X = lastCurrentPosition.X;
                            currentPosition.Y = lastCurrentPosition.Y + Math.Max(headerSize.Height, pageNumberSize.Height);
                            context.lineInterval = Options.LineInterval;

                            graphics.DrawString(pageNumber, Options.PageNumberFont, BlackBrush, new PointF(Options.PageSize.Width - Options.PagePadding - pageNumberSize.Width, currentPosition.Y - pageNumberSize.Height));
                            graphics.DrawLine(BlackPen, new PointF(Options.PagePadding, currentPosition.Y), new PointF(Options.PageSize.Width - Options.PagePadding, currentPosition.Y));

                            lastCurrentPosition = new PointF(Options.PagePadding, Options.PagePadding);
                            size = new SizeF(Options.PageSize.Width - Options.PagePadding * 2, currentPosition.Y - Options.PagePadding);
                        }
                        break;
                    case TagType.Picture:
                        if (tag is TagControls.PictureTag pictureTag && File.Exists(pictureTag.Path))
                        {
                            try
                            {
                                Image image = Image.FromFile(pictureTag.Path);
                                if (image != null)
                                {
                                    RectangleF rectangle = pictureTag.Rectangle;
                                    graphics.DrawImage(image, rectangle);
                                    addFloatObjectRect(rectangle, node);
                                    image.Dispose();

                                    using (Image label = drawTextBox(tag.Text, pictureTag.LabelAlignment))
                                    {
                                        if (label != null)
                                        {
                                            PointF pointF = new PointF(rectangle.X, rectangle.Y + rectangle.Height);
                                            switch (pictureTag.LabelAlignment)
                                            {
                                                case StringAlignment.Near:
                                                    break;
                                                case StringAlignment.Center:
                                                    pointF.X += (rectangle.Width - label.Width) / 2.0f;
                                                    break;
                                                case StringAlignment.Far:
                                                    pointF.X += rectangle.Width - label.Width;
                                                    break;
                                            }
                                            graphics.DrawImage(label, pointF);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                ExportException(e);
                            }
                        }
                        return;
                    case TagType.TextBox:
                        if (tag is TagControls.TextBoxTag textBoxTag)
                        {
                            Image image = drawTextBox(tag.Text, textBoxTag.LabelAlignment);
                            if (image != null)
                            {
                                addFloatObjectRect(new RectangleF(textBoxTag.Rectangle.Location, image.Size), node);
                                graphics.DrawImage(image, textBoxTag.Rectangle.Location);
                                textBoxTag.Image = image;
                            }
                        }
                        return;
                }

                foreach (TreeNode childNode in node.Nodes)
                {
                    if (tag?.Type != TagType.ExplainList || (childNode.Tag as BaseTag)?.Type != TagType.Element)
                    {
                        drawContents(childNode);
                    }
                }

                switch (tag?.Type)
                {
                    case TagType.RedPen:
                        break;
                    case TagType.ExplainList:
                        if (node.Nodes.Count > 0)
                        {
                            for (int i = node.LastNode.Index; i >= 0; i--)
                            {
                                BaseTag brotherTag = node.Nodes[i].Tag as BaseTag;
                                if (brotherTag?.Type != TagType.Element && brotherTag?.Type != TagType.Picture && brotherTag?.Type != TagType.TextBox)
                                {
                                    float x = lastCurrentPosition.X + characterSize.Width / 2.0f;
                                    graphics.DrawLine(BlackPen, new PointF(x, lastCurrentPosition.Y + characterSize.Height), new PointF(x, brotherTag.Rectangle.Y + characterSize.Height / 2.0f));
                                    break;
                                }
                            }
                        }
                        break;
                    case TagType.Element:
                        if ((parent.Tag as BaseTag)?.Type != TagType.ExplainList && currentPosition.Y == lastCurrentPosition.Y)
                        {
                            currentPosition.Y += characterSize.Height;
                        }
                        break;
                    case TagType.Supplement:
                        break;
                    case TagType.ChangeAndInfluence:
                        break;
                    case TagType.Period:
                        if (currentPosition.Y - lastCurrentPosition.Y < characterSize.Height * 2)
                        {
                            currentPosition.Y = lastCurrentPosition.Y + characterSize.Height * 2;
                        }
                        break;
                    case TagType.Event:
                        break;
                    case TagType.Block:
                        if (tag is TagControls.BlockTag blockTag)
                        {
                            if (blockTag.IsLine)
                            {
                                currentPosition.Y += characterSize.Height * blockTag.Interval;
                            }
                            graphics.DrawLine(BlackPen, new PointF(Options.PagePadding + leftWidth1, lastCurrentPosition.Y), new PointF(Options.PagePadding + leftWidth1, currentPosition.Y));
                            if (pageTag.HasDate)
                            {
                                graphics.DrawLine(BlackPen, new PointF(Options.PagePadding + leftWidth1 + leftWidth2, lastCurrentPosition.Y), new PointF(Options.PagePadding + leftWidth1 + leftWidth2, currentPosition.Y));
                            }
                            if (!blockTag.IsLine)
                            {
                                currentPosition.Y += characterSize.Height * blockTag.Interval;
                            }
                        }
                        break;
                    case TagType.Headline:
                        break;
                    case TagType.Page:
                        break;
                    case TagType.Picture:
                        break;
                    default:
                        break;
                }

                if (tag != null)
                {
                    size.Width += addWidth;
                    tag.Rectangle = new RectangleF(lastCurrentPosition, size);
                }

                currentPosition.X = lastCurrentPosition.X;
            }

            drawContents(topNode);

            graphics.Dispose();
            if (MainPanel.Controls[index] is PictureBox pictureBox)
            {
                pictureBox.Image = bitmap;
                pictureBox.Tag = floatObjectRects;
            }
            GC.Collect();
        }
    }

    public static class DrawExtension
    {
        public struct DrawContext
        {
            internal Font font;
            internal float width;
            internal string[] redPenList;
            internal int redPenNumberDigit;

            internal Brush redBrush;

            internal bool redPenNumberZeroLeft;
            internal bool redPenTextVisible;

            internal Pen blackPen;
            internal float lineInterval;

            internal char[] saveCharList;

            internal Font redPenFont;
        }

        public static Image GetRedPenImage(this Graphics graphics, int keyNumber, bool textOnly, ref DrawContext context)
        {
            if (context.redPenList != null)
            {
                int keyIndex = Math.Abs(keyNumber) - 1;

                if (keyIndex >= 0 && keyIndex < context.redPenList.Length)
                {
                    string text = context.redPenList[keyIndex];
                    Font font = new Font(context.redPenFont.FontFamily, context.font.Size, context.redPenFont.Style, context.redPenFont.Unit);
                    SizeF redPenTextSize = graphics.MeasureString(text, font);
                    redPenTextSize.Height = graphics.MeasureString("0", font).Height;

                    if (textOnly)
                    {
                        Bitmap redPenImage = new Bitmap((int)redPenTextSize.Width, (int)redPenTextSize.Height);

                        using (Graphics g = Graphics.FromImage(redPenImage))
                        {
                            g.SmoothingMode = graphics.SmoothingMode;
                            g.TextRenderingHint = graphics.TextRenderingHint;
                            g.DrawString(text, font, context.redBrush, 0, 0);
                        }

                        return redPenImage;
                    }
                    else
                    {
                        StringFormat centerAlignment = new StringFormat() { Alignment = StringAlignment.Center, };

                        bool collapse = keyNumber < 0;
                        string number = (keyIndex + 1).ToString();
                        if (context.redPenNumberZeroLeft)
                        {
                            for (int i = context.redPenNumberDigit - number.Length; i > 0; i--)
                            {
                                number = "0" + number;
                            }
                        }

                        SizeF redPenNumberSize = graphics.MeasureString(number, font);
                        float redPenImageWidth = redPenNumberSize.Width + redPenTextSize.Width;

                        Bitmap redPenImage = collapse ? new Bitmap((int)redPenNumberSize.Width, (int)redPenNumberSize.Height) : new Bitmap((int)redPenImageWidth, (int)redPenNumberSize.Height);

                        using (Graphics g = Graphics.FromImage(redPenImage))
                        {
                            g.SmoothingMode = graphics.SmoothingMode;
                            g.TextRenderingHint = graphics.TextRenderingHint;

                            if (collapse)
                            {

                            }
                            else
                            {
                                if (context.redPenTextVisible)
                                {
                                    g.DrawString(text, font, context.redBrush, new RectangleF(new PointF(redPenNumberSize.Width, 0), redPenTextSize), centerAlignment);
                                }

                                g.DrawLine(context.blackPen, new PointF(context.blackPen.Width, redPenNumberSize.Height - context.blackPen.Width), new PointF(redPenImageWidth - context.blackPen.Width, redPenNumberSize.Height - context.blackPen.Width));
                            }

                            g.DrawString(number, font, Brushes.Black, new RectangleF(new PointF(), redPenNumberSize), centerAlignment);
                            g.DrawRectangle(context.blackPen, new PointF(context.blackPen.Width, context.blackPen.Width), new PointF(redPenNumberSize.Width - context.blackPen.Width, redPenNumberSize.Height - context.blackPen.Width));
                        }

                        return redPenImage;
                    }
                }
            }

            return null;
        }

        public static void DrawText(this Graphics graphics, string text, ref PointF point, float startX, ref DrawContext context)
        {
            float textHeight = graphics.MeasureString("0", context.font).Height * (context.lineInterval + 1f);

            float limitWidth = context.width + startX - point.X;
            int breakLength = text.Length;

            while (breakLength > 0)
            {
                float strWidth = graphics.MeasureString(text.Substring(0, breakLength), context.font).Width;
                if (strWidth <= limitWidth)
                {
                    while (breakLength < text.Length && context.saveCharList.Contains(text[breakLength]))
                    {
                        breakLength++;
                    }

                    graphics.DrawString(text.Substring(0, breakLength), context.font, Brushes.Black, point);

                    if (breakLength == text.Length)
                    {
                        point.X += strWidth;
                        return;
                    }

                    point.X = startX;
                    point.Y += textHeight;

                    text = text.Substring(breakLength);

                    limitWidth = context.width;
                    breakLength = text.Length;

                    continue;
                }

                breakLength--;
            }
        }

        public static SizeF DrawScript(this Graphics graphics, string script, ref PointF point, ref DrawContext context)
        {
            if (string.IsNullOrWhiteSpace(script))
            {
                return new SizeF();
            }

            PointF startPoint = point;
            float textHeight = graphics.MeasureString("0", context.font).Height * (context.lineInterval + 1f);

            Regex regex = new Regex(@"<(-?\d+?-?)>|(\r\n)", RegexOptions.Compiled);
            Match match = regex.Match(script);

            SizeF resultSize = new SizeF(graphics.MeasureString(regex.Replace(script, string.Empty), context.font).Width, textHeight);

            int offset = 0;
            while (match.Success)
            {
                graphics.DrawText(script.Substring(offset, match.Index - offset), ref point, startPoint.X, ref context);

                if (match.Value.Equals("\r\n"))
                {
                    point.X = startPoint.X;
                    point.Y += resultSize.Height;
                }
                else
                {
                    string token = match.Result("$1");
                    Image redPenImage = graphics.GetRedPenImage(int.Parse(token.TrimEnd('-')), token.EndsWith("-"), ref context);
                    if (redPenImage != null)
                    {
                        if (redPenImage.Width > context.width)
                        {
                            Bitmap bitmap = new Bitmap((int)context.width, redPenImage.Height);
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.DrawImage(redPenImage, 0, 0, context.width, redPenImage.Height);
                                redPenImage.Dispose();
                                redPenImage = bitmap;
                            }
                        }

                        if (redPenImage.Width > (context.width + startPoint.X - point.X))
                        {
                            point.X = startPoint.X;
                            point.Y += redPenImage.Height;
                        }

                        resultSize.Height = Math.Max(resultSize.Height, redPenImage.Height);

                        graphics.DrawImage(redPenImage, point);
                        point.X += redPenImage.Width;
                        resultSize.Width += redPenImage.Width;
                        redPenImage.Dispose();
                    }
                }

                offset = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (offset < script.Length)
            {
                graphics.DrawText(script.Substring(offset), ref point, startPoint.X, ref context);
            }

            return new SizeF(Math.Min(resultSize.Width, context.width), point.Y - startPoint.Y + resultSize.Height);
        }
    }
}
