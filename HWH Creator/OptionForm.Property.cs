using SharedCSharp;
using SharedCSharp.Extension;
using System;
using System.Drawing;

namespace HWH_Creator
{
    partial class OptionForm
    {
        public struct Context
        {
            public Color RedPenColor;
            public bool RedPenNumberZeroLeft;
            public bool RedPenTextVisible;
            public bool RedPenAddSearch;
            public bool RedPenAddConvert;

            public int BorderThickness;
            public float LineInterval;
            public float PagePadding;
            public bool AutoScrollAfterNode;

            public char[] SaveCharList;

            public Font TextFont;
            public Font PageFont;
            public Font PageNumberFont;
            public Font HeadlineFont;
            public Font TextBoxFont;
            public Font RedPenFont;

            public string PaperName;
            public Size PageSize;
            public Size PictureBoxSize;

            public int ScaleMax;
            public int ScaleMin;
            public int Margin;

            public SearchEngines SearchEngine;
            public Browsers Browser;
            public string SearchToken;

            public bool StartPageEnabled;
            public int StartPagePathsLimit;
        }

        public string[] OptionsPort
        {
            get => new string[]
            {
                $"RedPenColor = {Options.RedPenColor.R},{Options.RedPenColor.G},{Options.RedPenColor.B}",
                $"RedPenNumberZeroLeft = {Options.RedPenNumberZeroLeft}",
                $"RedPenTextVisible = {Options.RedPenTextVisible}",
                $"RedPenAddSearch = {Options.RedPenAddSearch}",
                $"RedPenAddConvert = {Options.RedPenAddConvert}",

                $"BorderThickness = {Options.BorderThickness}",
                $"LineInterval = {Options.LineInterval}",
                $"PagePadding = {Options.PagePadding}",
                $"AutoScrollAfterNode = {Options.AutoScrollAfterNode}",

                $"SaveCharList = {SaveCharListPort}",
                
                $"TextFont = {Options.TextFont.FontFamily.Name},{Options.TextFont.Size},{Options.TextFont.Bold},{Options.TextFont.Italic}",
                $"PageFont = {Options.PageFont.FontFamily.Name},{Options.PageFont.Size},{Options.PageFont.Bold},{Options.PageFont.Italic}",
                $"PageNumberFont = {Options.PageNumberFont.FontFamily.Name},{Options.PageNumberFont.Size},{Options.PageNumberFont.Bold},{Options.PageNumberFont.Italic}",
                $"HeadlineFont = {Options.HeadlineFont.FontFamily.Name},{Options.HeadlineFont.Size},{Options.HeadlineFont.Bold},{Options.HeadlineFont.Italic}",
                $"TextBoxFont = {Options.TextBoxFont.FontFamily.Name},{Options.TextBoxFont.Size},{Options.TextBoxFont.Bold},{Options.TextBoxFont.Italic}",
                $"RedPenFont = {Options.RedPenFont.FontFamily.Name},{Options.RedPenFont.Size},{Options.RedPenFont.Bold},{Options.RedPenFont.Italic}",

                $"PaperSize = {PaperSizeComboBox.SelectedIndex}",
                $"dpi = {DPIComboBox.Text}",

                $"ScaleMax = {Options.ScaleMax}",
                $"ScaleMin = {Options.ScaleMin}",
                $"Margin = {Options.Margin}",

                $"SearchEngine = {(int)Options.SearchEngine}",
                $"Browser = {(int)Options.Browser}",

                $"StartPageEnabled = {Options.StartPageEnabled}",
                $"StartPagePathsLimit = {Options.StartPagePathsLimit}",
            };

            set
            {
                if (value == null)
                {
                    return;
                }

                foreach (string line in value)
                {
                    try
                    {
                        int index = line.IndexOf('=');
                        if (index == -1)
                        {
                            continue;
                        }

                        string left = line.Substring(0, index).Trim();
                        string right = line.Substring(index).TrimStart('=', ' ');

                        string[] data = right.Split(',');
                        switch (left)
                        {
                            case "RedPenColor":
                                RedPenColorButton.BackColor = SharedWinforms.StringParsers.ParseToColor(Color.FromArgb(255, 180, 50), data);
                                break;
                            case "RedPenNumberZeroLeft":
                                RedPenNumberZeroLeftCheckBox.Checked = right.ParseTo(true);
                                break;
                            case "RedPenTextVisible":
                                RedPenTextVisibleCheckBox.Checked = right.ParseTo(true);
                                break;
                            case "RedPenAddSearch":
                                RedPenAddSearchCheckBox.Checked = right.ParseTo(false);
                                break;
                            case "RedPenAddConvert":
                                RedPenAddConvertCheckBox.Checked = right.ParseTo(false);
                                break;

                            case "BorderThickness":
                                BorderThicknessNumericUpDown.Value = right.ParseTo(2m);
                                break;
                            case "LineInterval":
                                LineIntervalNumericUpDown.Value = right.ParseTo(0.5m);
                                break;
                            case "PagePadding":
                                PaddingNumericUpDown.Value = right.ParseTo(50m);
                                break;
                            case "AutoScrollAfterNode":
                                AutoScrollAfterNodeCheckBox.Checked = right.ParseTo(false);
                                break;

                            case "SaveCharList":
                                SaveCharListPort = right;
                                break;

                            case "TextFont":
                                FontTextButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游明朝", 36F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;
                            case "PageFont":
                                FontPageButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游ゴシック", 64F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;
                            case "PageNumberFont":
                                FontPageNumberButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游ゴシック", 24F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;
                            case "HeadlineFont":
                                FontHeadlineButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游ゴシック", 48F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;
                            case "TextBoxFont":
                                FontTextBoxButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游ゴシック", 42F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;
                            case "RedPenFont":
                                FontRedPenButton.Tag = SharedWinforms.StringParsers.ParseToFont(new Font("游ゴシック", 36F, FontStyle.Regular, GraphicsUnit.Point), data);
                                break;

                            case "PaperSize":
                                PaperSizeComboBox.SelectedIndex = right.ParseTo(0);
                                break;
                            case "dpi":
                                DPIComboBox.Text = right.ParseTo(350).ToString();
                                break;

                            case "ScaleMax":
                                ScaleMaxNumericUpDown.Value = right.ParseTo(100m);
                                break;
                            case "ScaleMin":
                                ScaleMinNumericUpDown.Value = right.ParseTo(10m);
                                break;
                            case "Margin":
                                MarginNumericUpDown.Value = right.ParseTo(10m);
                                break;

                            case "SearchEngine":
                                SearchComboBox.SelectedIndex = right.ParseTo(0);
                                break;
                            case "Browser":
                                switch ((Browsers)right.ParseTo(0))
                                {
                                    case Browsers.SystemBrowser:
                                        SystemBrowserRadioButton.Checked = true;
                                        break;
                                    case Browsers.Window:
                                        WindowBrowserRadioButton.Checked = true;
                                        break;
                                    case Browsers.Tab:
                                        TabBrowserRadioButton.Checked = true;
                                        break;
                                    default:
                                        SystemBrowserRadioButton.Checked = true;
                                        break;
                                }
                                break;

                            case "StartPageEnabled":
                                StartPageEnabledCheckBox.Checked = right.ParseTo(true);
                                break;
                            case "StartPagePathsLimit":
                                StartPagePathsLimitNumericUpDown.Value = right.ParseTo(10m);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
                    }
                }

                ApplyOptions();
            }
        }

        private string SaveCharListPort
        {
            get
            {
                string result = string.Empty;

                for (int i = 0; i < SaveCharList.Items.Count; i++)
                {
                    result += $"{SaveCharList.Items[i]}{(SaveCharList.CheckedIndices.Contains(i) ? 't' : 'f')}";
                }

                return result;
            }

            set
            {
                SaveCharList.Items.Clear();

                for (int i = 0; i + 1 < value.Length; i += 2)
                {
                    SaveCharList.Items.Add(value[i], value[i + 1] == 't');
                }
            }
        }
    }
}
