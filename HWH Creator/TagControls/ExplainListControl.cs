using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class ExplainListControl : UserControl
    {
        public ExplainListControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class ExplainListTag : BaseTag
    {
        public override string Name => "説明リスト";

        public override TagType Type => TagType.ExplainList;

        public override string DialogText => "「説明リスト」は説明をリスト化します。「要素」を子ノードとして\n追加し、リストを構成します。";

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

        private static readonly ExplainListControl Control = new ExplainListControl();

        public ExplainListTag()
        {
            Text = "説明するもの";
        }
    }
}
