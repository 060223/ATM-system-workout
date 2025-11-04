using ATMSimulation.Services;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class DepositForm : Form
    {
        private ATMService atmService;

        public DepositForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                bool printReceipt = chkPrintReceipt.Checked;

                if (atmService.Deposit(amount, printReceipt))
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

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}
