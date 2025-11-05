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
            ApplyStyles();

            // 设置默认测试账户，方便调试
            txtCardNumber.Text = "6222020000000002";
            txtPassword.Text = "123456";
        }

        private void ApplyStyles()
        {
            // 应用窗体样式
            UIStyleService.ApplyFormStyle(this);

            // 应用控件样式
            UIStyleService.ApplyLabelStyle(label1, true);
            UIStyleService.ApplyLabelStyle(label2);
            UIStyleService.ApplyLabelStyle(label3);
            UIStyleService.ApplyTextBoxStyle(txtCardNumber);
            UIStyleService.ApplyTextBoxStyle(txtPassword);
            UIStyleService.ApplyButtonStyle(btnLogin, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnExit, ButtonStyle.Secondary);

            // 设置背景
            this.BackColor = UIStyleService.LightColor;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(cardNumber))
            {
                MessageBox.Show("请输入卡号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardNumber.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            if (cardNumber.Length != 16)
            {
                MessageBox.Show("卡号必须是16位数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardNumber.Focus();
                return;
            }

            if (atmService.Login(cardNumber, password))
            {
                this.Hide();
                MainForm mainForm = new MainForm(atmService);
                mainForm.ShowDialog();
                this.Show();
                ResetForm();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗？", "确认退出",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void ResetForm()
        {
            txtPassword.Text = "";
            txtCardNumber.Focus();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtCardNumber.Focus();
        }
    }
}