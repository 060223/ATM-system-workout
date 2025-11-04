using ATMSimulation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// 使用别名来避免命名冲突
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace ATMSimulation.Services
{
    public class DataService
    {
        private List<Account> accounts;
        private string dataFilePath = "Data/accounts.json";

        public DataService()
        {
            // 确保Data目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(dataFilePath));
            LoadData();
            if (accounts.Count == 0)
            {
                InitializeSampleData();
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(dataFilePath))
                {
                    string json = File.ReadAllText(dataFilePath);
                    accounts = JsonConvert.DeserializeObject<List<Account>>(json) ?? new List<Account>();
                }
                else
                {
                    accounts = new List<Account>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败: {ex.Message}");
                accounts = new List<Account>();
            }
        }

        public void SaveData()
        {
            try
            {
                // 使用别名
                string json = JsonConvert.SerializeObject(accounts, JsonFormatting.Indented);
                File.WriteAllText(dataFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存数据失败: {ex.Message}");
            }
        }

        private void InitializeSampleData()
        {
            var random = new Random();
            for (int i = 1; i <= 20; i++)
            {
                // 地区分配逻辑：1-6→北京，7-13→上海，14-20→广东
                string location = i <= 6 ? "北京" :
                                 (i <= 13 ? "上海" : "广东");

                var account = new Account
                {
                    CardNumber = $"622202000000{i:0000}",
                    Password = "123456",
                    UserName = $"用户{i}",
                    Balance = 5000 + i * 100,
                    IsLocked = false,
                    FailedAttempts = 0,
                    Location = location // 应用分配的地区
                };

                // 添加一些初始交易记录
                account.AddTransaction("Deposit", account.Balance, description: "初始存款");
                if (i % 3 == 0)
                {
                    account.AddTransaction("Withdraw", 200, description: "取款");
                    account.Balance -= 200;
                }

                accounts.Add(account);
            }
            SaveData();
        }

        public Account GetAccount(string cardNumber)
        {
            return accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
        }

        public bool UpdateAccount(Account account)
        {
            var existing = GetAccount(account.CardNumber);
            if (existing != null)
            {
                var index = accounts.IndexOf(existing);
                accounts[index] = account;
                SaveData();
                return true;
            }
            return false;
        }

        public List<Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}