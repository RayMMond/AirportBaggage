namespace AirportBaggage
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.mouseLocationX = new System.Windows.Forms.ToolStripStatusLabel();
            this.mouseLocationY = new System.Windows.Forms.ToolStripStatusLabel();
            this.btAddBaggages = new System.Windows.Forms.Button();
            this.btStartSimulation = new System.Windows.Forms.Button();
            this.btAddBaggage = new System.Windows.Forms.Button();
            this.btConfig = new System.Windows.Forms.Button();
            this.tbDepartureFlight1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseLocationX,
            this.mouseLocationY});
            this.statusStrip.Location = new System.Drawing.Point(0, 599);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // mouseLocationX
            // 
            this.mouseLocationX.AutoSize = false;
            this.mouseLocationX.Name = "mouseLocationX";
            this.mouseLocationX.Size = new System.Drawing.Size(100, 17);
            // 
            // mouseLocationY
            // 
            this.mouseLocationY.AutoSize = false;
            this.mouseLocationY.Name = "mouseLocationY";
            this.mouseLocationY.Size = new System.Drawing.Size(100, 17);
            // 
            // btAddBaggages
            // 
            this.btAddBaggages.Location = new System.Drawing.Point(93, 573);
            this.btAddBaggages.Name = "btAddBaggages";
            this.btAddBaggages.Size = new System.Drawing.Size(95, 23);
            this.btAddBaggages.TabIndex = 2;
            this.btAddBaggages.Text = "开始添加包裹";
            this.btAddBaggages.UseVisualStyleBackColor = true;
            this.btAddBaggages.Click += new System.EventHandler(this.button1_Click);
            // 
            // btStartSimulation
            // 
            this.btStartSimulation.Location = new System.Drawing.Point(12, 573);
            this.btStartSimulation.Name = "btStartSimulation";
            this.btStartSimulation.Size = new System.Drawing.Size(75, 23);
            this.btStartSimulation.TabIndex = 3;
            this.btStartSimulation.Text = "开始模拟";
            this.btStartSimulation.UseVisualStyleBackColor = true;
            this.btStartSimulation.Click += new System.EventHandler(this.button2_Click);
            // 
            // btAddBaggage
            // 
            this.btAddBaggage.Location = new System.Drawing.Point(921, 573);
            this.btAddBaggage.Name = "btAddBaggage";
            this.btAddBaggage.Size = new System.Drawing.Size(75, 23);
            this.btAddBaggage.TabIndex = 4;
            this.btAddBaggage.Text = "自动添加";
            this.btAddBaggage.UseVisualStyleBackColor = true;
            this.btAddBaggage.Click += new System.EventHandler(this.button3_Click);
            // 
            // btConfig
            // 
            this.btConfig.Location = new System.Drawing.Point(194, 573);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(75, 23);
            this.btConfig.TabIndex = 5;
            this.btConfig.Text = "配置";
            this.btConfig.UseVisualStyleBackColor = true;
            this.btConfig.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbDepartureFlight1
            // 
            this.tbDepartureFlight1.Font = new System.Drawing.Font("宋体", 12F);
            this.tbDepartureFlight1.Location = new System.Drawing.Point(275, 462);
            this.tbDepartureFlight1.Name = "tbDepartureFlight1";
            this.tbDepartureFlight1.ReadOnly = true;
            this.tbDepartureFlight1.Size = new System.Drawing.Size(112, 26);
            this.tbDepartureFlight1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(181, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "出库航班：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDepartureFlight1);
            this.Controls.Add(this.btConfig);
            this.Controls.Add(this.btAddBaggage);
            this.Controls.Add(this.btStartSimulation);
            this.Controls.Add(this.btAddBaggages);
            this.Controls.Add(this.statusStrip);
            this.MaximumSize = new System.Drawing.Size(1024, 660);
            this.MinimumSize = new System.Drawing.Size(1024, 660);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机场早交行李系统仿真";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mouseLocationX;
        private System.Windows.Forms.ToolStripStatusLabel mouseLocationY;
        private System.Windows.Forms.Button btAddBaggages;
        private System.Windows.Forms.Button btStartSimulation;
        private System.Windows.Forms.Button btAddBaggage;
        private System.Windows.Forms.Button btConfig;
        private System.Windows.Forms.TextBox tbDepartureFlight1;
        private System.Windows.Forms.Label label1;
    }
}

