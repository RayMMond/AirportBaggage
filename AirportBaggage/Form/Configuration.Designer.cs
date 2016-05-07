namespace AirportBaggage
{
    partial class Configuration
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btAddFlight = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btSetFlight = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFlightBaggageCount = new System.Windows.Forms.TextBox();
            this.btEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("宋体", 12F);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(55, 68);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(214, 180);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "航班列表：";
            // 
            // btSave
            // 
            this.btSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btSave.Location = new System.Drawing.Point(610, 390);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(104, 32);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btCancel.Location = new System.Drawing.Point(610, 436);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(104, 32);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("宋体", 12F);
            this.tbTime.Location = new System.Drawing.Point(55, 310);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(214, 26);
            this.tbTime.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(29, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "时间：";
            // 
            // btAddFlight
            // 
            this.btAddFlight.Font = new System.Drawing.Font("宋体", 12F);
            this.btAddFlight.Location = new System.Drawing.Point(301, 68);
            this.btAddFlight.Name = "btAddFlight";
            this.btAddFlight.Size = new System.Drawing.Size(144, 32);
            this.btAddFlight.TabIndex = 6;
            this.btAddFlight.Text = "新增";
            this.btAddFlight.UseVisualStyleBackColor = true;
            this.btAddFlight.Click += new System.EventHandler(this.btAddFlight_Click);
            // 
            // btDelete
            // 
            this.btDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.btDelete.Location = new System.Drawing.Point(301, 144);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(144, 32);
            this.btDelete.TabIndex = 8;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btSetFlight
            // 
            this.btSetFlight.Font = new System.Drawing.Font("宋体", 12F);
            this.btSetFlight.Location = new System.Drawing.Point(301, 216);
            this.btSetFlight.Name = "btSetFlight";
            this.btSetFlight.Size = new System.Drawing.Size(144, 32);
            this.btSetFlight.TabIndex = 9;
            this.btSetFlight.Text = "设置为当前出库";
            this.btSetFlight.UseVisualStyleBackColor = true;
            this.btSetFlight.Click += new System.EventHandler(this.btSetFlight_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(29, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "数量：";
            // 
            // tbFlightBaggageCount
            // 
            this.tbFlightBaggageCount.Font = new System.Drawing.Font("宋体", 12F);
            this.tbFlightBaggageCount.Location = new System.Drawing.Point(55, 377);
            this.tbFlightBaggageCount.Name = "tbFlightBaggageCount";
            this.tbFlightBaggageCount.Size = new System.Drawing.Size(214, 26);
            this.tbFlightBaggageCount.TabIndex = 10;
            this.tbFlightBaggageCount.TextChanged += new System.EventHandler(this.tbFlightBaggageCount_TextChanged);
            // 
            // btEdit
            // 
            this.btEdit.Font = new System.Drawing.Font("宋体", 12F);
            this.btEdit.Location = new System.Drawing.Point(301, 106);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(144, 32);
            this.btEdit.TabIndex = 12;
            this.btEdit.Text = "编辑";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // Configuration
            // 
            this.AcceptButton = this.btSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 480);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFlightBaggageCount);
            this.Controls.Add(this.btSetFlight);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAddFlight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Name = "Configuration";
            this.Text = "配置";
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAddFlight;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btSetFlight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFlightBaggageCount;
        private System.Windows.Forms.Button btEdit;
    }
}