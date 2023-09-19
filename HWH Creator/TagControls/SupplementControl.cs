using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class SupplementControl : UserControl
    {
        public SupplementControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class SupplementTag : BaseTag
    {
        public override string Name => "補足";

        public override TagType Type => TagType.Supplement;

        public override string DialogText => "「補足」は親ノードの内容について補足をします。";

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

        private static readonly SupplementControl Control = new SupplementControl();

        public SupplementTag()
        {
            Text = "補足内容";
        }
    }
}
