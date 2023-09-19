using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class ElementControl : UserControl
    {
        public ElementControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class ElementTag : BaseTag
    {
        public override string Name => "要素";

        public override TagType Type => TagType.Element;

        public override string DialogText => "「要素」は親ノードのある要素を表します。親ノードが\n「説明リスト」だった場合、リストの要素として扱われます。";

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

        private static readonly ElementControl Control = new ElementControl();

        public ElementTag()
        {
            Text = "要素";
        }
    }
}
