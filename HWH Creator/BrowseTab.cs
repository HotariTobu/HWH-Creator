using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HWH_Creator
{
    public partial class BrowseTab : UserControl
    {
        public BrowseTab()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            MobileMode = true;
        }

        public string Url { get => Browser.Url.OriginalString; set => Browser.Url = new Uri(value); }
        public TabPage TabPage { get; set; }

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

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            TabPage.Dispose();
        }

        private bool MobileMode;
        private bool Navigating;

        private async void BrowseTab_SizeChanged(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                MobileMode = Width < Height;
            });
        }

        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (Navigating)
            {
                Navigating = false;
                return;
            }

            if (MobileMode)
            {
                e.Cancel = true;
                Navigating = true;
                Browser.Navigate(e.Url, null, null, "User-Agent:Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; " +
    "Microsoft; Lumia 950 XL Dual SIM) AppleWebKit/537.36 (KHTML, like Gecko) " +
    "Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.15063\r\n");
            }
        }
    }
}
