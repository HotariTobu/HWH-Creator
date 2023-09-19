using SharedCSharp.Extension;
using System;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContentsTree.Focused)
            {
                if (ContentsTree.SelectedNode is TreeNode node)
                {
                    bool page = node.Level == 0;
                    node.Remove();
                    if (ContentsTree.SelectedNode == null)
                    {
                        TreeNodeLabel.ResetText();
                        ContentsText.ResetText();
                    }

                    Edited = true;

                    if (page)
                    {
                        MainPanel.Controls.RemoveAt(node.Index);
                    }
                    else
                    {
                        UpdateImage(SelectedIndex);
                    }
                    UpdateStatus($"ノード「{node.Text}」を削除しました。");
                }
            }
            else if (RedPenListBox.Focused)
            {
                int index = RedPenListBox.SelectedIndex;
                if (SelectedPageTag is TagControls.PageTag pageTag && index >= 0 && index < pageTag.RedPenList.Count && RedPenListBox.SelectedItem is string text)
                {
                    pageTag.RedPenList.RemoveAt(index);
                    UpdateRedPenList(index);

                    index++;

                    UpdateContents(SelectedRootNode, match =>
                     {
                         string token = match.Result("$1");
                         int value = int.Parse(token.TrimEnd('-'));
                         int uValue = Math.Abs(value);
                         if (uValue == index)
                         {
                             return text;
                         }
                         else if (uValue > index)
                         {
                             return $"<{(value < 0 ? "-" : "")}{uValue - 1}{(token.EndsWith("-") ? "-" : "")}>";
                         }
                         else
                         {
                             return match.Value;
                         }
                     });

                    Edited = true;

                    UpdateContentsText();
                    UpdateImage(SelectedIndex);
                    UpdateStatus($"「赤ペン」リストから\"{text}\"を削除しました。");
                }
            }
        }

        private void UpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContentsTree.Focused)
            {
                if (ContentsTree.SelectedNode is TreeNode node)
                {
                    int index = node.Index;
                    if (index > 0)
                    {
                        index--;
                        ContentsTree.BeginUpdate();
                        if (node.Level > 0)
                        {
                            TreeNode parent = node.Parent;
                            node.Remove();
                            parent.Nodes.Insert(index, node);
                        }
                        else
                        {
                            node.Remove();
                            ContentsTree.Nodes.Insert(index, node);
                            UpdateImage(index + 1);
                        }
                        ContentsTree.SelectedNode = node;
                        ContentsTree.EndUpdate();

                        Edited = true;

                        UpdateImage(SelectedIndex);
                        UpdateStatus($"ノード「{node.Text}」を移動させました。");
                    }
                }
            }
            else if (RedPenListBox.Focused)
            {
                int index1 = RedPenListBox.SelectedIndex;
                int index2 = index1 - 1;
                if (SelectedPageTag is TagControls.PageTag pageTag && index1 > 0 && index1 < pageTag.RedPenList.Count)
                {
                    pageTag.RedPenList = pageTag.RedPenList.Swap(index1, index2).ToList();
                    UpdateRedPenList(index2);

                    index1++;
                    index2++;

                    UpdateContents(SelectedRootNode, match =>
                    {
                        string token = match.Result("$1");
                        int value = int.Parse(token.TrimEnd('-'));
                        int uValue = Math.Abs(value);
                        if (uValue == index1)
                        {
                            return $"<{(value < 0 ? "-" : "")}{index2}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else if (uValue == index2)
                        {
                            return $"<{(value < 0 ? "-" : "")}{index1}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else
                        {
                            return match.Value;
                        }
                    });

                    Edited = true;

                    UpdateContentsText();
                    UpdateImage(SelectedIndex);
                    UpdateStatus("「赤ペン」の順序を変えました。");
                }
            }
        }

        private void DownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContentsTree.Focused)
            {
                if (ContentsTree.SelectedNode is TreeNode node)
                {
                    int index = node.Index;
                    if (index < (node.Level > 0 ? node.Parent.Nodes.Count : ContentsTree.Nodes.Count) - 1)
                    {
                        index++;
                        ContentsTree.BeginUpdate();
                        if (node.Level > 0)
                        {
                            TreeNode parent = node.Parent;
                            node.Remove();
                            parent.Nodes.Insert(index, node);
                        }
                        else
                        {
                            node.Remove();
                            ContentsTree.Nodes.Insert(index, node);
                            UpdateImage(index - 1);
                        }
                        ContentsTree.SelectedNode = node;
                        ContentsTree.EndUpdate();

                        Edited = true;

                        UpdateImage(SelectedIndex);
                        UpdateStatus($"ノード「{node.Text}」を移動させました。");
                    }
                }
            }
            else if (RedPenListBox.Focused)
            {
                int index1 = RedPenListBox.SelectedIndex;
                int index2 = index1 + 1;
                if (SelectedPageTag is TagControls.PageTag pageTag && index2 > 0 && index2 < pageTag.RedPenList.Count)
                {
                    pageTag.RedPenList = pageTag.RedPenList.Swap(index1, index2).ToList();
                    UpdateRedPenList(index2);

                    index1++;
                    index2++;

                    UpdateContents(SelectedRootNode, match =>
                    {
                        string token = match.Result("$1");
                        int value = int.Parse(token.TrimEnd('-'));
                        int uValue = Math.Abs(value);
                        if (uValue == index1)
                        {
                            return $"<{(value < 0 ? "-" : "")}{index2}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else if (uValue == index2)
                        {
                            return $"<{(value < 0 ? "-" : "")}{index1}{(token.EndsWith("-") ? "-" : "")}>";
                        }
                        else
                        {
                            return match.Value;
                        }
                    });

                    Edited = true;

                    UpdateContentsText();
                    UpdateImage(SelectedIndex);
                    UpdateStatus("「赤ペン」の順序を変えました。");
                }
            }
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool hasNotPage = true;
            foreach (Control control in MainPanel.Controls)
            {
                if (control is PictureBox)
                {
                    hasNotPage = false;
                    break;
                }
            }
            if (hasNotPage)
            {
                MessageBox.Show(this, "文章がないので印刷できません。", "印刷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintDialog.PrinterSettings.FromPage = PrintDialog.PrinterSettings.MinimumPage = 1;
            PrintDialog.PrinterSettings.ToPage = PrintDialog.PrinterSettings.MaximumPage = MainPanel.Controls.Count;

            if (PrintDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
        }

        private void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (PrintDocument.PrinterSettings.PrintRange)
            {
                case System.Drawing.Printing.PrintRange.AllPages:
                    PrintDialog.PrinterSettings.FromPage = 1;
                    PrintDialog.PrinterSettings.ToPage = MainPanel.Controls.Count;
                    break;
                case System.Drawing.Printing.PrintRange.SomePages:
                    if (PrintDocument.PrinterSettings.FromPage > PrintDocument.PrinterSettings.ToPage)
                    {
                        PrintDocument.PrinterSettings.FromPage ^= PrintDocument.PrinterSettings.ToPage;
                        PrintDocument.PrinterSettings.ToPage ^= PrintDocument.PrinterSettings.FromPage;
                        PrintDocument.PrinterSettings.FromPage ^= PrintDocument.PrinterSettings.ToPage;
                    }
                    break;
                case System.Drawing.Printing.PrintRange.Selection:
                    break;
                case System.Drawing.Printing.PrintRange.CurrentPage:
                    {
                        int index = SelectedIndex;
                        if (index == -1)
                        {
                            MessageBox.Show(this, "ページが選択されていないので印刷できません。", "印刷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            PrintDialog.PrinterSettings.FromPage = PrintDialog.PrinterSettings.ToPage = index + 1;
                        }
                    }
                    break;
                default:
                    break;
            }

            string paperName = OptionForm.Options.PaperName;
            foreach (PaperSize paperSize in PrintDocument.PrinterSettings.PaperSizes)
            {
                if (paperSize.PaperName.StartsWith(paperName))
                {
                    PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = PrintDocument.DefaultPageSettings.PaperSize = paperSize;
                    break;
                }
            }

            pageIndex = PrintDocument.PrinterSettings.FromPage - 1;

            UpdateStatus("印刷が開始されました。");
        }

        private void PrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            UpdateStatus("印刷が完了しました。");
        }

        private int pageIndex;
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (e.PageSettings.Landscape)
            {
                MessageBox.Show(this, "ページは縦方向で印刷する必要があります。", "印刷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            if (pageIndex >= 0 && pageIndex < MainPanel.Controls.Count && MainPanel.Controls[pageIndex] is PictureBox pictureBox)
            {
                UpdateImage(pageIndex, true);
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                e.Graphics.DrawImage(pictureBox.Image, e.PageBounds);
            }

            pageIndex++;

            if (pageIndex == e.PageSettings.PrinterSettings.ToPage)
            {
                e.HasMorePages = false;

            }
            else
            {
                e.HasMorePages = true;
            }
        }
    }
}
