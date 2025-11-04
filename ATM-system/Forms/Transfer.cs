using ATMSimulation.Services;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class TransferForm : Form
    {
        private ATMService atmService;

        public TransferForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string targetCard = txtTargetCard.Text.Trim();
            string amountText = txtAmount.Text.Trim();

            if (string.IsNullOrEmpty(targetCard) || string.IsNullOrEmpty(amountText))
            {
                MessageBox.Show("请输入目标卡号和金额");
                return;
            }

            try
            {
                decimal amount = decimal.Parse(amountText);

                if (atmService.Transfer(targetCard, amount))
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

        private void txtTargetCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}