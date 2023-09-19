using SharedWinforms.Extension;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void AddTabPagePanel_ClientSizeChanged(object sender, EventArgs e)
        {
            RedPenButton.Width = AddTabPagePanel.ClientSize.Width - 6;
            AddTabPagePanel.PerformLayout();
        }

        private void RedPenButton_Click(object sender, EventArgs e)
        {
            if (SelectedPageTag is TagControls.PageTag pageTag)
            {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    InsertRedPenOf(pageTag.RedPenList.Count - 1);
                }
                else
                {
                    TagControls.RedPenTag redPenTag = new TagControls.RedPenTag();
                    EditDialog.Tag = redPenTag;
                    if (EditDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        AddRedPenToList(redPenTag.Text);
                    }
                }
            }
            else
            {
                if (ContentsTree.Nodes.Count == 0)
                {
                    MessageBox.Show(this, "まずはページを追加してください。", "赤ペン", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "まずはページを選択してください。", "赤ペン", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExplainListButton_Click(object sender, EventArgs e)
        {
            AddNode(3, false, new TagControls.ExplainListTag());
        }

        private void ElementButton_Click(object sender, EventArgs e)
        {
            AddNode(3, false, new TagControls.ElementTag());
        }

        private void SupplementButton_Click(object sender, EventArgs e)
        {
            AddNode(3, false, new TagControls.SupplementTag());
        }

        private void ChangeAndInfluenceButton_Click(object sender, EventArgs e)
        {
            AddNode(3, false, new TagControls.ChangeAndInfluenceTag());
        }

        private void PeriodButton_Click(object sender, EventArgs e)
        {
            AddNode(2, true, new TagControls.PeriodTag());
        }

        private void EventButton_Click(object sender, EventArgs e)
        {
            AddNode(2, true, new TagControls.EventTag());
        }

        private void BlockButton_Click(object sender, EventArgs e)
        {
            AddNode(1, true, new TagControls.BlockTag());
        }

        private void HeadlineButton_Click(object sender, EventArgs e)
        {
            AddNode(0, true, new TagControls.HeadlineTag());
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode($"{ContentsTree.Nodes.Count + 1:00}");
            ContentsTree.Nodes.Add(node);
            TagControls.PageTag pageTag = new TagControls.PageTag();
            node.Tag = pageTag;
            node.ImageIndex = (int)pageTag.Type;
            node.SelectedImageIndex = (int)pageTag.Type; ;

            TreeNode tempNode = ContentsTree.SelectedNode;
            PictureBox page = AddPageBox();

            ContentsTree.SelectedNode = node;
            if (EditSelectedNode(true))
            {
                UpdateStatus("「ページ」を追加しました。");
            }
            else
            {
                if (tempNode == null)
                {
                    TreeNodeLabel.ResetText();
                    ContentsText.ResetText();
                }
                else
                {
                    ContentsTree.SelectedNode = tempNode;
                }

                node.Remove();
                MainPanel.Controls.Remove(page);

                if (!Edited && MainPanel.Controls.Count == 0)
                {
                    AddStartPage();
                }

                return;
            }
        }

        private void PictureButton_Click(object sender, EventArgs e)
        {
            AddNode(0, false, new TagControls.PictureTag()
            {
                Rectangle = new RectangleF(Options.PagePadding, Options.PagePadding, 0f, 0f),
            });
        }

        private void TextBoxButton_Click(object sender, EventArgs e)
        {
            AddNode(0, false, new TagControls.TextBoxTag()
            {
                Rectangle = new RectangleF(Options.PagePadding, Options.PagePadding, 0f, 0f),
            });
        }

        private void RedPenListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index;
            if (index == -1)
            {
                return;
            }

            ListBox listBox = (sender as ListBox);
            string item = listBox?.Items[index]?.ToString();
            if (!string.IsNullOrWhiteSpace(item))
            {
                Rectangle bounds = e.Bounds;
                string number = (index + 1).ToString();
                string itemCount = listBox.Items.Count.ToString();
                SizeF numberSize = e.Graphics.MeasureString(itemCount, e.Font);
                int numberWidth = (int)numberSize.Width;

                if (Options.RedPenNumberZeroLeft)
                {
                    number = new string('0', itemCount.Length - number.Length) + number;
                }

                Pen pen = SystemPens.ControlText;
                Brush brush1 = SystemBrushes.Window;
                Brush brush2 = new Pen(listBox.ForeColor).Brush;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                    {
                        pen = SystemPens.HighlightText;
                        brush1 = SystemBrushes.Highlight;
                        brush2 = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        brush1 = SystemBrushes.Control;
                    }
                }

                e.Graphics.FillRectangle(brush1, bounds);
                e.Graphics.DrawString(number, e.Font, pen.Brush, new RectangleF(bounds.Location, numberSize), new StringFormat() { Alignment = StringAlignment.Far });

                bounds.Location = new Point(bounds.X + numberWidth, bounds.Y);
                bounds.Size = new Size(bounds.Width - numberWidth, bounds.Height);

                e.Graphics.DrawLine(pen, bounds.Location, new Point(bounds.X, bounds.Bottom));
                e.Graphics.DrawString(item, e.Font, brush2, bounds);
            }
            e.DrawFocusRectangle();
        }

        private void RedPenListBox_DoubleClick(object sender, EventArgs e)
        {
            int index = RedPenListBox.SelectedIndex;
            if (index != -1)
            {
                TagControls.RedPenTag redPenTag = new TagControls.RedPenTag()
                {
                    Text = RedPenListBox.SelectedItem as string,
                };
                string redPenTagData = redPenTag.Data;
                EditDialog.Tag = redPenTag;
                if (EditDialog.ShowDialog(this) == DialogResult.OK && !redPenTagData.Equals(redPenTag.Data))
                {
                    string text = redPenTag.Text;
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        if (SelectedPageTag is TagControls.PageTag pageTag && index >= 0 && index < pageTag.RedPenList.Count)
                        {
                            pageTag.RedPenList[index] = text;

                            Edited = true;

                            UpdateImage(SelectedIndex);
                            UpdateRedPenList(index);
                            UpdateStatus("「赤ペン」を編集しました。");
                        }
                    }
                }
            }
        }

        private void RedPenListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (e.KeyChar)
                {
                    case ' ':
                        SearchText(RedPenListBox.SelectedItem as string);
                        e.Handled = true;
                        break;
                    case '\n':
                        RedPenListAddButton.PerformClick();
                        e.Handled = true;
                        break;
                }
            }
        }

        private void RedPenListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContentsTree.DoDragDrop(RedPenListBox.IndexFromPoint(e.Location), DragDropEffects.All);
            }
        }

        private void RedPenListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                int index1 = (e.Data.GetData(typeof(int)) as int?) ?? -1;
                int index2 = RedPenListBox.IndexFromPoint(RedPenListBox.PointToClient(new Point(e.X, e.Y)));
                int count = RedPenListBox.Items.Count;
                if (SelectedPageTag is TagControls.PageTag pageTag && index1 != index2 && index1 >= 0 && index1 < count && index2 >= 0 && index2 < count)
                {
                    string text = pageTag.RedPenList[index1];
                    pageTag.RedPenList.RemoveAt(index1);
                    pageTag.RedPenList.Insert(index2, text);
                    UpdateRedPenList(index2);

                    index1++;
                    index2++;

                    bool down = index1 < index2;
                    int sign = down ? 1 : -1;

                    UpdateContents(SelectedRootNode, match =>
                    {
                        string token = match.Result("$1");
                        int value = int.Parse(token.TrimEnd('-'));
                        int uValue = Math.Abs(value);
                        if (uValue == index1)
                        {
                            return $"<{(value < 0 ? "-" : "")}{index2}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else if ((down && uValue > index1 && uValue <= index2) || (!down && uValue < index1 && uValue >= index2))
                        {
                            return $"<{(value < 0 ? "-" : "")}{uValue - sign}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else
                        {
                            return match.Value;
                        }
                    });

                    Edited = true;

                    UpdateContentsText();
                    UpdateImage(SelectedIndex);
                    UpdateStatus("「赤ペン」の順序を変えました。");
                }
            }
        }

        private void RedPenListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void RedPenListBox_DragOver(object sender, DragEventArgs e)
        {
            RedPenListBox.AutoScroll();
        }

        private void RedPenListAddButton_Click(object sender, EventArgs e)
        {
            InsertRedPenOf(RedPenListBox.SelectedIndex);
        }

        private void ScaleTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateScale();
        }
    }
}
