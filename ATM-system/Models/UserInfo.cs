using System;

namespace ATMSimulation.Models
{
    public class UserInfo
    {
        public string FullName { get; set; }           // 开户者姓名
        public string IdCardNumber { get; set; }       // 身份证号
        public string PhoneNumber { get; set; }        // 手机号
        public string VerificationCode { get; set; }   // 手机验证码
        public DateTime RegistrationTime { get; set; } // 注册时间
        public string CardNumber { get; set; }         // 银行卡号
        public string InitialPassword { get; set; }    // 初始密码

        public UserInfo()
        {
            RegistrationTime = DateTime.Now;
        }
    }
}