using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckCancel("新規作成しますか？", "新規作成する前の確認"))
            {
                return;
            }

            Reset();
        }

        private void OpenFile(string path)
        {
            if (CheckCancel("開きますか？", "開く前の確認確認"))
            {
                return;
            }

            if (Read(path))
            {
                AddPathToList(path);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(OpenFileDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private bool Save(bool newFile = false)
        {
            ApplyContentsTextToTag();
            if (string.IsNullOrWhiteSpace(FilePath) || newFile)
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = SaveFileDialog.FileName;
                    AddPathToList(FilePath);
                    return Write();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Write();
            }
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportFileDialog.FileName = string.Empty;
            if (ExportFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = ExportFileDialog.FileName;
                string fullName = Path.Combine(Directory.GetParent(path).FullName, Path.GetFileNameWithoutExtension(path));
                string extension = Path.GetExtension(path);
                ImageFormat imageFormat;
                switch (extension)
                {
                    case ".tif":
                        imageFormat = ImageFormat.Tiff;
                        break;
                    default:
                        imageFormat = ImageFormat.Png;
                        break;
                }

                int i = 0;
                foreach (Control control in MainPanel.Controls)
                {
                    if (control is PictureBox pictureBox)
                    {
                        UpdateImage(i, true);
                        pictureBox.Image.Save($"{fullName}_{ContentsTree.Nodes[i].Text}{extension}", imageFormat);
                        i++;
                    }
                }
            }

            UpdateStatus("エクスポートが完了しました。");
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
