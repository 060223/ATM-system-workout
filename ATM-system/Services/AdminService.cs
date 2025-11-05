using ATMSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ATMSimulation.Services
{
    public class AdminService
    {
        private DataService dataService;
        private const string AdminPassword = "123456"; // 默认管理员密码
        private Random random = new Random();

        public AdminService()
        {
            dataService = new DataService();
        }

        // 验证管理员密码
        public bool ValidateAdminPassword(string password)
        {
            bool isValid = password == AdminPassword;



            return isValid;
        }

        // 生成新的银行卡号（16位，确保唯一）
        public string GenerateNewCardNumber()
        {
            string newCardNumber;
            bool isUnique;

            do
            {
                // 生成以622202开头的16位卡号（模拟真实银行卡号格式）
                string prefix = "622202";
                string randomPart = random.Next(1000000000).ToString("D10"); // 10位随机数
                newCardNumber = prefix + randomPart;

                // 检查是否已存在
                isUnique = dataService.GetAccount(newCardNumber) == null;
            }
            while (!isUnique);

            return newCardNumber;
        }

        // 生成随机密码（6位数字）
        public string GenerateRandomPassword()
        {
            return random.Next(100000, 999999).ToString();
        }

        // 生成手机验证码（6位数字）
        public string GenerateVerificationCode()
        {
            return random.Next(100000, 999999).ToString();
        }

        // 创建新账户
        public bool CreateNewAccount(UserInfo userInfo, string password)
        {

            // 创建新账户
            var newAccount = new Account
            {
                CardNumber = userInfo.CardNumber,
                Password = password,
                UserName = userInfo.FullName,
                Balance = 0, // 初始余额为0
                IsLocked = false,
                FailedAttempts = 0,
                Location = "未设置",
                Transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            Time = DateTime.Now,
                            Type = "AccountCreation",
                            Amount = 0,
                            Description = "账户开户"
                        }
                    }
            };

            // 获取所有账户
            var allAccounts = dataService.GetAllAccounts();
            allAccounts.Add(newAccount);

            // 保存数据
            dataService.SaveData();



            return true;
        }




        // 注销账户
        public bool CloseAccount(string cardNumber, string password)
        {


            var account = dataService.GetAccount(cardNumber);
            if (account == null)
            {
                MessageBox.Show("账户不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 验证密码
            if (account.Password != password)
            {
                MessageBox.Show("密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // 检查余额
            if (account.Balance > 0)
            {
                var result = MessageBox.Show($"账户仍有余额 {account.Balance:C}，确定要注销吗？",
                    "确认注销", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            // 从数据中移除账户
            var allAccounts = dataService.GetAllAccounts();
            var accountToRemove = allAccounts.FirstOrDefault(a => a.CardNumber == cardNumber);

            if (accountToRemove != null)
            {
                allAccounts.Remove(accountToRemove);
                dataService.SaveData();



                return true;
            }

            return false;
        }

    }
        }

        
        

        
              
           
        
   

