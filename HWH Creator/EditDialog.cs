using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace HWH_Creator
{
    public partial class EditDialog : Form
    {
        public EditDialog()
        {
            InitializeComponent();
        }

        public new BaseTag Tag { get; set; }

        private void EditDialog_Load(object sender, EventArgs e)
        {
            if (Tag == null)
            {
                Close();
            }
            else
            {
                Text = Tag?.Name ?? string.Empty;
                TextLabel.Text = Tag?.DialogText ?? string.Empty;
                BasePanel.Controls.Add(Tag?.InitializeControl());
            }
        }

        private void EditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && (!Tag?.ApplyContents() ?? false))
            {
                e.Cancel = true;
                return;
            }

            Text = "EditDialog";
            TextLabel.ResetText();
            BasePanel.Controls.Clear();
        }

        private void EditDialog_Shown(object sender, EventArgs e)
        {
            if (BasePanel.Controls.Count > 0)
            {
                foreach (Control control in BasePanel.Controls[0].Controls)
                {
                    if (control is MyTextBox)
                    {
                        control.Focus();
                        break;
                    }
                }
            }
        }
    }
}
