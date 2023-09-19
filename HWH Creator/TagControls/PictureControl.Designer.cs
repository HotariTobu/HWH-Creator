namespace HWH_Creator.TagControls
{
    partial class PictureControl
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
            this.PathBox = new System.Windows.Forms.TextBox();
            this.ReferenceButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.InitializeButton = new System.Windows.Forms.Button();
            this.TextBox = new HWH_Creator.MyTextBox();
            this.AlignmentComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PathBox
            // 
            this.PathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PathBox.Location = new System.Drawing.Point(0, 1);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(354, 20);
            this.PathBox.TabIndex = 1;
            this.PathBox.TextChanged += new System.EventHandler(this.PathBox_TextChanged);
            // 
            // ReferenceButton
            // 
            this.ReferenceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReferenceButton.Location = new System.Drawing.Point(360, 0);
            this.ReferenceButton.Name = "ReferenceButton";
            this.ReferenceButton.Size = new System.Drawing.Size(75, 23);
            this.ReferenceButton.TabIndex = 2;
            this.ReferenceButton.Text = "参照";
            this.ReferenceButton.UseVisualStyleBackColor = true;
            this.ReferenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.Location = new System.Drawing.Point(0, 130);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(435, 79);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 5;
            this.PictureBox.TabStop = false;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "画像ファイル|*.jpg;*.jpeg;*.png;*.tiff;*.tif;*.gif;*.bmp";
            // 
            // XNumericUpDown
            // 
            this.XNumericUpDown.DecimalPlaces = 1;
            this.XNumericUpDown.Location = new System.Drawing.Point(46, 5);
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
            this.label1.Location = new System.Drawing.Point(-2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X座標：";
            // 
            // YNumericUpDown
            // 
            this.YNumericUpDown.DecimalPlaces = 1;
            this.YNumericUpDown.Location = new System.Drawing.Point(166, 5);
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
            this.label2.Location = new System.Drawing.Point(118, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y座標：";
            // 
            // WidthNumericUpDown
            // 
            this.WidthNumericUpDown.DecimalPlaces = 1;
            this.WidthNumericUpDown.Location = new System.Drawing.Point(46, 30);
            this.WidthNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.WidthNumericUpDown.Name = "WidthNumericUpDown";
            this.WidthNumericUpDown.Size = new System.Drawing.Size(58, 19);
            this.WidthNumericUpDown.TabIndex = 3;
            this.WidthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "幅：";
            // 
            // HeightNumericUpDown
            // 
            this.HeightNumericUpDown.DecimalPlaces = 1;
            this.HeightNumericUpDown.Location = new System.Drawing.Point(166, 30);
            this.HeightNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.HeightNumericUpDown.Name = "HeightNumericUpDown";
            this.HeightNumericUpDown.Size = new System.Drawing.Size(58, 19);
            this.HeightNumericUpDown.TabIndex = 4;
            this.HeightNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "高さ：";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "(px)";
            // 
            // InitializeButton
            // 
            this.InitializeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InitializeButton.Location = new System.Drawing.Point(360, 29);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 4;
            this.InitializeButton.Text = "初期化";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // TextBox
            // 
            this.TextBox.AcceptsReturn = true;
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox.Location = new System.Drawing.Point(0, 84);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox.Size = new System.Drawing.Size(435, 40);
            this.TextBox.TabIndex = 6;
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
            this.AlignmentComboBox.Location = new System.Drawing.Point(360, 58);
            this.AlignmentComboBox.Name = "AlignmentComboBox";
            this.AlignmentComboBox.Size = new System.Drawing.Size(75, 20);
            this.AlignmentComboBox.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.XNumericUpDown);
            this.panel1.Controls.Add(this.YNumericUpDown);
            this.panel1.Controls.Add(this.WidthNumericUpDown);
            this.panel1.Controls.Add(this.HeightNumericUpDown);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 51);
            this.panel1.TabIndex = 3;
            // 
            // PictureControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AlignmentComboBox);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.ReferenceButton);
            this.Controls.Add(this.PathBox);
            this.Name = "PictureControl";
            this.Size = new System.Drawing.Size(435, 209);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PictureControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PictureControl_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Button ReferenceButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown XNumericUpDown;
        internal System.Windows.Forms.NumericUpDown YNumericUpDown;
        internal System.Windows.Forms.NumericUpDown WidthNumericUpDown;
        internal System.Windows.Forms.NumericUpDown HeightNumericUpDown;
        private System.Windows.Forms.Button InitializeButton;
        private System.Windows.Forms.PictureBox PictureBox;
        internal MyTextBox TextBox;
        internal System.Windows.Forms.ComboBox AlignmentComboBox;
        private System.Windows.Forms.Panel panel1;
    }
}
