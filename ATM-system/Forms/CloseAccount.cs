using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class CloseAccountForm : Form
    {
        private AdminService adminService;

        public CloseAccountForm(AdminService service)
        {
            InitializeComponent();
            adminService = service;
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIStyleService.ApplyFormStyle(this);
            UIStyleService.ApplyLabelStyle(label1, true);
            UIStyleService.ApplyLabelStyle(label2);
            UIStyleService.ApplyLabelStyle(label3);
            UIStyleService.ApplyTextBoxStyle(txtCardNumber);
            UIStyleService.ApplyTextBoxStyle(txtPassword);
            UIStyleService.ApplyButtonStyle(btnCloseAccount, ButtonStyle.Warning);
            UIStyleService.ApplyButtonStyle(btnCancel, ButtonStyle.Secondary);
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(cardNumber))
            {
                MessageBox.Show("请输入银行卡号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardNumber.Focus();
                return;
            }

            if (cardNumber.Length != 16)
            {
                MessageBox.Show("银行卡号必须是16位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardNumber.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            // 确认注销
            var result = MessageBox.Show("确定要注销这个账户吗？此操作不可撤销！",
                "确认注销", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (adminService.CloseAccount(cardNumber, password))
                {
                    MessageBox.Show("账户注销成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("账户注销失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void CloseAccountForm_Load(object sender, EventArgs e)
        {
            txtCardNumber.Focus();
        }
    }
}