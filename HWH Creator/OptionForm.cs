using SharedCSharp;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

/*
 * 設定の保存方法
 * コントロール=>コンテキスト=>ファイル=>コントロール=>・・・
 */

namespace HWH_Creator
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();

            for (int i = 0; i < SaveCharList.Items.Count; i++)
            {
                SaveCharList.SetItemChecked(i, true);
            }

            FontTextButton.Tag = new Font("游明朝", 36F, FontStyle.Regular, GraphicsUnit.Point);
            FontPageButton.Tag = new Font("游ゴシック", 64F, FontStyle.Regular, GraphicsUnit.Point);
            FontPageNumberButton.Tag = new Font("游ゴシック", 24F, FontStyle.Regular, GraphicsUnit.Point);
            FontHeadlineButton.Tag = new Font("游ゴシック", 48F, FontStyle.Regular, GraphicsUnit.Point);
            FontTextBoxButton.Tag = new Font("游ゴシック", 42F, FontStyle.Regular, GraphicsUnit.Point);
            FontRedPenButton.Tag = new Font("游ゴシック", 36F, FontStyle.Regular, GraphicsUnit.Point);

            PaperSizeComboBox.SelectedIndex = 0;
            DPIComboBox.SelectedIndex = 0;
            
            SearchComboBox.SelectedIndex = 0;

            if (File.Exists("Option.txt"))
            {
                OptionsPort = File.ReadAllLines("Option.txt", Encoding.UTF8);
            }
            else
            {
                ApplyOptions();
            }
        }

        public void Save()
        {
            File.WriteAllLines("Option.txt", OptionsPort, Encoding.UTF8);
        }

        public Context Options { get; private set; }

        private void ApplyOptions()
        {
            char[] saveCharCheckedList = new char[SaveCharList.CheckedItems.Count];
            for (int i = 0; i < saveCharCheckedList.Length; i++)
            {
                string str = SaveCharList.CheckedItems[i].ToString();
                saveCharCheckedList[i] = str.Length == 1 ? str[0] : ' ';
            }

            Size paperSize = PaperSizes[PaperSizeComboBox.SelectedIndex];
            float dpi = float.Parse(DPIComboBox.Text);
            Graphics g = CreateGraphics();
            SizeF displayDpi = new SizeF(g.DpiX, g.DpiY);
            g.Dispose();
            const float Inch = 25.4f;

            string searchToken;
            switch ((SearchEngines)SearchComboBox.SelectedIndex)
            {
                case SearchEngines.Google:
                    searchToken = "https://www.google.co.jp/search?q=";
                    break;
                case SearchEngines.Yahoo:
                    searchToken = "https://search.yahoo.co.jp/search?p=";
                    break;
                case SearchEngines.Bing:
                    searchToken = "https://www.bing.com/search?q=";
                    break;
                default:
                    return;
            }

            Options = new Context()
            {
                RedPenColor = RedPenColorButton.BackColor,
                RedPenNumberZeroLeft = RedPenNumberZeroLeftCheckBox.Checked,
                RedPenTextVisible = RedPenTextVisibleCheckBox.Checked,
                RedPenAddSearch = RedPenAddSearchCheckBox.Checked,
                RedPenAddConvert = RedPenAddConvertCheckBox.Checked,

                BorderThickness = (int)BorderThicknessNumericUpDown.Value,
                LineInterval = (float)LineIntervalNumericUpDown.Value,
                PagePadding = (float)PaddingNumericUpDown.Value,
                AutoScrollAfterNode = AutoScrollAfterNodeCheckBox.Checked,

                SaveCharList = saveCharCheckedList,

                TextFont = FontTextButton.Tag as Font,
                PageFont = FontPageButton.Tag as Font,
                PageNumberFont = FontPageNumberButton.Tag as Font,
                HeadlineFont = FontHeadlineButton.Tag as Font,
                TextBoxFont = FontTextBoxButton.Tag as Font,
                RedPenFont = FontRedPenButton.Tag as Font,

                PaperName = PaperSizeComboBox.Text.Split('(')[0],
                PageSize = new Size((int)(paperSize.Width * dpi / Inch), (int)(paperSize.Height * dpi / Inch)),
                PictureBoxSize = new Size((int)(paperSize.Width * displayDpi.Width / Inch), (int)(paperSize.Height * displayDpi.Height / Inch)),

                ScaleMax = (int)ScaleMaxNumericUpDown.Value,
                ScaleMin = (int)ScaleMinNumericUpDown.Value,
                Margin = (int)MarginNumericUpDown.Value,

                SearchEngine = (SearchEngines)SearchComboBox.SelectedIndex,
                Browser = SystemBrowserRadioButton.Checked ? Browsers.SystemBrowser : WindowBrowserRadioButton.Checked ? Browsers.Window : TabBrowserRadioButton.Checked ? Browsers.Tab : Browsers.SystemBrowser,
                SearchToken = searchToken,

                StartPageEnabled = StartPageEnabledCheckBox.Checked,
                StartPagePathsLimit = (int)StartPagePathsLimitNumericUpDown.Value,
            };

            try
            {
                FontPageNumericUpDown.Value = (decimal)(((FontPageButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                FontPageNumberNumericUpDown.Value = (decimal)(((FontPageNumberButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                FontHeadlineNumericUpDown.Value = (decimal)(((FontHeadlineButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                FontTextBoxNumericUpDown.Value = (decimal)(((FontTextBoxButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
            }
            catch (System.Exception e)
            {
                FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
            }
        }

        private readonly Size[] PaperSizes = new Size[]
        {
            //A4
            new Size(210,297),
            //B5
            new Size(182,257),
        };
    }

    public enum SearchEngines
    {
        Google,
        Yahoo,
        Bing,
    }

    public enum Browsers
    {
        SystemBrowser,
        Window,
        Tab,
    }
}
