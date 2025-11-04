using ATMSimulation.Models;
using System;
using System.Windows.Forms;

namespace ATMSimulation.Services
{
    public class ATMService
    {
        private DataService dataService;
        private Account currentAccount;
        private string currentLocation = "北京"; // 当前ATM位置（仅用于登录异地检测）

        // 定义大额转账阈值（可根据需求调整）
        private const decimal LARGE_AMOUNT_THRESHOLD = 1000;

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

            // 登录时的异地检测
            if (account.Location != currentLocation && !string.IsNullOrEmpty(account.Location))
            {
                var result = MessageBox.Show($"检测到异地登录（上次位置：{account.Location}，当前ATM位置：{currentLocation}），是否继续？",
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

            // 基础金额校验
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

            // 目标账户校验
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

            // 核心：判断是否为“异地且大额”转账
            bool isRemoteTransfer = currentAccount.Location != targetAccount.Location; // 异地判断
            bool isLargeAmount = amount > LARGE_AMOUNT_THRESHOLD; // 大额判断

            // 1. 同时满足异地和大额：显示专门的合并提醒
            if (isRemoteTransfer && isLargeAmount)
            {
                var result = MessageBox.Show(
                    $"检测到异地大额转账风险！\n您的地点：{currentAccount.Location}，对方地点：{targetAccount.Location}\n转账金额：{amount}元（超过{LARGE_AMOUNT_THRESHOLD}元）\n是否继续？",
                    "异地大额转账警告",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error // 用更严重的图标强调风险
                );
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }
            // 2. 仅大额（非异地）：显示原有大额提醒
            else if (isLargeAmount)
            {
                var result = MessageBox.Show($"您正在进行大额转账{amount}元，是否继续？",
                    "大额转账确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }
            // 3. 仅异地（非大额）：显示原有异地提醒
            else if (isRemoteTransfer)
            {
                var result = MessageBox.Show(
                    $"检测到异地转账（您的地点：{currentAccount.Location}，对方地点：{targetAccount.Location}），是否继续？",
                    "异地转账提醒",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            // 执行转账
            currentAccount.Balance -= amount;
            targetAccount.Balance += amount;

            // 记录交易（补充异地/大额标记）
            string transferDesc = isRemoteTransfer && isLargeAmount
                ? $"向{targetCardNumber}（{targetAccount.Location}）异地大额转账{amount}元"
                : isRemoteTransfer
                    ? $"向{targetCardNumber}（{targetAccount.Location}）异地转账{amount}元"
                    : isLargeAmount
                        ? $"向{targetCardNumber}（{targetAccount.Location}）大额转账{amount}元"
                        : $"向{targetCardNumber}（{targetAccount.Location}）转账{amount}元";

            currentAccount.AddTransaction("Transfer", amount, targetCardNumber, transferDesc);
            targetAccount.AddTransaction("Transfer", amount, currentAccount.CardNumber,
                $"收到来自{currentAccount.CardNumber}（{currentAccount.Location}）的转账{amount}元");

            dataService.UpdateAccount(currentAccount);
            dataService.UpdateAccount(targetAccount);

            MessageBox.Show($"转账成功！向{targetCardNumber}（{targetAccount.Location}）转账{amount}元");
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