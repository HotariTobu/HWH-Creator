namespace HWH_Creator.TagControls
{
    partial class PeriodControl
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
            this.CenturyBox2 = new System.Windows.Forms.ComboBox();
            this.CenturyBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AboutCheckBox2 = new System.Windows.Forms.CheckBox();
            this.ACBDCheckBox2 = new System.Windows.Forms.CheckBox();
            this.AboutCheckBox1 = new System.Windows.Forms.CheckBox();
            this.ACBDCheckBox1 = new System.Windows.Forms.CheckBox();
            this.YearBox1 = new System.Windows.Forms.MaskedTextBox();
            this.YearBox2 = new System.Windows.Forms.MaskedTextBox();
            this.YearRedCheckBox1 = new System.Windows.Forms.CheckBox();
            this.YearRedCheckBox2 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DateBox1 = new System.Windows.Forms.MaskedTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.DateBox2 = new System.Windows.Forms.MaskedTextBox();
            this.TextBox = new HWH_Creator.MyTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CenturyBox2
            // 
            this.CenturyBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CenturyBox2.FormattingEnabled = true;
            this.CenturyBox2.Items.AddRange(new object[] {
            "年",
            "世紀"});
            this.CenturyBox2.Location = new System.Drawing.Point(105, 3);
            this.CenturyBox2.Name = "CenturyBox2";
            this.CenturyBox2.Size = new System.Drawing.Size(50, 20);
            this.CenturyBox2.TabIndex = 3;
            this.CenturyBox2.SelectedIndexChanged += new System.EventHandler(this.CenturyBox2_SelectedIndexChanged);
            // 
            // CenturyBox1
            // 
            this.CenturyBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CenturyBox1.FormattingEnabled = true;
            this.CenturyBox1.Items.AddRange(new object[] {
            "年",
            "世紀"});
            this.CenturyBox1.Location = new System.Drawing.Point(105, 3);
            this.CenturyBox1.Name = "CenturyBox1";
            this.CenturyBox1.Size = new System.Drawing.Size(50, 20);
            this.CenturyBox1.TabIndex = 3;
            this.CenturyBox1.SelectedIndexChanged += new System.EventHandler(this.CenturyBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "～";
            // 
            // AboutCheckBox2
            // 
            this.AboutCheckBox2.AutoSize = true;
            this.AboutCheckBox2.Location = new System.Drawing.Point(207, 3);
            this.AboutCheckBox2.Name = "AboutCheckBox2";
            this.AboutCheckBox2.Size = new System.Drawing.Size(42, 16);
            this.AboutCheckBox2.TabIndex = 5;
            this.AboutCheckBox2.Text = "ごろ";
            this.AboutCheckBox2.UseVisualStyleBackColor = true;
            // 
            // ACBDCheckBox2
            // 
            this.ACBDCheckBox2.AutoSize = true;
            this.ACBDCheckBox2.Location = new System.Drawing.Point(3, 3);
            this.ACBDCheckBox2.Name = "ACBDCheckBox2";
            this.ACBDCheckBox2.Size = new System.Drawing.Size(60, 16);
            this.ACBDCheckBox2.TabIndex = 1;
            this.ACBDCheckBox2.Text = "紀元前";
            this.ACBDCheckBox2.UseVisualStyleBackColor = true;
            // 
            // AboutCheckBox1
            // 
            this.AboutCheckBox1.AutoSize = true;
            this.AboutCheckBox1.Location = new System.Drawing.Point(207, 3);
            this.AboutCheckBox1.Name = "AboutCheckBox1";
            this.AboutCheckBox1.Size = new System.Drawing.Size(42, 16);
            this.AboutCheckBox1.TabIndex = 5;
            this.AboutCheckBox1.Text = "ごろ";
            this.AboutCheckBox1.UseVisualStyleBackColor = true;
            // 
            // ACBDCheckBox1
            // 
            this.ACBDCheckBox1.AutoSize = true;
            this.ACBDCheckBox1.Location = new System.Drawing.Point(3, 3);
            this.ACBDCheckBox1.Name = "ACBDCheckBox1";
            this.ACBDCheckBox1.Size = new System.Drawing.Size(60, 16);
            this.ACBDCheckBox1.TabIndex = 1;
            this.ACBDCheckBox1.Text = "紀元前";
            this.ACBDCheckBox1.UseVisualStyleBackColor = true;
            // 
            // YearBox1
            // 
            this.YearBox1.HidePromptOnLeave = true;
            this.YearBox1.Location = new System.Drawing.Point(69, 3);
            this.YearBox1.Mask = "9999";
            this.YearBox1.Name = "YearBox1";
            this.YearBox1.Size = new System.Drawing.Size(30, 19);
            this.YearBox1.TabIndex = 2;
            this.YearBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // YearBox2
            // 
            this.YearBox2.HidePromptOnLeave = true;
            this.YearBox2.Location = new System.Drawing.Point(69, 3);
            this.YearBox2.Mask = "9999";
            this.YearBox2.Name = "YearBox2";
            this.YearBox2.Size = new System.Drawing.Size(30, 19);
            this.YearBox2.TabIndex = 2;
            this.YearBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // YearRedCheckBox1
            // 
            this.YearRedCheckBox1.AutoSize = true;
            this.YearRedCheckBox1.Location = new System.Drawing.Point(255, 3);
            this.YearRedCheckBox1.Name = "YearRedCheckBox1";
            this.YearRedCheckBox1.Size = new System.Drawing.Size(76, 16);
            this.YearRedCheckBox1.TabIndex = 6;
            this.YearRedCheckBox1.Text = "赤色にする";
            this.YearRedCheckBox1.UseVisualStyleBackColor = true;
            // 
            // YearRedCheckBox2
            // 
            this.YearRedCheckBox2.AutoSize = true;
            this.YearRedCheckBox2.Location = new System.Drawing.Point(255, 3);
            this.YearRedCheckBox2.Name = "YearRedCheckBox2";
            this.YearRedCheckBox2.Size = new System.Drawing.Size(76, 16);
            this.YearRedCheckBox2.TabIndex = 6;
            this.YearRedCheckBox2.Text = "赤色にする";
            this.YearRedCheckBox2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.ACBDCheckBox1);
            this.flowLayoutPanel1.Controls.Add(this.YearBox1);
            this.flowLayoutPanel1.Controls.Add(this.CenturyBox1);
            this.flowLayoutPanel1.Controls.Add(this.DateBox1);
            this.flowLayoutPanel1.Controls.Add(this.AboutCheckBox1);
            this.flowLayoutPanel1.Controls.Add(this.YearRedCheckBox1);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(357, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // DateBox1
            // 
            this.DateBox1.HidePromptOnLeave = true;
            this.DateBox1.Location = new System.Drawing.Point(161, 3);
            this.DateBox1.Mask = "99/99";
            this.DateBox1.Name = "DateBox1";
            this.DateBox1.Size = new System.Drawing.Size(40, 19);
            this.DateBox1.TabIndex = 4;
            this.DateBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.ACBDCheckBox2);
            this.flowLayoutPanel2.Controls.Add(this.YearBox2);
            this.flowLayoutPanel2.Controls.Add(this.CenturyBox2);
            this.flowLayoutPanel2.Controls.Add(this.DateBox2);
            this.flowLayoutPanel2.Controls.Add(this.AboutCheckBox2);
            this.flowLayoutPanel2.Controls.Add(this.YearRedCheckBox2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(334, 26);
            this.flowLayoutPanel2.TabIndex = 2;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // DateBox2
            // 
            this.DateBox2.HidePromptOnLeave = true;
            this.DateBox2.Location = new System.Drawing.Point(161, 3);
            this.DateBox2.Mask = "99/99";
            this.DateBox2.Name = "DateBox2";
            this.DateBox2.Size = new System.Drawing.Size(40, 19);
            this.DateBox2.TabIndex = 4;
            this.DateBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBox
            // 
            this.TextBox.AcceptsReturn = true;
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox.Location = new System.Drawing.Point(0, 64);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox.Size = new System.Drawing.Size(461, 171);
            this.TextBox.TabIndex = 3;
            // 
            // PeriodControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "PeriodControl";
            this.Size = new System.Drawing.Size(461, 235);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox CenturyBox2;
        internal System.Windows.Forms.ComboBox CenturyBox1;
        internal System.Windows.Forms.CheckBox AboutCheckBox2;
        internal System.Windows.Forms.CheckBox ACBDCheckBox2;
        internal System.Windows.Forms.CheckBox AboutCheckBox1;
        internal System.Windows.Forms.CheckBox ACBDCheckBox1;
        internal MyTextBox TextBox;
        internal System.Windows.Forms.MaskedTextBox YearBox1;
        internal System.Windows.Forms.MaskedTextBox YearBox2;
        internal System.Windows.Forms.CheckBox YearRedCheckBox1;
        internal System.Windows.Forms.CheckBox YearRedCheckBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        internal System.Windows.Forms.MaskedTextBox DateBox1;
        internal System.Windows.Forms.MaskedTextBox DateBox2;
    }
}
