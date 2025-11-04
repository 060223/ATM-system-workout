using ATMSimulation.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ATMSimulation.Services
{
    public class ATMService
    {
        private DataService dataService;
        private Account currentAccount;
        private string currentLocation = "北京";

        public ATMService()
        {
            dataService = new DataService();
        }

        public bool Login(string cardNumber, string password)
        {
            var account = dataService.GetAccount(cardNumber);
            if (account == null)
            {
                MessageBox.Show("卡号不存在");
                return false;
            }

            if (account.IsLocked)
            {
                MessageBox.Show("账户已被锁定，请联系银行");
                return false;
            }

            if (account.Password != password)
            {
                account.FailedAttempts++;
                if (account.FailedAttempts >= 3)
                {
                    account.IsLocked = true;
                    MessageBox.Show("密码错误3次，账户已被锁定");
                }
                else
                {
                    MessageBox.Show($"密码错误，还剩{3 - account.FailedAttempts}次机会");
                }
                dataService.UpdateAccount(account);
                return false;
            }

            // 创新点1：异地操作检测
            if (account.Location != currentLocation && !string.IsNullOrEmpty(account.Location))
            {
                var result = MessageBox.Show($"检测到异地操作（上次位置：{account.Location}），是否继续？",
                    "安全提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            account.FailedAttempts = 0;
            account.Location = currentLocation;
            dataService.UpdateAccount(account);
            currentAccount = account;
            return true;
        }

        public bool Withdraw(decimal amount, bool printReceipt = false)
        {
            if (currentAccount == null) return false;

            if (amount <= 0)
            {
                MessageBox.Show("取款金额必须大于0");
                return false;
            }

            if (amount > currentAccount.Balance)
            {
                MessageBox.Show("余额不足");
                return false;
            }

            currentAccount.Balance -= amount;
            currentAccount.AddTransaction("Withdraw", amount, description: $"取款{amount}元");

            dataService.UpdateAccount(currentAccount);

            if (printReceipt)
            {
                MessageBox.Show($"取款成功！金额：{amount}元\n余额：{currentAccount.Balance}元\n请取走现金");
            }
            else
            {
                MessageBox.Show($"取款成功！金额：{amount}元");
            }

            return true;
        }

        public bool Deposit(decimal amount, bool printReceipt = false)
        {
            if (currentAccount == null || amount <= 0) return false;

            currentAccount.Balance += amount;
            currentAccount.AddTransaction("Deposit", amount, description: $"存款{amount}元");

            dataService.UpdateAccount(currentAccount);

            if (printReceipt)
            {
                MessageBox.Show($"存款成功！金额：{amount}元\n余额：{currentAccount.Balance}元");
            }
            else
            {
                MessageBox.Show($"存款成功！金额：{amount}元");
            }

            return true;
        }

        public bool Transfer(string targetCardNumber, decimal amount)
        {
            if (currentAccount == null) return false;

            // 创新点2：大额转账验证
            if (amount > 5000)
            {
                var result = MessageBox.Show($"您正在进行大额转账{amount}元，是否继续？",
                    "大额转账确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            if (amount <= 0)
            {
                MessageBox.Show("转账金额必须大于0");
                return false;
            }

            if (amount > currentAccount.Balance)
            {
                MessageBox.Show("余额不足");
                return false;
            }

            var targetAccount = dataService.GetAccount(targetCardNumber);
            if (targetAccount == null)
            {
                MessageBox.Show("目标账户不存在");
                return false;
            }

            if (targetAccount.CardNumber == currentAccount.CardNumber)
            {
                MessageBox.Show("不能向自己转账");
                return false;
            }

            // 执行转账
            currentAccount.Balance -= amount;
            targetAccount.Balance += amount;

            // 记录交易
            currentAccount.AddTransaction("Transfer", amount, targetCardNumber,
                $"向{targetCardNumber}转账{amount}元");
            targetAccount.AddTransaction("Transfer", amount, currentAccount.CardNumber,
                $"收到来自{currentAccount.CardNumber}的转账{amount}元");

            dataService.UpdateAccount(currentAccount);
            dataService.UpdateAccount(targetAccount);

            MessageBox.Show($"转账成功！向{targetCardNumber}转账{amount}元");
            return true;
        }

        public bool ChangePassword(string newPassword)
        {
            if (currentAccount == null) return false;

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length != 6)
            {
                MessageBox.Show("密码必须为6位数字");
                return false;
            }

            currentAccount.Password = newPassword;
            dataService.UpdateAccount(currentAccount);
            MessageBox.Show("密码修改成功！");
            return true;
        }

        public Account GetCurrentAccount()
        {
            return currentAccount;
        }

        public void Logout()
        {
            currentAccount = null;
        }
    }
}
