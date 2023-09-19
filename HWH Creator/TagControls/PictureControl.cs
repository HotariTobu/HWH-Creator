using SharedCSharp;
using SharedWinforms;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SharedCSharp.Extension;

namespace HWH_Creator.TagControls
{
    public partial class PictureControl : UserControl
    {
        public PictureControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        internal void LoadPicture(string path, bool reset = false)
        {
            if (File.Exists(path))
            {
                try
                {
                    PictureBox.Image = ImageReader.ReadImage(path);
                    PathBox.Text = path;

                    WidthNumericUpDown.Value = PictureBox.Image.Width;
                    HeightNumericUpDown.Value = PictureBox.Image.Height;

                    return;
                }
                catch (Exception e)
                {
                    FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
                }
            }
            
            if (reset)
            {
                PictureBox.Image = null;
            }
        }

        private void ReferenceButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog(FindForm()) == DialogResult.OK)
            {
                LoadPicture(OpenFileDialog.FileName);
            }
        }

        private void PictureControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                LoadPicture(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
            }
        }

        private void PictureControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private long LastTextChangedTime { get; set; }
        private int Span => 500;

        private void PathBox_TextChanged(object sender, EventArgs e)
        {
            long textChangedTime = DateTime.Now.GetMilli();
            if (textChangedTime - LastTextChangedTime > Span)
            {
                LoadPicture(PathBox.Text);

                LastTextChangedTime = textChangedTime;
            }
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            LoadPicture(PathBox.Text);
            FuncCenter.CallFunc((int)MainForm.FuncKeys.GetOptions, null, out object result);
            if (result is OptionForm.Context options)
            {
                XNumericUpDown.Value = YNumericUpDown.Value = (decimal)options.PagePadding;
            }
        }
    }

    public class PictureTag : BaseTag
    {
        public override string Name => "画像";

        public override TagType Type => TagType.Picture;

        public override string DialogText => "「画像」は文章の中でどこにでも配置できます。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"Path = {Path}",
                    $"X = {X}",
                    $"Y = {Y}",
                    $"Width = {Width}",
                    $"Height = {Height}",
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
                        case "Path":
                            Path = data;
                            break;
                        case "X":
                            x = data.ParseTo(0f);
                            break;
                        case "Y":
                            y = data.ParseTo(0f);
                            break;
                        case "Width":
                            width = data.ParseTo(0f);
                            break;
                        case "Height":
                            height = data.ParseTo(0f);
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
            Path = Control.PathBox.Text;
            float x = (float)Control.XNumericUpDown.Value;
            float y = (float)Control.YNumericUpDown.Value;
            float width = (float)Control.WidthNumericUpDown.Value;
            float height = (float)Control.HeightNumericUpDown.Value;
            Rectangle = new RectangleF(x, y, width, height);
            LabelAlignment = (StringAlignment)Control.AlignmentComboBox.SelectedIndex;
            Text = Control.TextBox.Text;

            if (!File.Exists(Path))
            {
                if (MessageBox.Show(Control.FindForm(), "存在しないパスが指定されていますが、よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        public override Control InitializeControl()
        {
            Control.PathBox.Text = Path;
            Control.LoadPicture(Path, true);
            try
            {
                Control.XNumericUpDown.Value = (decimal)X;
                Control.YNumericUpDown.Value = (decimal)Y;
                Control.WidthNumericUpDown.Value = (decimal)Width;
                Control.HeightNumericUpDown.Value = (decimal)Height;
            }
            catch (Exception e)
            {
                FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
            }
            Control.AlignmentComboBox.SelectedIndex = (int)LabelAlignment;
            Control.TextBox.Text = Text;
            return Control;
        }

        private static readonly PictureControl Control = new PictureControl();

        public PictureTag()
        {
            Text = "画像の説明";
            Path = "画像ファイルのパス";
            LabelAlignment = StringAlignment.Near;
        }

        public string Path { get; set; }
        public float X => Rectangle.X;
        public float Y => Rectangle.Y;
        public float Width => Rectangle.Width;
        public float Height => Rectangle.Height;
        public StringAlignment LabelAlignment { get; set; }

        public bool Equals(PictureTag pictureTag, float range)
        {
            if (pictureTag == null)
            {
                return false;
            }

            bool result = true;

            result &= pictureTag.Text.Equals(Text);
            result &= pictureTag.Path.Equals(Path);
            result &= Math.Abs(pictureTag.X - X) <= range;
            result &= Math.Abs(pictureTag.Y - Y) <= range;
            result &= Math.Abs(pictureTag.Width - Width) <= range;
            result &= Math.Abs(pictureTag.Height - Height) <= range;
            result &= pictureTag.LabelAlignment == LabelAlignment;

            return result;
        }
    }
}
