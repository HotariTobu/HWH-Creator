using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharedCSharp.Extension;

namespace HWH_Creator.TagControls
{
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }
    }

    public class PageTag : BaseTag
    {
        public override string Name => "ページ";

        public override TagType Type => TagType.Page;

        public override string DialogText => "「ページ」は用紙単位のまとまりです。トップノードになります。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"RedPenList = {string.Join("\r,\n", RedPenList)}",
                    $"HasBC = {HasBC}",
                    $"HasDate = {HasDate}",
                    $"HasAbout = {HasAbout}",
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
                        case "RedPenList":
                            RedPenList = data.Split(new string[] { "\r,\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            break;
                        case "HasBC":
                            HasBC = data.ParseTo(false);
                            break;
                        case "HasDate":
                            HasDate = data.ParseTo(false);
                            break;
                        case "HasAbout":
                            HasAbout = data.ParseTo(false);
                            break;
                    }
                }
            }
        }

        public override bool ApplyContents()
        {
            Text = Control.TextBox.Text;
            HasBC = Control.BCCheckBox.Checked;
            HasDate = Control.DateCheckBox.Checked;
            HasAbout = Control.AboutCheckBox.Checked;
            return true;
        }

        public override Control InitializeControl()
        {
            Control.TextBox.Text = Text;
            Control.BCCheckBox.Checked = HasBC;
            Control.DateCheckBox.Checked = HasDate;
            Control.AboutCheckBox.Checked = HasAbout;
            return Control;
        }

        private static readonly PageControl Control = new PageControl();

        public PageTag()
        {
            Text = "タイトル";
            RedPenList = new List<string>();
            HasBC = false;
            HasDate = false;
            HasAbout = false;
        }

        public List<string> RedPenList { get; set; }
        public bool HasBC { get; private set; }
        public bool HasDate { get; private set; }
        public bool HasAbout { get; private set; }
    }
}
