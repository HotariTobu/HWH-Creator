namespace HWH_Creator
{
    partial class StartPage
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
            this.LinksFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.AddPageButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.DeletePathsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LinksFlowLayoutPanel
            // 
            this.LinksFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinksFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LinksFlowLayoutPanel.Location = new System.Drawing.Point(3, 32);
            this.LinksFlowLayoutPanel.Name = "LinksFlowLayoutPanel";
            this.LinksFlowLayoutPanel.Size = new System.Drawing.Size(534, 255);
            this.LinksFlowLayoutPanel.TabIndex = 0;
            // 
            // AddPageButton
            // 
            this.AddPageButton.Location = new System.Drawing.Point(109, 3);
            this.AddPageButton.Name = "AddPageButton";
            this.AddPageButton.Size = new System.Drawing.Size(100, 23);
            this.AddPageButton.TabIndex = 2;
            this.AddPageButton.Text = "ページを追加する";
            this.AddPageButton.UseVisualStyleBackColor = true;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(3, 3);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(100, 23);
            this.OpenFileButton.TabIndex = 2;
            this.OpenFileButton.Text = "ファイルを開く";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            // 
            // QuitButton
            // 
            this.QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitButton.FlatAppearance.BorderSize = 0;
            this.QuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QuitButton.Image = global::HWH_Creator.Properties.Resources.Quit;
            this.QuitButton.Location = new System.Drawing.Point(514, 3);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(23, 23);
            this.QuitButton.TabIndex = 4;
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // DeletePathsButton
            // 
            this.DeletePathsButton.Location = new System.Drawing.Point(215, 3);
            this.DeletePathsButton.Name = "DeletePathsButton";
            this.DeletePathsButton.Size = new System.Drawing.Size(100, 23);
            this.DeletePathsButton.TabIndex = 3;
            this.DeletePathsButton.Text = "履歴を削除する";
            this.DeletePathsButton.UseVisualStyleBackColor = true;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.DeletePathsButton);
            this.Controls.Add(this.AddPageButton);
            this.Controls.Add(this.LinksFlowLayoutPanel);
            this.Name = "StartPage";
            this.Size = new System.Drawing.Size(540, 290);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel LinksFlowLayoutPanel;
        public System.Windows.Forms.Button AddPageButton;
        public System.Windows.Forms.Button OpenFileButton;
        public System.Windows.Forms.Button QuitButton;
        public System.Windows.Forms.Button DeletePathsButton;
    }
}
