using System.Drawing;
using System.Windows.Forms;

namespace ATMSimulation.Services
{
    // 先定义枚举
    public enum ButtonStyle
    {
        Primary,
        Success,
        Warning,
        Danger,
        Secondary
    }

    public static class UIStyleService
    {
        // 颜色定义
        public static Color PrimaryColor = Color.FromArgb(41, 128, 185);     // 蓝色
        public static Color SecondaryColor = Color.FromArgb(41, 128, 185);   // 浅蓝
        public static Color SuccessColor = Color.FromArgb(41, 128, 185);     // 绿色
        public static Color WarningColor = Color.FromArgb(41, 128, 185);     // 黄色
        public static Color DangerColor = Color.FromArgb(41, 128, 185);       // 红色
        public static Color LightColor = Color.FromArgb(41, 128, 185);      // 浅灰
        public static Color DarkColor = Color.FromArgb(52, 73, 94);          // 深蓝灰

        // 应用窗体样式
        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = LightColor;
            form.Font = new Font("微软雅黑", 9);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        // 应用按钮样式
        public static void ApplyButtonStyle(Button button, ButtonStyle style = ButtonStyle.Primary)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("微软雅黑", 9, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            switch (style)
            {
                case ButtonStyle.Primary:
                    button.BackColor = PrimaryColor;
                    button.ForeColor = Color.White;
                    break;
                case ButtonStyle.Success:
                    button.BackColor = SuccessColor;
                    button.ForeColor = Color.White;
                    break;
                case ButtonStyle.Warning:
                    button.BackColor = WarningColor;
                    button.ForeColor = Color.White;
                    break;
                case ButtonStyle.Danger:
                    button.BackColor = DangerColor;
                    button.ForeColor = Color.White;
                    break;
                case ButtonStyle.Secondary:
                    button.BackColor = SecondaryColor;
                    button.ForeColor = Color.White;
                    break;
            }

            // 鼠标悬停效果
            button.MouseEnter += (s, e) =>
            {
                var btn = s as Button;
                btn.BackColor = ControlPaint.Light(btn.BackColor, 0.2f);
            };

            button.MouseLeave += (s, e) =>
            {
                var btn = s as Button;
                switch (style)
                {
                    case ButtonStyle.Primary: btn.BackColor = PrimaryColor; break;
                    case ButtonStyle.Success: btn.BackColor = SuccessColor; break;
                    case ButtonStyle.Warning: btn.BackColor = WarningColor; break;
                    case ButtonStyle.Danger: btn.BackColor = DangerColor; break;
                    case ButtonStyle.Secondary: btn.BackColor = SecondaryColor; break;
                }
            };
        }

        // 应用标签样式
        public static void ApplyLabelStyle(Label label, bool isTitle = false)
        {
            if (isTitle)
            {
                label.Font = new Font("微软雅黑", 12, FontStyle.Bold);
                label.ForeColor = DarkColor;
            }
            else
            {
                label.Font = new Font("微软雅黑", 9);
                label.ForeColor = DarkColor;
            }
        }

        // 应用文本框样式
        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.Font = new Font("微软雅黑", 9);
        }

        // 应用分组框样式
        public static void ApplyGroupBoxStyle(GroupBox groupBox)
        {
            groupBox.Font = new Font("微软雅黑", 9, FontStyle.Bold);
            groupBox.ForeColor = DarkColor;
        }

        // 应用列表框样式
        public static void ApplyListViewStyle(ListView listView)
        {
            listView.Font = new Font("微软雅黑", 9);
            listView.BackColor = Color.White;
            listView.BorderStyle = BorderStyle.FixedSingle;
        }

        // 应用复选框样式
        public static void ApplyCheckBoxStyle(CheckBox checkBox)
        {
            checkBox.Font = new Font("微软雅黑", 9);
            checkBox.ForeColor = DarkColor;
        }
    }
}