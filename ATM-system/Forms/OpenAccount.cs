using ATMSimulation.Models;
using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class OpenAccountForm : Form
    {
        private AdminService adminService;
        private UserInfo userInfo;
        private string generatedCardNumber;
        private string verificationCode;

        public OpenAccountForm(AdminService service)
        {
            InitializeComponent();
            adminService = service;
            userInfo = new UserInfo();
            ApplyStyles();

            // 生成验证码（模拟发送）
            verificationCode = adminService.GenerateVerificationCode();
            lblVerificationCodeHint.Text = $"验证码已发送到手机（模拟）: {verificationCode}";
        }

        private void ApplyStyles()
        {
            UIStyleService.ApplyFormStyle(this);
            UIStyleService.ApplyLabelStyle(label1, true);
            UIStyleService.ApplyLabelStyle(label2);
            UIStyleService.ApplyLabelStyle(label3);
            UIStyleService.ApplyLabelStyle(label4);
            UIStyleService.ApplyLabelStyle(label5);
            UIStyleService.ApplyLabelStyle(lblVerificationCodeHint);
            UIStyleService.ApplyTextBoxStyle(txtFullName);
            UIStyleService.ApplyTextBoxStyle(txtIdCard);
            UIStyleService.ApplyTextBoxStyle(txtPhone);
            UIStyleService.ApplyTextBoxStyle(txtVerificationCode);
            UIStyleService.ApplyButtonStyle(btnNext, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnCancel, ButtonStyle.Secondary);
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!ValidateStep1())
                return;

            // 保存用户信息
            userInfo.FullName = txtFullName.Text.Trim();
            userInfo.IdCardNumber = txtIdCard.Text.Trim();
            userInfo.PhoneNumber = txtPhone.Text.Trim();
            userInfo.VerificationCode = txtVerificationCode.Text.Trim();

            // 验证验证码
            if (userInfo.VerificationCode != verificationCode)
            {
                MessageBox.Show("验证码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVerificationCode.Focus();
                return;
            }

            // 生成银行卡号
            generatedCardNumber = adminService.GenerateNewCardNumber();
            userInfo.CardNumber = generatedCardNumber;

            // 显示第二步：设置密码
            ShowStep2();
        }

        private bool ValidateStep1()
        {
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                MessageBox.Show("请输入开户者姓名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtIdCard.Text.Trim()))
            {
                MessageBox.Show("请输入身份证号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdCard.Focus();
                return false;
            }

            if (txtIdCard.Text.Trim().Length != 18)
            {
                MessageBox.Show("身份证号必须是18位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdCard.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("请输入手机号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }

            if (txtPhone.Text.Trim().Length != 11)
            {
                MessageBox.Show("手机号必须是11位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtVerificationCode.Text.Trim()))
            {
                MessageBox.Show("请输入验证码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVerificationCode.Focus();
                return false;
            }

            return true;
        }

        private void ShowStep2()
        {
            // 隐藏第一步控件
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            lblVerificationCodeHint.Visible = false;
            txtFullName.Visible = false;
            txtIdCard.Visible = false;
            txtPhone.Visible = false;
            txtVerificationCode.Visible = false;

            // 显示第二步控件
            lblCardNumber.Visible = true;
            lblCardNumberValue.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            txtPassword.Visible = true;
            txtConfirmPassword.Visible = true;
            btnCreate.Visible = true;

            // 更新标题和按钮
            label1.Text = "设置账户密码";
            lblCardNumberValue.Text = generatedCardNumber;
            btnNext.Visible = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            if (password.Length != 6)
            {
                MessageBox.Show("密码必须是6位数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("两次输入的密码不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            // 创建账户
            if (adminService.CreateNewAccount(userInfo, password))
            {
                MessageBox.Show($"账户创建成功！\n\n银行卡号: {generatedCardNumber}\n请妥善保管好您的银行卡",
                    "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("账户创建失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIdCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 身份证号只允许输入数字和X
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 'X' && e.KeyChar != 'x')
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 手机号只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVerificationCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 验证码只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 密码只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 确认密码只允许输入数字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}