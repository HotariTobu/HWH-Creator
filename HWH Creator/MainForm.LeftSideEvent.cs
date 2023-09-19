using SharedWinforms.Extension;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void TreeNodeLabel_Leave(object sender, EventArgs e)
        {
            if (ContentsTree.SelectedNode is TreeNode node && !node.Text.Equals(TreeNodeLabel.Text))
            {
                node.Text = TreeNodeLabel.Text;

                Edited = true;

                UpdateStatus($"ノード「{node.Text}」の名前を変更しました。");
            }
        }

        private TagControls.PageTag lastSelectedPageTag;

        private void ContentsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeLabel.Text = e.Node.Text;
            UpdateContentsText();
            ContentsTree.Focus();

            TagControls.PageTag pageTag = SelectedPageTag;
            if (pageTag != lastSelectedPageTag)
            {
                UpdateRedPenList(-1);
                lastSelectedPageTag = pageTag;
            }

            if (Options.AutoScrollAfterNode)
            {
                Control control = MainPanel.Parent;
                Point globalPoint = control.PointToScreen(new Point(control.Width / 2, control.Height / 2));
                RectangleF rectangle = (e.Node.Tag as BaseTag)?.Rectangle ?? new RectangleF();
                Point target = SelectedPictureBoxOfIndex?.PointToScreen(PointToPictureBox(new PointF(rectangle.X + rectangle.Width / 2.0f, rectangle.Y + rectangle.Height / 2.0f))) ?? globalPoint;
                MainPanel.ScrollBy(new Size(globalPoint.Sub(target)));
            }
        }

        private void ContentsTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditSelectedNode();
        }

        private void ContentsTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Item is TreeNode node)
            {
                ContentsTree.DoDragDrop(node, DragDropEffects.All);
            }
        }

        private void ContentsTree_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                if (ContentsTree.GetNodeFromPoint(ContentsTree.PointToClient(new Point(e.X, e.Y))) is TreeNode parent && e.Data.GetData(typeof(TreeNode)) is TreeNode node && parent != node)
                {
                    int lastSelectedIndex = node.GetRootNode().Index;
                    bool moved = false;
                    if (parent.Level + 1 == node.Level)
                    {
                        node.Remove();
                        parent.Nodes.Add(node);
                        moved = true;
                    }
                    else if (parent.Level == node.Level)
                    {
                        int index = parent.Index;
                        if (parent.Parent == null)
                        {
                            if (parent.Level == 0)
                            {
                                node.Remove();
                                ContentsTree.Nodes.Insert(index, node);
                                moved = true;
                            }
                        }
                        else
                        {
                            node.Remove();
                            parent.Parent.Nodes.Insert(index, node);
                            moved = true;
                        }
                    }

                    if (moved)
                    {
                        ContentsTree.SelectedNode = node;
                        int currentSelectedIndex = SelectedIndex;

                        Edited = true;

                        UpdateImage(lastSelectedIndex);
                        UpdateImage(currentSelectedIndex);
                        UpdateStatus($"ノード「{node.Text}」を移動させました。");
                    }
                }
            }
        }

        private void ContentsTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void ContentsTree_DragOver(object sender, DragEventArgs e)
        {
            ContentsTree.AutoScroll();
        }

        private bool ContentsTextEdited;

        private void ContentsText_TextChanged(object sender, EventArgs e)
        {
            ContentsTextEdited = true;
        }

        private void ContentsText_Leave(object sender, EventArgs e)
        {
            ApplyContentsTextToTag();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            ApplyContentsTextToTag();
        }

        private void ApplyContentsTextToTag()
        {
            if (ContentsTextEdited && ContentsTree.SelectedNode?.Tag is BaseTag tag)
            {
                string tagText = tag.Text;
                tag.Text = ContentsText.Text;
                ReplaceTagText(tag);

                if (!tagText?.Equals(tag.Text) ?? true)
                {
                    Edited = true;

                    UpdateImage(SelectedIndex);
                }

                ContentsTextEdited = false;
            }
        }

        private void ReplaceTagText(BaseTag tag)
        {
            Regex regex = new Regex(@"<(\w+)>");
            Match match = regex.Match(tag.Text);

            string text = string.Empty;

            int offset = 0;
            while (match.Success)
            {
                text += tag.Text.Substring(offset, match.Index - offset);

                if (RedPenIndexOf(match.Result("$1")) is int index)
                {
                    if (index == -1)
                    {
                        text += match.Value;
                    }
                    else
                    {
                        text += $"<{index + 1}>";
                    }
                }

                offset = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (offset < tag.Text.Length)
            {
                text += tag.Text.Substring(offset);
            }

            tag.Text = text;
        }
    }
}
