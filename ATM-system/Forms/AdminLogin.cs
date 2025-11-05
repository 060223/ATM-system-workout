using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class AdminLoginForm : Form
    {
        private AdminService adminService;

        public AdminLoginForm()
        {
            InitializeComponent();
            adminService = new AdminService();
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIStyleService.ApplyFormStyle(this);
            UIStyleService.ApplyLabelStyle(label1, true);
            UIStyleService.ApplyLabelStyle(label2);
            UIStyleService.ApplyTextBoxStyle(txtPassword);
            UIStyleService.ApplyButtonStyle(btnLogin, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnBack, ButtonStyle.Secondary);
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入管理员密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            if (adminService.ValidateAdminPassword(password))
            {
                // 登录成功，跳转到管理员主菜单
                this.Hide();
                AdminMainForm adminMainForm = new AdminMainForm(adminService);
                adminMainForm.ShowDialog();
                this.Show();
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("管理员密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void AdminLoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}