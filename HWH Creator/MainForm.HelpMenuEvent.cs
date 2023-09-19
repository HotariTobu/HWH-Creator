using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class MainForm
    {
        private void ReadmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("Readme.txt"))
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "Readme.txt",
                    };
                    process.Start();
                }
            }
        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string optionData = string.Join(",", OptionForm.OptionsPort);
            if (OptionForm.ShowDialog() == DialogResult.OK && !optionData.Equals(string.Join(",", OptionForm.OptionsPort)))
            {
                ApplyOptions();
            }
        }

        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.ShowDialog();
        }
    }
}
