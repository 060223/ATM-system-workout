using ATMSimulation.Models;
using ATMSimulation.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATMSimulation.Forms
{
    public partial class QueryForm : Form
    {
        private ATMService atmService;

        public QueryForm(ATMService service)
        {
            InitializeComponent();
            atmService = service;
            LoadAccountInfo();
            LoadTransactionHistory();
        }

        private void LoadAccountInfo()
        {
            var account = atmService.GetCurrentAccount();
            if (account != null)
            {
                lblCardNumber.Text = $"卡号: {account.CardNumber}";
                lblUserName.Text = $"用户名: {account.UserName}";
                lblBalance.Text = $"余额: {account.Balance:C}";
                lblLocation.Text = $"常用地点: {account.Location}";
                lblStatus.Text = $"状态: {(account.IsLocked ? "已锁定" : "正常")}";
            }
        }

        private void LoadTransactionHistory()
        {
            var account = atmService.GetCurrentAccount();
            if (account != null && account.Transactions != null)
            {
                // 按时间倒序排列，显示最近的交易
                var sortedTransactions = new List<Transaction>(account.Transactions);
                sortedTransactions.Sort((x, y) => y.Time.CompareTo(x.Time));

                listViewTransactions.Items.Clear();

                foreach (var transaction in sortedTransactions)
                {
                    var item = new ListViewItem(transaction.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(GetTransactionTypeText(transaction.Type));
                    item.SubItems.Add(transaction.Amount.ToString("C"));
                    item.SubItems.Add(transaction.TargetAccount ?? "-");
                    item.SubItems.Add(transaction.Description);

                    // 根据交易类型设置颜色
                    if (transaction.Type == "Deposit")
                    {
                        item.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (transaction.Type == "Withdraw")
                    {
                        item.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (transaction.Type == "Transfer")
                    {
                        item.ForeColor = System.Drawing.Color.Blue;
                    }

                    listViewTransactions.Items.Add(item);
                }

                lblTransactionCount.Text = $"共 {sortedTransactions.Count} 笔交易记录";
            }
        }

        private string GetTransactionTypeText(string type)
        {
            switch (type)
            {
                case "Deposit": return "存款";
                case "Withdraw": return "取款";
                case "Transfer": return "转账";
                default: return type;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 模拟打印功能
            MessageBox.Show("交易记录已发送到打印机", "打印提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 模拟导出功能
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
            saveFileDialog.Title = "导出交易记录";
            saveFileDialog.FileName = $"交易记录_{DateTime.Now:yyyyMMdd}.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show($"交易记录已导出到: {saveFileDialog.FileName}", "导出成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}