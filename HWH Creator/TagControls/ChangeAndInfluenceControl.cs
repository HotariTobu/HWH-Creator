using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class ChangeAndInfluenceControl : UserControl
    {
        public ChangeAndInfluenceControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class ChangeAndInfluenceTag : BaseTag
    {
        public override string Name => "変化や影響";

        public override TagType Type => TagType.ChangeAndInfluence;

        public override string DialogText => "「変化や影響」は親ノードからの変化や親ノードによる影響を\n表します。";

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

        private static readonly ChangeAndInfluenceControl Control = new ChangeAndInfluenceControl();

        public ChangeAndInfluenceTag()
        {
            Text = "変化や影響の内容";
        }
    }
}
