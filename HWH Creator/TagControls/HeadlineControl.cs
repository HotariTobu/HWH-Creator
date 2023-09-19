using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class HeadlineControl : UserControl
    {
        public HeadlineControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class HeadlineTag : BaseTag
    {
        public override string Name => "見出し";

        public override TagType Type => TagType.Headline;

        public override string DialogText => "「見出し」はページの中の大きなくくりです。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                });

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

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
                    }
                }
            }
        }

        public override bool ApplyContents()
        {
            Text = Control.TextBox.Text;
            return true;
        }

        public override Control InitializeControl()
        {
            Control.TextBox.Text = Text;
            return Control;
        }

        private static readonly HeadlineControl Control = new HeadlineControl();

        public HeadlineTag()
        {
            Text = "サブタイトル";
        }
    }
}
