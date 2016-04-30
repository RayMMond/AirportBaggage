namespace CommonLib
{
    partial class OpaLayer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Loading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Loading
            // 
            this.pictureBox_Loading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_Loading.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Loading.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Loading.Name = "pictureBox_Loading";
            this.pictureBox_Loading.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Loading.TabIndex = 0;
            this.pictureBox_Loading.TabStop = false;
            // 
            // OpaLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBox_Loading);
            this.Name = "OpaLayer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Loading;
    }
}
