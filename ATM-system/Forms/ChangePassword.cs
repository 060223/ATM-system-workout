using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private ATMService atmService;

        public ChangePasswordForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            // 应用窗体样式
            UIStyleService.ApplyFormStyle(this);

            // 应用控件样式
            UIStyleService.ApplyLabelStyle(label1);
            UIStyleService.ApplyLabelStyle(label2);
            UIStyleService.ApplyLabelStyle(label3);
            UIStyleService.ApplyTextBoxStyle(txtCurrentPassword);
            UIStyleService.ApplyTextBoxStyle(txtNewPassword);
            UIStyleService.ApplyTextBoxStyle(txtConfirmPassword);
            UIStyleService.ApplyButtonStyle(btnChangePassword, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnCancel, ButtonStyle.Secondary);
            UIStyleService.ApplyGroupBoxStyle(groupBox1);

            // 设置背景
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // 验证输入
            if (string.IsNullOrEmpty(currentPassword))
            {
                MessageBox.Show("请输入当前密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("请输入新密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("请确认新密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPassword.Focus();
                return;
            }

            if (newPassword.Length != 6)
            {
                MessageBox.Show("密码必须为6位数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return;
            }

            // 需求一：检查新密码是否与当前密码相同
            if (newPassword == currentPassword)
            {
                MessageBox.Show("新密码不能与当前密码相同", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("新密码和确认密码不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            // 验证当前密码
            var account = atmService.GetCurrentAccount();
            if (account.Password != currentPassword)
            {
                MessageBox.Show("当前密码不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Focus();
                txtCurrentPassword.SelectAll();
                return;
            }

            // 执行密码修改
            if (atmService.ChangePassword(newPassword))
            {
                MessageBox.Show("密码修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("密码修改失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPassword.Focus();
            }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConfirmPassword.Focus();
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangePassword.PerformClick();
            }
        }
    }
}