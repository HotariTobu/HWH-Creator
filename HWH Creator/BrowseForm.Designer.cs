﻿namespace HWH_Creator
{
    partial class BrowseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.BackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FowardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Browser.Size = new System.Drawing.Size(800, 426);
            this.Browser.TabIndex = 0;
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackMenuItem,
            this.FowardMenuItem,
            this.RefreshMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(800, 24);
            this.MenuBar.TabIndex = 1;
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
            // RefreshMenuItem
            // 
            this.RefreshMenuItem.Image = global::HWH_Creator.Properties.Resources.Reload;
            this.RefreshMenuItem.Name = "RefreshMenuItem";
            this.RefreshMenuItem.Size = new System.Drawing.Size(28, 20);
            this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // BrowseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.MenuBar);
            this.MainMenuStrip = this.MenuBar;
            this.Name = "BrowseForm";
            this.Text = "BrowseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowseForm_FormClosed);
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
        private System.Windows.Forms.ToolStripMenuItem RefreshMenuItem;
    }
}