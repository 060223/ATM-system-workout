using ATMSimulation.Services;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class MainForm : Form
    {
        private ATMService atmService;

        public MainForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
            UpdateBalance();
        }

        private void UpdateBalance()
        {
            var account = atmService.GetCurrentAccount();
            lblBalance.Text = $"余额: {account.Balance:C}";
            lblWelcome.Text = $"欢迎, {account.UserName}";
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm form = new WithdrawForm(atmService);
            form.ShowDialog();
            UpdateBalance();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            DepositForm form = new DepositForm(atmService);
            form.ShowDialog();
            UpdateBalance();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            TransferForm form = new TransferForm(atmService);
            form.ShowDialog();
            UpdateBalance();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryForm form = new QueryForm(atmService);
            form.ShowDialog();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm form = new ChangePasswordForm(atmService);
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            atmService.Logout();
            this.Close();
        }
    }
}