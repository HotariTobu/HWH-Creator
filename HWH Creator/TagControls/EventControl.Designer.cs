namespace HWH_Creator.TagControls
{
    partial class EventControl
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
            this.CenturyBox = new System.Windows.Forms.ComboBox();
            this.AboutCheckBox = new System.Windows.Forms.CheckBox();
            this.ACBDCheckBox = new System.Windows.Forms.CheckBox();
            this.YearBox = new System.Windows.Forms.MaskedTextBox();
            this.YearRedCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DateBox = new System.Windows.Forms.MaskedTextBox();
            this.TextBox = new HWH_Creator.MyTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CenturyBox
            // 
            this.CenturyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CenturyBox.FormattingEnabled = true;
            this.CenturyBox.Items.AddRange(new object[] {
            "年",
            "世紀"});
            this.CenturyBox.Location = new System.Drawing.Point(105, 3);
            this.CenturyBox.Name = "CenturyBox";
            this.CenturyBox.Size = new System.Drawing.Size(50, 20);
            this.CenturyBox.TabIndex = 3;
            this.CenturyBox.SelectedIndexChanged += new System.EventHandler(this.CenturyBox_SelectedIndexChanged);
            // 
            // AboutCheckBox
            // 
            this.AboutCheckBox.AutoSize = true;
            this.AboutCheckBox.Location = new System.Drawing.Point(207, 3);
            this.AboutCheckBox.Name = "AboutCheckBox";
            this.AboutCheckBox.Size = new System.Drawing.Size(42, 16);
            this.AboutCheckBox.TabIndex = 5;
            this.AboutCheckBox.Text = "ごろ";
            this.AboutCheckBox.UseVisualStyleBackColor = true;
            // 
            // ACBDCheckBox
            // 
            this.ACBDCheckBox.AutoSize = true;
            this.ACBDCheckBox.Location = new System.Drawing.Point(3, 3);
            this.ACBDCheckBox.Name = "ACBDCheckBox";
            this.ACBDCheckBox.Size = new System.Drawing.Size(60, 16);
            this.ACBDCheckBox.TabIndex = 1;
            this.ACBDCheckBox.Text = "紀元前";
            this.ACBDCheckBox.UseVisualStyleBackColor = true;
            // 
            // YearBox
            // 
            this.YearBox.HidePromptOnLeave = true;
            this.YearBox.Location = new System.Drawing.Point(69, 3);
            this.YearBox.Mask = "9999";
            this.YearBox.Name = "YearBox";
            this.YearBox.Size = new System.Drawing.Size(30, 19);
            this.YearBox.TabIndex = 2;
            this.YearBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // YearRedCheckBox
            // 
            this.YearRedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.YearRedCheckBox.AutoSize = true;
            this.YearRedCheckBox.Location = new System.Drawing.Point(255, 3);
            this.YearRedCheckBox.Name = "YearRedCheckBox";
            this.YearRedCheckBox.Size = new System.Drawing.Size(76, 16);
            this.YearRedCheckBox.TabIndex = 6;
            this.YearRedCheckBox.Text = "赤色にする";
            this.YearRedCheckBox.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.ACBDCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.YearBox);
            this.flowLayoutPanel1.Controls.Add(this.CenturyBox);
            this.flowLayoutPanel1.Controls.Add(this.DateBox);
            this.flowLayoutPanel1.Controls.Add(this.AboutCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.YearRedCheckBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(334, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // DateBox
            // 
            this.DateBox.HidePromptOnLeave = true;
            this.DateBox.Location = new System.Drawing.Point(161, 3);
            this.DateBox.Mask = "99/99";
            this.DateBox.Name = "DateBox";
            this.DateBox.Size = new System.Drawing.Size(40, 19);
            this.DateBox.TabIndex = 4;
            this.DateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBox
            // 
            this.TextBox.AcceptsReturn = true;
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox.Location = new System.Drawing.Point(0, 32);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox.Size = new System.Drawing.Size(448, 168);
            this.TextBox.TabIndex = 2;
            // 
            // EventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "EventControl";
            this.Size = new System.Drawing.Size(448, 200);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox CenturyBox;
        internal System.Windows.Forms.CheckBox AboutCheckBox;
        internal System.Windows.Forms.CheckBox ACBDCheckBox;
        internal MyTextBox TextBox;
        internal System.Windows.Forms.MaskedTextBox YearBox;
        internal System.Windows.Forms.CheckBox YearRedCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal System.Windows.Forms.MaskedTextBox DateBox;
    }
}
