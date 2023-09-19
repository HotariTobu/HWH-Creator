using SharedCSharp;
using SharedCSharp.Extension;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        public enum FuncKeys
        {
            AddRedPenToList,
            ExportException,
            GetOptions,
            GetSelectedPageTag,
            RedPenIndexOf,
            SearchText,
        }

        private void AddFuncs()
        {
            FuncCenter.AddFunc((int)FuncKeys.AddRedPenToList, AddRedPenToList);
            FuncCenter.AddFunc((int)FuncKeys.ExportException, ExportException);
            FuncCenter.AddFunc((int)FuncKeys.GetOptions, x => Options);
            FuncCenter.AddFunc((int)FuncKeys.GetSelectedPageTag, x=>SelectedPageTag);
            FuncCenter.AddFunc((int)FuncKeys.RedPenIndexOf, RedPenIndexOf);
            FuncCenter.AddFunc((int)FuncKeys.SearchText, SearchText);
        }

        private object AddRedPenToList(object value)
        {
            string text = value as string;
            if (string.IsNullOrWhiteSpace(text) || text.Equals(new Regex(@"<(-?\d+?-?)>").Match(text).Value))
            {
                return null;
            }

            if (SelectedPageTag is TagControls.PageTag pageTag)
            {
                if (pageTag.RedPenList.ContainsWithEqualsMethod(text))
                {
                    return null;
                }

                pageTag.RedPenList = pageTag.RedPenList.Append(text).ToList();

                Edited = true;

                UpdateRedPenList(pageTag.RedPenList.Count - 1);

                string status = $"「赤ペン」リストに\"{text}\"を追加し";

                if (Options.RedPenAddSearch)
                {
                    SearchText(text);
                    status += "、検索し";
                }

                if (Options.RedPenAddConvert)
                {
                    ConvertRedPen(pageTag.RedPenList.Count - 1);
                    status += "、変換し";
                }

                UpdateStatus(status + "ました。");
            }

            return null;
        }

        private object ExportException(object value)
        {
            if (value is Exception e)
            {
                MessageBox.Show(this, e.Message, "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string log = "Exceptions.log";
                File.AppendAllText(log,
                            $"{DateTime.Now.ToLongDateString()}_{DateTime.Now.ToLongTimeString()}\n" +
                            $"\t{e.Message.Replace("\n", "\n\t")}\n" +
                            $"\t{e.StackTrace.Replace("\n", "\n\t")}\n\n");

                UpdateStatus($"例外を記録しました。詳しくは{log}を参照してください。");
            }
            return null;
        }

        private object RedPenIndexOf(object value)
        {
            if (value is string text && !string.IsNullOrWhiteSpace(text))
            {
                return SelectedPageTag?.RedPenList?.FindIndex(x=>x.Equals(text));
            }

            return -1;
        }

        private object SearchText(object value)
        {
            if (value is string text && !string.IsNullOrWhiteSpace(text))
            {
                text = ReplaceRedPenToText(text);

                string url = Options.SearchToken;

                foreach (byte part in System.Text.Encoding.UTF8.GetBytes(text))
                {
                    url += $"%{System.Convert.ToInt32(part):x}";
                }

                switch (OptionForm.Options.Browser)
                {
                    case Browsers.SystemBrowser:
                        System.Diagnostics.Process.Start(url);
                        break;
                    case Browsers.Window:
                        BrowseForm browseForm = new BrowseForm()
                        {
                            Icon = Icon,
                            Text = text,
                            Url = url,
                        };
                        browseForm.Show();
                        break;
                    case Browsers.Tab:
                        TabPage tabPage = new TabPage(text);
                        tabPage.Controls.Add(new BrowseTab()
                        {
                            Url = url,
                            TabPage = tabPage,
                        });
                        BrowseTabControl.TabPages.Add(tabPage);
                        break;
                    default:
                        break;
                }

                UpdateStatus($"\"{text}\"を検索しました。");
            }

            return null;
        }
    }
}
