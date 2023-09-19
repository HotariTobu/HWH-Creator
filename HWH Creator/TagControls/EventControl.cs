using SharedCSharp;
using SharedCSharp.Extension;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class EventControl : UserControl
    {
        public EventControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        internal bool HasDate { get; set; }

        internal string Month { get => int.TryParse(DateBox.Text.Split('/').ElementAtOrDefault(0), out int result) ? $"{result, 2}" : "  "; }
        internal string DayOfMonth { get => int.TryParse(DateBox.Text.Split('/').ElementAtOrDefault(1), out int result) ? $"{result, 2}" : "  "; }

        private void CenturyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearBox.Mask = CenturyBox.SelectedIndex == 0 ? "9999" : "99";
            DateBox.Visible = CenturyBox.SelectedIndex == 0 && HasDate;
        }
    }

    public class EventTag : BaseTag
    {
        public override string Name => "出来事";

        public override TagType Type => TagType.Event;

        public override string DialogText => "「出来事」は西暦や世紀と一緒に出来事を表します。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"IsBC = {IsBC}",
                    $"Year = {Year}",
                    $"IsCentury = {IsCentury}",
                    $"Date = {Date}",
                    $"IsAbout = {IsAbout}",
                    $"IsYearRed = {IsYearRed}",
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
                        case "IsBC":
                            IsBC = data.ParseTo(false);
                            break;
                        case "Year":
                            Year = data;
                            break;
                        case "IsCentury":
                            IsCentury = data.ParseTo(false);
                            break;
                        case "Date":
                            Date = new string(' ', 5 - data.Length) + data;
                            break;
                        case "IsAbout":
                            IsAbout = data.ParseTo(false);
                            break;
                        case "IsYearRed":
                            IsYearRed = data.ParseTo(false);
                            break;
                    }
                }
            }
        }

        public override bool ApplyContents()
        {
            IsBC = Control.ACBDCheckBox.Checked;
            Year = Control.YearBox.Text;
            IsCentury = Control.CenturyBox.SelectedIndex == 1;
            Date = Control.Month + "/" + Control.DayOfMonth;
            IsAbout = Control.AboutCheckBox.Checked;
            IsYearRed = Control.YearRedCheckBox.Checked;
            Text = Control.TextBox.Text;

            if (Control.YearBox.Text.Length != 0)
            {
                switch (Control.CenturyBox.SelectedIndex)
                {
                    case 0:
                        if (Control.YearBox.Text.Length <= 2)
                        {
                            if (MessageBox.Show(Control.FindForm(), "2桁以下の西暦でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                    case 1:
                        if (Control.YearBox.Text.Length >= 3)
                        {
                            if (MessageBox.Show(Control.FindForm(), "3桁以上の世紀でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            if (Date.Length == 1)
            {
                Date = string.Empty;
            }

            return true;
        }

        public override Control InitializeControl()
        {
            FuncCenter.CallFunc((int)MainForm.FuncKeys.GetSelectedPageTag, null, out object result);
            if (result is TagControls.PageTag pageTag)
            {
                Control.ACBDCheckBox.Visible = pageTag.HasBC;
                Control.DateBox.Visible = pageTag.HasDate;
                Control.AboutCheckBox.Visible = pageTag.HasAbout;

                Control.HasDate = pageTag.HasDate;
                Control.CenturyBox.SelectedIndex = 0;
            }

            Control.ACBDCheckBox.Checked = IsBC;
            Control.YearBox.Text = Year;
            Control.CenturyBox.SelectedIndex = IsCentury ? 1 : 0;
            Control.DateBox.Text = Date;
            Control.AboutCheckBox.Checked = IsAbout;
            Control.YearRedCheckBox.Checked = IsYearRed;
            Control.TextBox.Text = Text;
            return Control;
        }

        private static readonly EventControl Control = new EventControl();

        public EventTag()
        {
            Text = "出来事";
            IsBC = false;
            Year = string.Empty;
            IsCentury = false;
            Date = string.Empty;
            IsAbout = false;
            IsYearRed = false;
        }

        public bool IsBC { get; set; }
        public string Year { get; set; }
        public bool IsCentury { get; set; }
        public string Date { get; set; }
        public bool IsAbout { get; set; }
        public bool IsYearRed { get; set; }
    }
}
