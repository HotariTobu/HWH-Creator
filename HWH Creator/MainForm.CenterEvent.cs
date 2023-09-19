using SharedCSharp.Extension;
using SharedWinforms.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private bool mainPanelDrgging;
        private Point lastPos;
        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mainPanelDrgging = e.Button == MouseButtons.Middle;
            lastPos = Cursor.Position;
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mainPanelDrgging)
            {
                MainPanel.ScrollBy(new Size(Cursor.Position.Sub(lastPos)));
                lastPos = Cursor.Position;
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mainPanelDrgging = false;
        }

        private void MainPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
            {
                if (sender is Control control)
                {
                    int index = MainPanel.Controls.IndexOf(control);
                    if (index >= 0 && index < ContentsTree.Nodes.Count)
                    {
                        PointF imagePoint = PointToImage(e.Location);

                        //「画像」、「テキストボックス」だけ走査
                        foreach ((RectangleF, TreeNode) rect in control.Tag as List<(RectangleF, TreeNode)>)
                        {
                            if (rect.Item1.Contains(imagePoint))
                            {
                                if (rect.Item2 is TreeNode node && node.Parent is TreeNode parent)
                                {
                                    if (node.Tag is TagControls.PictureTag pictureTag)
                                    {
                                        CloseEditors();

                                        int nodeIndex = node.Index;
                                        parent.Nodes.Remove(node);

                                        UpdateImage(index);

                                        parent.Nodes.Insert(nodeIndex, node);
                                        ContentsTree.SelectedNode = node;

                                        string pictureTagData = pictureTag?.Data;
                                        PictureEditor.PictureTag = pictureTag;
                                        PictureEditor.PageSize = Options.PageSize;
                                        PictureEditor.Box = control;
                                        PictureEditor.Finish = () =>
                                        {
                                            control.Controls.Remove(PictureEditor);

                                            UpdateImage(index);

                                            if (!pictureTag?.Equals(new TagControls.PictureTag() { Data = pictureTagData }, Math.Max(Math.Abs(control.Width - ((int)((float)control.Width * control.Width / Options.PageSize.Width) * (float)Options.PageSize.Width / control.Width)), Math.Abs(control.Height - (int)((float)control.Height * control.Height / Options.PageSize.Height) * (float)Options.PageSize.Height / control.Height)) * 2) ?? true)
                                            {
                                                Edited = true;

                                                UpdateStatus($"ノード「{node.Text}」を編集しました。");
                                            }
                                        };
                                        PictureEditor.Delete = () =>
                                        {
                                            ContentsTree.SelectedNode = node;
                                            ContentsTree.Focus();
                                            DeleteToolStripMenuItem.PerformClick();
                                        };
                                        PictureEditor.Initialize();
                                        control.Controls.Add(PictureEditor);
                                        PictureEditor.Select();
                                    }
                                    else if (node.Tag is TagControls.TextBoxTag textBoxTag)
                                    {
                                        CloseEditors();

                                        int nodeIndex = node.Index;
                                        parent.Nodes.Remove(node);

                                        UpdateImage(index);

                                        parent.Nodes.Insert(nodeIndex, node);
                                        ContentsTree.SelectedNode = node;

                                        string textBoxTagData = textBoxTag?.Data;
                                        TextBoxEditor.TextBoxTag = textBoxTag;
                                        TextBoxEditor.PageSize = Options.PageSize;
                                        TextBoxEditor.Box = control;
                                        TextBoxEditor.Finish = () =>
                                        {
                                            control.Controls.Remove(TextBoxEditor);

                                            UpdateImage(index);

                                            if (!textBoxTag?.Equals(new TagControls.TextBoxTag() { Data = textBoxTagData }, Math.Max(Math.Abs(control.Width - ((int)((float)control.Width * control.Width / Options.PageSize.Width) * (float)Options.PageSize.Width / control.Width)), Math.Abs(control.Height - (int)((float)control.Height * control.Height / Options.PageSize.Height) * (float)Options.PageSize.Height / control.Height)) * 2) ?? true)
                                            {
                                                Edited = true;

                                                UpdateStatus($"ノード「{node.Text}」を編集しました。");
                                            }
                                        };
                                        TextBoxEditor.Delete = () =>
                                        {
                                            ContentsTree.SelectedNode = node;
                                            ContentsTree.Focus();
                                            DeleteToolStripMenuItem.PerformClick();
                                        };
                                        TextBoxEditor.Initialize();
                                        control.Controls.Add(TextBoxEditor);
                                        TextBoxEditor.Select();
                                    }
                                }

                                return;
                            }
                        }

                        bool selectNode(TreeNode node)
                        {
                            if (node?.Tag is BaseTag tag)
                            {
                                if (tag.Rectangle.Contains(imagePoint))
                                {
                                    ContentsTree.Focus();
                                    ContentsTree.SelectedNode = node;
                                    return true;
                                }

                                foreach (TreeNode child in node.Nodes)
                                {
                                    if (selectNode(child))
                                    {
                                        return true;
                                    }
                                }
                            }

                            return false;
                        }

                        selectNode(ContentsTree.Nodes[index]);
                    }
                }
            }
        }

        private void CloseEditors()
        {
            ApplyContentsTextToTag();
            PictureEditor.Close();
            TextBoxEditor.Close();
        }

        private void MainPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
            {
                EditSelectedNode();
            }
        }

        private void MainPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                Control control = SelectedPictureBoxOfCursor;
                Point lastGlobalPoint = new Point();
                PointF imagePoint = new PointF();
                if (control != null)
                {
                    lastGlobalPoint = Cursor.Position;
                    imagePoint = PointToImage(control.PointToClient(lastGlobalPoint));
                }

                int value = ScaleTrackBar.Value + (int)((ScaleTrackBar.Maximum - ScaleTrackBar.Minimum) / 20.0f * e.Delta / 100.0f);
                ScaleTrackBar.Value = value < ScaleTrackBar.Minimum ? ScaleTrackBar.Minimum : value > ScaleTrackBar.Maximum ? ScaleTrackBar.Maximum : value;
                UpdateScale();

                if (control != null)
                {
                    Point clientPoint = PointToPictureBox(imagePoint);
                    MainPanel.SuspendDrawing();
                    MainPanel.ScrollBy(new Size(lastGlobalPoint.Sub(control.PointToScreen(clientPoint))));
                    MainPanel.ResumeDrawing();
                    Cursor.Position = control.PointToScreen(clientPoint);
                }
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                if (CheckCancel("開きますか？", "D&Dを完了させる前の確認"))
                {
                    return;
                }

                OpenFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (((string[])e.Data.GetData(DataFormats.FileDrop))[0].EndsWith(".hwh"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CheckCancel("終了しますか？", "終了する前の確認");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OptionForm.Save();
        }

        private string ReplaceRedPenToText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            string[] redPenList = (SelectedPageTag?.RedPenList ?? new List<string>()).ToArray();

            Regex regex = new Regex(@"<(-?\d+?-?)>", RegexOptions.Compiled);
            Match match = regex.Match(text);

            string result = string.Empty;

            int offset = 0;
            while (match.Success)
            {
                result += text.Substring(offset, match.Index - offset);

                int index = Math.Abs(match.Result("$1").TrimEnd('-').ParseTo(0)) - 1;
                if (index >= 0 && index < redPenList.Length)
                {
                    result += redPenList[index] ?? string.Empty;
                }
                else
                {
                    result += match.Value;
                }

                offset = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (offset < text.Length)
            {
                result += text.Substring(offset);
            }

            return result;
        }
    }
}
