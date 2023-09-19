namespace HWH_Creator.TagControls
{
    partial class PageControl
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
            this.TextBox = new HWH_Creator.MyTextBox();
            this.BCCheckBox = new System.Windows.Forms.CheckBox();
            this.DateCheckBox = new System.Windows.Forms.CheckBox();
            this.AboutCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TextBox.Location = new System.Drawing.Point(0, 0);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(465, 31);
            this.TextBox.TabIndex = 1;
            // 
            // BCCheckBox
            // 
            this.BCCheckBox.AutoSize = true;
            this.BCCheckBox.Location = new System.Drawing.Point(3, 37);
            this.BCCheckBox.Name = "BCCheckBox";
            this.BCCheckBox.Size = new System.Drawing.Size(112, 16);
            this.BCCheckBox.TabIndex = 2;
            this.BCCheckBox.Text = "紀元前表記をする";
            this.BCCheckBox.UseVisualStyleBackColor = true;
            // 
            // DateCheckBox
            // 
            this.DateCheckBox.AutoSize = true;
            this.DateCheckBox.Location = new System.Drawing.Point(3, 59);
            this.DateCheckBox.Name = "DateCheckBox";
            this.DateCheckBox.Size = new System.Drawing.Size(100, 16);
            this.DateCheckBox.TabIndex = 3;
            this.DateCheckBox.Text = "日付表記をする";
            this.DateCheckBox.UseVisualStyleBackColor = true;
            // 
            // AboutCheckBox
            // 
            this.AboutCheckBox.AutoSize = true;
            this.AboutCheckBox.Location = new System.Drawing.Point(3, 81);
            this.AboutCheckBox.Name = "AboutCheckBox";
            this.AboutCheckBox.Size = new System.Drawing.Size(115, 16);
            this.AboutCheckBox.TabIndex = 4;
            this.AboutCheckBox.Text = "あいまい表記をする";
            this.AboutCheckBox.UseVisualStyleBackColor = true;
            // 
            // PageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AboutCheckBox);
            this.Controls.Add(this.DateCheckBox);
            this.Controls.Add(this.BCCheckBox);
            this.Controls.Add(this.TextBox);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(465, 225);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal MyTextBox TextBox;
        internal System.Windows.Forms.CheckBox BCCheckBox;
        internal System.Windows.Forms.CheckBox DateCheckBox;
        internal System.Windows.Forms.CheckBox AboutCheckBox;
    }
}
