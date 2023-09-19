namespace HWH_Creator.TagControls
{
    partial class TextBoxControl
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
            this.XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.InitializeButton = new System.Windows.Forms.Button();
            this.TextBox = new HWH_Creator.MyTextBox();
            this.AlignmentComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // XNumericUpDown
            // 
            this.XNumericUpDown.DecimalPlaces = 1;
            this.XNumericUpDown.Location = new System.Drawing.Point(46, 3);
            this.XNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.XNumericUpDown.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.XNumericUpDown.Name = "XNumericUpDown";
            this.XNumericUpDown.Size = new System.Drawing.Size(58, 19);
            this.XNumericUpDown.TabIndex = 1;
            this.XNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X座標：";
            // 
            // YNumericUpDown
            // 
            this.YNumericUpDown.DecimalPlaces = 1;
            this.YNumericUpDown.Location = new System.Drawing.Point(166, 3);
            this.YNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.YNumericUpDown.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.YNumericUpDown.Name = "YNumericUpDown";
            this.YNumericUpDown.Size = new System.Drawing.Size(58, 19);
            this.YNumericUpDown.TabIndex = 2;
            this.YNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y座標：";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "(px)";
            // 
            // InitializeButton
            // 
            this.InitializeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InitializeButton.Location = new System.Drawing.Point(368, 0);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 3;
            this.InitializeButton.Text = "初期化";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // TextBox
            // 
            this.TextBox.AcceptsReturn = true;
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox.Location = new System.Drawing.Point(0, 29);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox.Size = new System.Drawing.Size(524, 162);
            this.TextBox.TabIndex = 5;
            // 
            // AlignmentComboBox
            // 
            this.AlignmentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlignmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlignmentComboBox.FormattingEnabled = true;
            this.AlignmentComboBox.Items.AddRange(new object[] {
            "左寄せ",
            "中央揃え",
            "右寄せ"});
            this.AlignmentComboBox.Location = new System.Drawing.Point(449, 2);
            this.AlignmentComboBox.Name = "AlignmentComboBox";
            this.AlignmentComboBox.Size = new System.Drawing.Size(75, 20);
            this.AlignmentComboBox.TabIndex = 4;
            // 
            // TextBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AlignmentComboBox);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.YNumericUpDown);
            this.Controls.Add(this.XNumericUpDown);
            this.Name = "TextBoxControl";
            this.Size = new System.Drawing.Size(524, 191);
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown XNumericUpDown;
        internal System.Windows.Forms.NumericUpDown YNumericUpDown;
        private System.Windows.Forms.Button InitializeButton;
        internal MyTextBox TextBox;
        internal System.Windows.Forms.ComboBox AlignmentComboBox;
    }
}
