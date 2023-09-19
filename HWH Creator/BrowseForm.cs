using System;
using System.Windows.Forms;

namespace HWH_Creator
{
    public partial class BrowseForm : Form
    {
        public BrowseForm()
        {
            InitializeComponent();
        }

        public string Url { get => Browser.Url.OriginalString; set => Browser.Url = new Uri(value); }

        private void BackMenuItem_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }

        private void FowardMenuItem_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            Browser.Refresh();
        }

        private void BrowseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
