using ATMSimulation.Services;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class AdminMainForm : Form
    {
        private AdminService adminService;

        public AdminMainForm(AdminService service)
        {
            InitializeComponent();
            adminService = service;
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIStyleService.ApplyFormStyle(this);
            UIStyleService.ApplyButtonStyle(btnOpenAccount, ButtonStyle.Primary);
            UIStyleService.ApplyButtonStyle(btnCloseAccount, ButtonStyle.Warning);
            UIStyleService.ApplyButtonStyle(btnLogout, ButtonStyle.Danger);
            UIStyleService.ApplyLabelStyle(label1, true);
            this.BackColor = UIStyleService.LightColor;
            panelContainer.BackColor = Color.White;
        }

        private void btnOpenAccount_Click(object sender, EventArgs e)
        {
            // 打开申请账户窗体
            OpenAccountForm openAccountForm = new OpenAccountForm(adminService);
            openAccountForm.ShowDialog();
        }

        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            // 打开注销账户窗体
            CloseAccountForm closeAccountForm = new CloseAccountForm(adminService);
            closeAccountForm.ShowDialog();
        }

        

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
