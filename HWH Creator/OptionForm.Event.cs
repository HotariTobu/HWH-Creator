using SharedCSharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HWH_Creator
{
    partial class OptionForm
    {
        private void RedPenColorButton_Click(object sender, EventArgs e)
        {
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                RedPenColorButton.BackColor = ColorDialog.Color;
            }
        }

        private void SaveCharAddButton_Click(object sender, System.EventArgs e)
        {
            if (SaveCharBox.Text.Length == 1 && !SaveCharList.Items.Contains(SaveCharBox.Text))
            {
                SaveCharList.Items.Add(SaveCharBox.Text, true);
            }
            SaveCharBox.Clear();
        }

        private void SaveCharDeleteButton_Click(object sender, System.EventArgs e)
        {
            if (SaveCharList.SelectedIndex != -1)
            {
                SaveCharList.Items.Remove(SaveCharList.SelectedItem);
            }
        }
        private void FontButton_Click(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                FontDialog.Font = control?.Tag as Font;
                if (FontDialog.ShowDialog() == DialogResult.OK)
                {
                    control.Tag = FontDialog.Font;

                    try
                    {
                        FontPageNumericUpDown.Value = (decimal)(((FontPageButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                        FontPageNumberNumericUpDown.Value = (decimal)(((FontPageNumberButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                        FontHeadlineNumericUpDown.Value = (decimal)(((FontHeadlineButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                        FontTextBoxNumericUpDown.Value = (decimal)(((FontTextBoxButton.Tag as Font)?.Size - (FontTextButton.Tag as Font)?.Size) ?? 0);
                    }
                    catch (Exception ee)
                    {
                        FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, ee);
                    }
                }
            }
        }

        private float FontBaseSize => ((FontTextButton.Tag as Font)?.Size) ?? 0f;

        private void FontPageNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (FontPageButton.Tag is Font font)
            {
                FontPageButton.Tag = new Font(font.FontFamily, FontBaseSize + (float)FontPageNumericUpDown.Value, font.Style, font.Unit);
            }
        }

        private void FontPageNumberNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (FontPageNumberButton.Tag is Font font)
            {
                FontPageNumberButton.Tag = new Font(font.FontFamily, FontBaseSize + (float)FontPageNumberNumericUpDown.Value, font.Style, font.Unit);
            }
        }

        private void FontHeadlineNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (FontHeadlineButton.Tag is Font font)
            {
                FontHeadlineButton.Tag = new Font(font.FontFamily, FontBaseSize + (float)FontHeadlineNumericUpDown.Value, font.Style, font.Unit);
            }
        }

        private void FontTextBoxNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (FontTextBoxButton.Tag is Font font)
            {
                FontTextBoxButton.Tag = new Font(font.FontFamily, FontBaseSize + (float)FontTextBoxNumericUpDown.Value, font.Style, font.Unit);
            }
        }

        private void DPIComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void DPIComboBox_Leave(object sender, EventArgs e)
        {
            if (DPIComboBox.Text.Length == 0)
            {
                DPIComboBox.SelectedIndex = 0;
            }
        }

        private void ScaleMaxNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (ScaleMaxNumericUpDown.Value < ScaleMinNumericUpDown.Value)
            {
                ScaleMaxNumericUpDown.Value = ScaleMinNumericUpDown.Value;
            }
        }

        private void ScaleMinNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (ScaleMaxNumericUpDown.Value < ScaleMinNumericUpDown.Value)
            {
                ScaleMinNumericUpDown.Value = ScaleMaxNumericUpDown.Value;
            }
        }

        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                ApplyOptions();
            }
        }
    }
}
