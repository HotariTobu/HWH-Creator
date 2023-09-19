using SharedWinforms.Extension;
using System.Drawing;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private string FilePath;

        private readonly OptionForm OptionForm;
        private readonly AboutBox AboutBox;
        private readonly EditDialog EditDialog;
        private readonly PictureEditor PictureEditor;
        private readonly TextBoxEditor TextBoxEditor;

        private OptionForm.Context Options;

        private readonly int[] NodeCounts;

        private Pen BlackPen;
        private Brush BlackBrush;
        private Brush RedBrush;

        private bool _Edited;
        private bool Edited
        {
            get => _Edited;

            set
            {
                if (_Edited = value && !string.IsNullOrWhiteSpace(FilePath))
                {
                    if (!Text.StartsWith("*"))
                    {
                        Text = Text.Insert(0, "*");
                    }

                    SaveToolStripMenuItem.Enabled = true;
                }
                else
                {
                    if (Text.StartsWith("*"))
                    {
                        Text = Text.Substring(1);
                    }

                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private TreeNode SelectedRootNode { get => ContentsTree.SelectedNode?.GetRootNode(); }

        private TagControls.PageTag SelectedPageTag { get => SelectedRootNode?.Tag as TagControls.PageTag; }

        private int SelectedIndex { get => SelectedRootNode?.Index ?? -1; }

        private Control SelectedPictureBoxOfIndex
        {
            get
            {
                int index = SelectedIndex;
                if (index >= 0 && index < MainPanel.Controls.Count)
                {
                    return MainPanel.Controls[index];
                }
                return null;
            }
        }

        private Control SelectedPictureBoxOfCursor { get => MainPanel.GetChildAtPoint(MainPanel.PointToClient(Cursor.Position)); }
    }
}
