using SharedCSharp;
using SharedCSharp.Extension;
using System;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class BlockControl : UserControl
    {
        public BlockControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class BlockTag : BaseTag
    {
        public override string Name => "ブロック";

        public override TagType Type => TagType.Block;

        public override string DialogText => "「ブロック」は「見出し」の中で「出来事」や「期間」をいくつかの\nグループに分けます。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"Interval = {Interval}",
                    $"IsLine = {IsLine}",
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
                        case "Interval":
                            Interval = data.ParseTo(1);
                            break;
                        case "IsLine":
                            IsLine = data.ParseTo(false);
                            break;
                    }
                }
            }
        }

        public override bool ApplyContents()
        {
            Interval = (int)Control.IntervalNumericUpDown.Value;
            IsLine = Control.LineCheckBox.Checked;
            return true;
        }

        public override Control InitializeControl()
        {
            try
            {
                Control.IntervalNumericUpDown.Value = Interval;
            }
            catch (Exception e)
            {
                FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
            }
            Control.LineCheckBox.Checked = IsLine;
            return Control;
        }

        private static readonly BlockControl Control = new BlockControl();

        public BlockTag()
        {
            Text = string.Empty;
            Interval = 1;
            IsLine = false;
        }

        public int Interval { get; set; }
        public bool IsLine { get; set; }
    }
}
