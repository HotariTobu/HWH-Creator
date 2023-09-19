using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using SharedWinforms.Extension;

namespace HWH_Creator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            AddFuncs();

            InitializeComponent();
            MainPanel.MouseWheel += MainPanel_MouseWheel;
            PrintDocument.DocumentName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}_PrintDocument";

            OptionForm = new OptionForm();
            AboutBox = new AboutBox();
            EditDialog = new EditDialog();
            PictureEditor = new PictureEditor();
            TextBoxEditor = new TextBoxEditor();

            ApplyOptions();
            Reset();

            NodeCounts = new int[20];

            //SystemInformation

            {
                const int value = 11001;

                using (var reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    reg.SetValue(AppDomain.CurrentDomain.FriendlyName, value, RegistryValueKind.DWord);
                }
            }
        }

        private void ApplyOptions()
        {
            try
            {
                Options = OptionForm.Options;

                RedPenListBox.ForeColor = Options.RedPenColor;

                ScaleTrackBar.Maximum = Options.ScaleMax;
                ScaleTrackBar.Minimum = Options.ScaleMin;

                BlackPen = new Pen(Color.Black, Options.BorderThickness);
                BlackBrush = Brushes.Black;
                RedBrush = new Pen(Options.RedPenColor).Brush;

                if (Options.StartPageEnabled)
                {
                    AddStartPage();
                }
                else
                {
                    RemoveStartPage();
                }
            }
            catch (Exception e)
            {
                ExportException(e);
            }

            for (int i = 0; i < ContentsTree.Nodes.Count; i++)
            {
                UpdateImage(i);
            }

            lastScale = 0;
            UpdateScale();

            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                Edited = true;
            }

            UpdateStatus("設定を適用しました。");
        }

        private void Reset()
        {
            Text = AboutBox.AssemblyTitle;
            FilePath = null;
            Edited = false;

            TreeNodeLabel.ResetText();
            ContentsTree.Nodes.Clear();
            ContentsText.ResetText();
            MainPanel.Controls.Clear();
            RedPenListBox.Items.Clear();

            AddStartPage();

            UpdateStatus("新規作成しました。");
        }

        private void AddStartPage()
        {
            if (!Options.StartPageEnabled || MainPanel.Controls.Count > 0)
            {
                return;
            }

            MainPanel.SuspendLayout();
            Size size = MainPanel.ClientSize.Sub(new Size(6, 6));
            StartPage startPage = new StartPage
            {
                Size = size,
                MaximumSize = size,
                MinimumSize = size,
            };
            MainPanel.Controls.Add(startPage);
            startPage.OpenFileButton.Click += OpenToolStripMenuItem_Click;
            startPage.AddPageButton.Click += PageButton_Click;
            startPage.DeletePathsButton.Click += (sender, e) =>
            {
                File.Delete("Paths.txt");
                startPage.LinksFlowLayoutPanel.Controls.Clear();
            };

            try
            {
                if (File.Exists("Paths.txt"))
                {
                    int index = 0;
                    foreach (string path in File.ReadAllLines("Paths.txt", Encoding.UTF8) ?? new string[0])
                    {
                        LinkLabel linkLabel = new LinkLabel
                        {
                            AutoSize = true,
                            Margin = new Padding(5),
                            Size = new Size(56, 12),
                            TabIndex = index,
                            TabStop = true,
                            Text = path
                        };
                        linkLabel.LinkClicked += (sender, e) =>
                        {
                            if (File.Exists(path))
                            {
                                OpenFile(path);
                            }
                            else
                            {
                                if (File.Exists("Paths.txt"))
                                {
                                    File.WriteAllLines("Paths.txt", File.ReadAllLines("Paths.txt", Encoding.UTF8)?.Where(x => !x.Equals(path)) ?? new string[0], Encoding.UTF8);
                                }
                                linkLabel.Dispose();
                            }
                        };
                        startPage.LinksFlowLayoutPanel.Controls.Add(linkLabel);
                        index++;
                    }
                }
            }
            catch (Exception e)
            {
                ExportException(e);
            }
            MainPanel.ResumeLayout();
        }

        private void AddPathToList(string path)
        {
            try
            {
                if (!File.Exists("Paths.txt"))
                {
                    File.Create("Paths.txt").Close();
                }

                File.WriteAllLines("Paths.txt", File.ReadAllLines("Paths.txt", Encoding.UTF8)?.Prepend(path)?.Distinct(EqualityComparer<string>.Default)?.Take(Options.StartPagePathsLimit) ?? new string[0], Encoding.UTF8);
            }
            catch (Exception e)
            {
                ExportException(e);
            }
        }

        private void RemoveStartPage()
        {
            foreach (Control control in MainPanel.Controls)
            {
                if (control is StartPage)
                {
                    control.Dispose();
                }
            }
        }

        private PictureBox AddPageBox()
        {
            MainPanel.SuspendLayout();
            PictureBox page = new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            MainPanel.Controls.Add(page);

            RemoveStartPage();

            page.Size = Options.PictureBoxSize;
            page.Scale(new SizeF(ScaleTrackBar.Value / 100.0f, ScaleTrackBar.Value / 100.0f));
            page.Margin = new Padding(OptionForm.Options.Margin);

            page.MouseDown += MainPanel_MouseDown;
            page.MouseMove += MainPanel_MouseMove;
            page.MouseUp += MainPanel_MouseUp;
            page.MouseClick += MainPanel_MouseClick;
            page.MouseDoubleClick += MainPanel_MouseDoubleClick;
            MainPanel.ResumeLayout();

            return page;
        }

        private void AddNode(int level, bool higher, BaseTag tag)
        {
            TreeNode lastNode = ContentsTree.SelectedNode;
            TreeNode parent = lastNode;
            if (parent == null || parent?.Level < level)
            {
                return;
            }

            if (higher)
            {
                while (parent.Level > level)
                {
                    parent = parent.Parent;
                }
            }

            if (parent.Tag is TagControls.PictureTag || parent.Tag is TagControls.TextBoxTag)
            {
                return;
            }

            TreeNode node = parent.Nodes.Add($"{tag.Name}{++NodeCounts[(int)tag.Type]}");
            node.Tag = tag;
            node.ImageIndex = (int)tag.Type;
            node.SelectedImageIndex = (int)tag.Type;
            parent.Expand();

            ContentsTree.SelectedNode = node;
            if (EditSelectedNode(true))
            {
                UpdateStatus($"ノード「{tag.Name}」を追加しました。");
            }
            else
            {
                NodeCounts[(int)tag.Type]--;
                node.Remove();
                ContentsTree.SelectedNode = lastNode;
            }
        }

        private bool EditSelectedNode(bool addition = false)
        {
            if (ContentsTree.SelectedNode?.Tag is BaseTag tag)
            {
                string tagData = tag.Data;
                EditDialog.Tag = tag;
                if (EditDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ReplaceTagText(EditDialog.Tag);

                    if (addition || !tagData.Equals(tag.Data))
                    {
                        Edited = true;

                        UpdateContentsText();
                        UpdateImage(SelectedIndex);
                        UpdateStatus($"「{ContentsTree.SelectedNode.Text}」を編集しました。");

                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateContentsText()
        {
            ContentsText.Text = (ContentsTree.SelectedNode?.Tag as BaseTag)?.Text ?? string.Empty;
            ContentsText.SelectionStart = ContentsText.TextLength;
            ContentsText.SelectionLength = 0;
            ContentsTextEdited = false;
        }

        private void UpdateContents(TreeNode node, Func<Match, string> func)
        {
            if (node.Tag is BaseTag tag)
            {
                Regex regex = new Regex(@"<(-?\d+?-?)>", RegexOptions.Compiled);
                Match match = regex.Match(tag.Text);

                string content = string.Empty;

                int offset = 0;
                while (match.Success)
                {
                    content += tag.Text.Substring(offset, match.Index - offset) + func(match);
                    offset = match.Index + match.Length;
                    match = match.NextMatch();
                }

                if (offset < tag.Text.Length)
                {
                    content += tag.Text.Substring(offset);
                }

                tag.Text = content;
            }

            foreach (TreeNode child in node.Nodes)
            {
                UpdateContents(child, func);
            }
        }

        private void UpdateRedPenList(int index)
        {
            RedPenListBox.SuspendDrawing();
            RedPenListBox.Items.Clear();
            RedPenListBox.Items.AddRange((SelectedPageTag?.RedPenList ?? new List<string>()).ToArray());
            if (index >= 0 && index < RedPenListBox.Items.Count)
            {
                RedPenListBox.SelectedIndex = index;
            }
            RedPenListBox.ResumeDrawing();
        }

        private int lastScale;
        private void UpdateScale()
        {
            int scale = ScaleTrackBar.Value;
            if (lastScale != scale)
            {
                CloseEditors();

                MainPanel.SuspendLayout();
                float percent = scale / 100.0f;
                Size size = new Size((int)(Options.PictureBoxSize.Width * percent), (int)(Options.PictureBoxSize.Height * percent));
                foreach (Control control in MainPanel.Controls)
                {
                    control.Size = size;
                }
                MainPanel.ResumeLayout();
            }
            lastScale = scale;
        }

        private void UpdateStatus(string text)
        {
            StatusLabel.Text = text;
        }

        private void ConvertRedPen(int index)
        {
            if (SelectedPageTag is TagControls.PageTag pageTag && index >= 0 && index < pageTag.RedPenList.Count)
            {
                string text = pageTag.RedPenList[index];
                string redPen = $"<{index + 1}>";

                void updateContents(TreeNode node)
                {
                    if (node.Tag is BaseTag tag)
                    {
                        tag.Text = tag.Text.Replace(text, redPen);
                    }

                    foreach (TreeNode child in node.Nodes)
                    {
                        updateContents(child);
                    }
                }

                ApplyContentsTextToTag();
                updateContents(SelectedRootNode);

                Edited = true;

                UpdateContentsText();
                UpdateImage(SelectedIndex);
                UpdateStatus($"\"{text}\"を「赤ペン」に変換しました。");
            }
        }

        private PointF PointToImage(Point pictureBoxPoint)
        {
            Size size = new Size();
            if (MainPanel.Controls.Count != 0)
            {
                size = MainPanel.Controls[0].Size;
            }
            return new PointF((float)pictureBoxPoint.X * Options.PageSize.Width / size.Width, (float)pictureBoxPoint.Y * Options.PageSize.Height / size.Height);
        }

        private Point PointToPictureBox(PointF imagePoint)
        {
            Size size = new Size();
            if (MainPanel.Controls.Count != 0)
            {
                size = MainPanel.Controls[0].Size;
            }
            return new Point((int)(imagePoint.X * size.Width / Options.PageSize.Width), (int)(imagePoint.Y * size.Height / Options.PageSize.Height));
        }

        private void InsertRedPenOf(int index)
        {
            if (index != -1)
            {
                ContentsText.SelectedText = $"<{index + 1}>";

                ApplyContentsTextToTag();

                UpdateStatus("「赤ペン」を文章に挿入しました。");
            }
        }

        /// <summary>
        /// 変更確認を行います。
        /// </summary>
        /// <param name="text">行おうとしている操作</param>
        /// <param name="caption"></param>
        /// <returns>キャンセルされればtrue、それ以外はfalse</returns>
        private bool CheckCancel(string text, string caption)
        {
            return (PictureEditor.IsClosed == false && MessageBox.Show(this, "「画像」を編集中ですが、確定させずに" + text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    || (TextBoxEditor.IsClosed == false && MessageBox.Show(this, "「テキストボックス」を編集中ですが、確定させずに" + text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    || (Edited && MessageBox.Show(this, "編集内容を保存せずに" + text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel);
        }
    }
}