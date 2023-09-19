using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class RedPenControl : UserControl
    {
        public RedPenControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class RedPenTag : BaseTag
    {
        public override string Name => "赤ペン";

        public override TagType Type => TagType.RedPen;

        public override string DialogText => "「赤ペン」は赤シートで隠したい用語などを表します。リストの\n上からn番目の用語は<n> で文章に挿入することができます。";

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

        private static readonly RedPenControl Control = new RedPenControl();

        public RedPenTag()
        {
            Text = "用語など";
        }
    }
}
