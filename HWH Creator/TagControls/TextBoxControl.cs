using System;
using System.Drawing;
using System.Windows.Forms;
using SharedCSharp;
using SharedCSharp.Extension;

namespace HWH_Creator.TagControls
{
    public partial class TextBoxControl : UserControl
    {
        public TextBoxControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            FuncCenter.CallFunc((int)MainForm.FuncKeys.GetOptions, null, out object result);
            if (result is OptionForm.Context options)
            {
                try
                {
                    XNumericUpDown.Value = YNumericUpDown.Value = (decimal)options.PagePadding;
                }
                catch (Exception ee)
                {
                    FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, ee);
                }
            }
        }
    }

    public class TextBoxTag : BaseTag
    {
        public override string Name => "テキストボックス";

        public override TagType Type => TagType.TextBox;

        public override string DialogText => "「テキストボックス」は文章の中でどこにでも配置できます。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"X = {X}",
                    $"Y = {Y}",
                    $"LabelAlignment = {(int)LabelAlignment}",
                });

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                float x = 0, y = 0, width = 0, height = 0;

                foreach (string line in value.Split(new string[] { "\r\t\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int index = line.IndexOf('=');
                    if (index == -1)
                    {
                        continue;
                    }

                    string data = line.Substring(index).TrimStart('=', ' ');
                    switch (line.Substring(0, index).Trim())
                    {
                        case "Text":
                            Text = data;
                            break;
                        case "X":
                            x = data.ParseTo(0f);
                            break;
                        case "Y":
                            y = data.ParseTo(0f);
                            break;
                        case "LabelAlignment":
                            LabelAlignment = (StringAlignment)data.ParseTo(0);
                            break;
                    }
                }

                Rectangle = new RectangleF(x, y, width, height);
            }
        }

        public override bool ApplyContents()
        {
            float x = (float)Control.XNumericUpDown.Value;
            float y = (float)Control.YNumericUpDown.Value;
            Rectangle = new RectangleF(x, y, Rectangle.Width, Rectangle.Height);
            LabelAlignment = (StringAlignment)Control.AlignmentComboBox.SelectedIndex;
            Text = Control.TextBox.Text;

            return true;
        }

        public override Control InitializeControl()
        {
            try
            {
                Control.XNumericUpDown.Value = (decimal)X;
                Control.YNumericUpDown.Value = (decimal)Y;
            }
            catch (Exception e)
            {
                FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
            }
            Control.AlignmentComboBox.SelectedIndex = (int)LabelAlignment;
            Control.TextBox.Text = Text;
            return Control;
        }

        private static readonly TextBoxControl Control = new TextBoxControl();

        public TextBoxTag()
        {
            Text = "テキスト";
            LabelAlignment = StringAlignment.Near;
        }

        public float X => Rectangle.X;
        public float Y => Rectangle.Y;
        public float Width => Rectangle.Width;
        public float Height => Rectangle.Height;
        public StringAlignment LabelAlignment { get; set; }
        private Image _Image;
        public Image Image { get => _Image; set => Rectangle = new RectangleF(Rectangle.Location, (_Image = value).Size); }

        public bool Equals(TextBoxTag textBoxTag, float range)
        {
            if (textBoxTag == null)
            {
                return false;
            }

            bool result = true;

            result &= textBoxTag.Text.Equals(Text);
            result &= Math.Abs(textBoxTag.X - X) <= range;
            result &= Math.Abs(textBoxTag.Y - Y) <= range;
            result &= textBoxTag.LabelAlignment == LabelAlignment;

            return result;
        }
    }
}
