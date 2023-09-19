using System;
using System.Drawing;
using System.Windows.Forms;
using SharedWinforms.Extension;

namespace HWH_Creator
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
