namespace HWH_Creator.TagControls
{
    partial class BlockControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.IntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LineCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "間隔（行）";
            // 
            // IntervalNumericUpDown
            // 
            this.IntervalNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalNumericUpDown.Location = new System.Drawing.Point(58, 0);
            this.IntervalNumericUpDown.Name = "IntervalNumericUpDown";
            this.IntervalNumericUpDown.Size = new System.Drawing.Size(50, 19);
            this.IntervalNumericUpDown.TabIndex = 1;
            this.IntervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LineCheckBox
            // 
            this.LineCheckBox.AutoSize = true;
            this.LineCheckBox.Location = new System.Drawing.Point(3, 25);
            this.LineCheckBox.Name = "LineCheckBox";
            this.LineCheckBox.Size = new System.Drawing.Size(84, 16);
            this.LineCheckBox.TabIndex = 2;
            this.LineCheckBox.Text = "線でつなげる";
            this.LineCheckBox.UseVisualStyleBackColor = true;
            // 
            // BlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LineCheckBox);
            this.Controls.Add(this.IntervalNumericUpDown);
            this.Controls.Add(this.label1);
            this.Name = "BlockControl";
            this.Size = new System.Drawing.Size(108, 73);
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.NumericUpDown IntervalNumericUpDown;
        internal System.Windows.Forms.CheckBox LineCheckBox;
    }
}
