using SharedCSharp;
using SharedCSharp.Extension;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HWH_Creator.TagControls
{
    public partial class PeriodControl : UserControl
    {
        public PeriodControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        internal bool HasDate { get; set; }

        internal string Month1 { get => int.TryParse(DateBox1.Text.Split('/').ElementAtOrDefault(0), out int result) ? $"{result,2}" : "  "; }
        internal string DayOfMonth1 { get => int.TryParse(DateBox1.Text.Split('/').ElementAtOrDefault(1), out int result) ? $"{result,2}" : "  "; }
        internal string Month2 { get => int.TryParse(DateBox2.Text.Split('/').ElementAtOrDefault(0), out int result) ? $"{result,2}" : "  "; }
        internal string DayOfMonth2 { get => int.TryParse(DateBox2.Text.Split('/').ElementAtOrDefault(1), out int result) ? $"{result,2}" : "  "; }

        private void CenturyBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearBox1.Mask = CenturyBox1.SelectedIndex == 0 ? "9999" : "99";
            DateBox1.Visible = CenturyBox1.SelectedIndex == 0 && HasDate;
        }

        private void CenturyBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            YearBox2.Mask = CenturyBox2.SelectedIndex == 0 ? "9999" : "99";
            DateBox2.Visible = CenturyBox2.SelectedIndex == 0 && HasDate;
        }
    }

    public class PeriodTag : BaseTag
    {
        public override string Name => "期間";

        public override TagType Type => TagType.Period;

        public override string DialogText => "「期間」は時代などを表します。";

        public override string Data
        {
            get => string.Join("\r\t\n", new string[] {
                    $"Text = {Text}",
                    $"IsBC1 = {IsBC1}",
                    $"IsBC2 = {IsBC2}",
                    $"Year1 = {Year1}",
                    $"Year2 = {Year2}",
                    $"IsCentury1 = {IsCentury1}",
                    $"IsCentury2 = {IsCentury2}",
                    $"Date1 = {Date1}",
                    $"Date2 = {Date2}",
                    $"IsAbout1 = {IsAbout1}",
                    $"IsAbout2 = {IsAbout2}",
                    $"IsYearRed1 = {IsYearRed1}",
                    $"IsYearRed2 = {IsYearRed2}",
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
                        case "IsBC1":
                            IsBC1 = data.ParseTo(false);
                            break;
                        case "IsBC2":
                            IsBC2 = data.ParseTo(false);
                            break;
                        case "Year1":
                            Year1 = data;
                            break;
                        case "Year2":
                            Year2 = data;
                            break;
                        case "IsCentury1":
                            IsCentury1 = data.ParseTo(false);
                            break;
                        case "IsCentury2":
                            IsCentury2 = data.ParseTo(false);
                            break;
                        case "Date1":
                            Date1 = new string(' ', 5 - data.Length) + data; ;
                            break;
                        case "Date2":
                            Date2 = new string(' ', 5 - data.Length) + data; ;
                            break;
                        case "IsAbout1":
                            IsAbout1 = data.ParseTo(false);
                            break;
                        case "IsAbout2":
                            IsAbout2 = data.ParseTo(false);
                            break;
                        case "IsYearRed1":
                            IsYearRed1 = data.ParseTo(false);
                            break;
                        case "IsYearRed2":
                            IsYearRed2 = data.ParseTo(false);
                            break;
                    }
                }
            }
        }

        public override bool ApplyContents()
        {
            IsBC1 = Control.ACBDCheckBox1.Checked;
            IsBC2 = Control.ACBDCheckBox2.Checked;
            Year1 = Control.YearBox1.Text;
            Year2 = Control.YearBox2.Text;
            IsCentury1 = Control.CenturyBox1.SelectedIndex == 1;
            IsCentury2 = Control.CenturyBox2.SelectedIndex == 1;
            Date1 = Control.Month1 + "/" + Control.DayOfMonth1;
            Date2 = Control.Month2 + "/" + Control.DayOfMonth2;
            IsAbout1 = Control.AboutCheckBox1.Checked;
            IsAbout2 = Control.AboutCheckBox2.Checked;
            IsYearRed1 = Control.YearRedCheckBox1.Checked;
            IsYearRed2 = Control.YearRedCheckBox2.Checked;
            Text = Control.TextBox.Text;

            if (Control.YearBox1.Text.Length != 0)
            {
                switch (Control.CenturyBox1.SelectedIndex)
                {
                    case 0:
                        if (Control.YearBox1.Text.Length <= 2)
                        {
                            if (MessageBox.Show(Control.FindForm(), "2桁以下の西暦でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                    case 1:
                        if (Control.YearBox1.Text.Length >= 3)
                        {
                            if (MessageBox.Show(Control.FindForm(), "3桁以上の世紀でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            if (Control.YearBox2.Text.Length != 0)
            {
                switch (Control.CenturyBox2.SelectedIndex)
                {
                    case 0:
                        if (Control.YearBox2.Text.Length <= 2)
                        {
                            if (MessageBox.Show(Control.FindForm(), "2桁以下の西暦でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                    case 1:
                        if (Control.YearBox2.Text.Length >= 3)
                        {
                            if (MessageBox.Show(Control.FindForm(), "3桁以上の世紀でよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            if (Control.YearBox1.Text.ParseTo(0) * (IsBC1 ? -1 : 1) * (IsCentury1 ? 100 : 1) > Control.YearBox2.Text.ParseTo(0) * (IsBC2 ? -1 : 1) * (IsCentury2 ? 100 : 1))
            {
                if (MessageBox.Show(Control.FindForm(), "上の西暦よりも下の西暦の方が小さいですがよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return false;
                }
            }

            if (Date1.Length == 1)
            {
                Date1 = string.Empty;
            }

            if (Date2.Length == 1)
            {
                Date2 = string.Empty;
            }

            return true;
        }

        public override Control InitializeControl()
        {
            FuncCenter.CallFunc((int)MainForm.FuncKeys.GetSelectedPageTag, null, out object result);
            if (result is TagControls.PageTag pageTag)
            {
                Control.ACBDCheckBox1.Visible = pageTag.HasBC;
                Control.ACBDCheckBox2.Visible = pageTag.HasBC;
                Control.DateBox1.Visible = pageTag.HasDate;
                Control.DateBox2.Visible = pageTag.HasDate;
                Control.AboutCheckBox1.Visible = pageTag.HasAbout;
                Control.AboutCheckBox2.Visible = pageTag.HasAbout;

                Control.HasDate = pageTag.HasDate;
                Control.CenturyBox1.SelectedIndex = 0;
                Control.CenturyBox2.SelectedIndex = 0;
            }

            Control.ACBDCheckBox1.Checked = IsBC1;
            Control.ACBDCheckBox2.Checked = IsBC2;
            Control.YearBox1.Text = Year1;
            Control.YearBox2.Text = Year2;
            Control.CenturyBox1.SelectedIndex = IsCentury1 ? 1 : 0;
            Control.CenturyBox2.SelectedIndex = IsCentury2 ? 1 : 0;
            Control.DateBox1.Text = Date1;
            Control.DateBox2.Text = Date2;
            Control.AboutCheckBox1.Checked = IsAbout1;
            Control.AboutCheckBox2.Checked = IsAbout2;
            Control.YearRedCheckBox1.Checked = IsYearRed1;
            Control.YearRedCheckBox2.Checked = IsYearRed2;
            Control.TextBox.Text = Text;
            return Control;
        }

        private static readonly PeriodControl Control = new PeriodControl();

        public PeriodTag()
        {
            Text = "時代など";
            IsBC1 = false;
            IsBC2 = false;
            Year1 = string.Empty;
            Year2 = string.Empty;
            IsCentury1 = false;
            IsCentury2 = false;
            Date1 = string.Empty;
            Date2 = string.Empty;
            IsAbout1 = false;
            IsAbout2 = false;
            IsYearRed1 = false;
            IsYearRed2 = false;
        }

        public bool IsBC1 { get; set; }
        public bool IsBC2 { get; set; }
        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public bool IsCentury1 { get; set; }
        public bool IsCentury2 { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public bool IsAbout1 { get; set; }
        public bool IsAbout2 { get; set; }
        public bool IsYearRed1 { get; set; }
        public bool IsYearRed2 { get; set; }
    }
}
