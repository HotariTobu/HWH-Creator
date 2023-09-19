namespace HWH_Creator
{
    partial class BrowseTab
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.BackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FowardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.IsWebBrowserContextMenuEnabled = false;
            this.Browser.Location = new System.Drawing.Point(0, 24);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Size = new System.Drawing.Size(233, 454);
            this.Browser.TabIndex = 2;
            this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackMenuItem,
            this.FowardMenuItem,
            this.CloseMenuItem,
            this.RefreshMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(233, 24);
            this.MenuBar.TabIndex = 3;
            // 
            // BackMenuItem
            // 
            this.BackMenuItem.Image = global::HWH_Creator.Properties.Resources.Undo;
            this.BackMenuItem.Name = "BackMenuItem";
            this.BackMenuItem.Size = new System.Drawing.Size(28, 20);
            this.BackMenuItem.Click += new System.EventHandler(this.BackMenuItem_Click);
            // 
            // FowardMenuItem
            // 
            this.FowardMenuItem.Image = global::HWH_Creator.Properties.Resources.Redo;
            this.FowardMenuItem.Name = "FowardMenuItem";
            this.FowardMenuItem.Size = new System.Drawing.Size(28, 20);
            this.FowardMenuItem.Click += new System.EventHandler(this.FowardMenuItem_Click);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseMenuItem.Image = global::HWH_Creator.Properties.Resources.Quit;
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(28, 20);
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // RefreshMenuItem
            // 
            this.RefreshMenuItem.Image = global::HWH_Creator.Properties.Resources.Reload;
            this.RefreshMenuItem.Name = "RefreshMenuItem";
            this.RefreshMenuItem.Size = new System.Drawing.Size(28, 20);
            this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // BrowseTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.MenuBar);
            this.Name = "BrowseTab";
            this.Size = new System.Drawing.Size(233, 478);
            this.SizeChanged += new System.EventHandler(this.BrowseTab_SizeChanged);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem BackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FowardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshMenuItem;
    }
}
