using ATMSimulation.Services;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class LoginForm : Form
    {
        private ATMService atmService;

        public LoginForm()
        {
            InitializeComponent();
            atmService = new ATMService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入卡号和密码");
                return;
            }

            if (atmService.Login(cardNumber, password))
            {
                this.Hide();
                MainForm mainForm = new MainForm(atmService);
                mainForm.ShowDialog();
                this.Show();
                txtPassword.Text = "";
                txtCardNumber.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}