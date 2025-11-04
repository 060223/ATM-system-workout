namespace ATMSimulation.Forms
{
    partial class WithdrawForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkPrintReceipt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuick100;
        private System.Windows.Forms.Button btnQuick500;
        private System.Windows.Forms.Button btnQuick1000;
        private System.Windows.Forms.Button btnQuick2000;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkPrintReceipt = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuick100 = new System.Windows.Forms.Button();
            this.btnQuick500 = new System.Windows.Forms.Button();
            this.btnQuick1000 = new System.Windows.Forms.Button();
            this.btnQuick2000 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "取款金额";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(120, 29);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(150, 20);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.Location = new System.Drawing.Point(60, 180);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(100, 30);
            this.btnWithdraw.TabIndex = 2;
            this.btnWithdraw.Text = "确认取款";
            this.btnWithdraw.UseVisualStyleBackColor = true;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkPrintReceipt
            // 
            this.chkPrintReceipt.AutoSize = true;
            this.chkPrintReceipt.Location = new System.Drawing.Point(120, 150);
            this.chkPrintReceipt.Name = "chkPrintReceipt";
            this.chkPrintReceipt.Size = new System.Drawing.Size(74, 17);
            this.chkPrintReceipt.TabIndex = 4;
            this.chkPrintReceipt.Text = "打印回单";
            this.chkPrintReceipt.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "快速取款金额";
            // 
            // btnQuick100
            // 
            this.btnQuick100.Location = new System.Drawing.Point(120, 65);
            this.btnQuick100.Name = "btnQuick100";
            this.btnQuick100.Size = new System.Drawing.Size(60, 25);
            this.btnQuick100.TabIndex = 6;
            this.btnQuick100.Text = "100元";
            this.btnQuick100.UseVisualStyleBackColor = true;
            this.btnQuick100.Click += new System.EventHandler(this.btnQuick100_Click);
            // 
            // btnQuick500
            // 
            this.btnQuick500.Location = new System.Drawing.Point(190, 65);
            this.btnQuick500.Name = "btnQuick500";
            this.btnQuick500.Size = new System.Drawing.Size(60, 25);
            this.btnQuick500.TabIndex = 7;
            this.btnQuick500.Text = "500元";
            this.btnQuick500.UseVisualStyleBackColor = true;
            this.btnQuick500.Click += new System.EventHandler(this.btnQuick500_Click);
            // 
            // btnQuick1000
            // 
            this.btnQuick1000.Location = new System.Drawing.Point(120, 100);
            this.btnQuick1000.Name = "btnQuick1000";
            this.btnQuick1000.Size = new System.Drawing.Size(60, 25);
            this.btnQuick1000.TabIndex = 8;
            this.btnQuick1000.Text = "1000元";
            this.btnQuick1000.UseVisualStyleBackColor = true;
            this.btnQuick1000.Click += new System.EventHandler(this.btnQuick1000_Click);
            // 
            // btnQuick2000
            // 
            this.btnQuick2000.Location = new System.Drawing.Point(190, 100);
            this.btnQuick2000.Name = "btnQuick2000";
            this.btnQuick2000.Size = new System.Drawing.Size(60, 25);
            this.btnQuick2000.TabIndex = 9;
            this.btnQuick2000.Text = "2000元";
            this.btnQuick2000.UseVisualStyleBackColor = true;
            this.btnQuick2000.Click += new System.EventHandler(this.btnQuick2000_Click);
            // 
            // WithdrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 241);
            this.Controls.Add(this.btnQuick2000);
            this.Controls.Add(this.btnQuick1000);
            this.Controls.Add(this.btnQuick500);
            this.Controls.Add(this.btnQuick100);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkPrintReceipt);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WithdrawForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "取款";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}