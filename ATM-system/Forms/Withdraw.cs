using ATMSimulation.Services;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class WithdrawForm : Form
    {
        private ATMService atmService;

        public WithdrawForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                bool printReceipt = chkPrintReceipt.Checked;

                if (atmService.Withdraw(amount, printReceipt))
                {
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入有效的金额");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuick100_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "100";
        }

        private void btnQuick500_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "500";
        }

        private void btnQuick1000_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "1000";
        }

        private void btnQuick2000_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "2000";
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字和小数点
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // 只允许一个小数点
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}