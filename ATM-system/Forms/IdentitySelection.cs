using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class IdentitySelectionForm : Form
    {
        public IdentitySelectionForm()
        {
            InitializeComponent();
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIStyleService.ApplyFormStyle(this);
            UIStyleService.ApplyButtonStyle(btnUser, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnAdmin, ButtonStyle.Success);
            UIStyleService.ApplyLabelStyle(label1, true);
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            // 跳转到普通用户登录界面
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // 跳转到管理员登录界面
            this.Hide();
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
