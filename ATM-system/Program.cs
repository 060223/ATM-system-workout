

using ATMSimulation.Forms;
using System;
using System.Windows.Forms;

namespace ATMSimulation
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 启用应用程序的视觉样式
            Application.EnableVisualStyles();

            // 设置文本渲染兼容性
            Application.SetCompatibleTextRenderingDefault(false);

            // 配置应用程序级别的异常处理
            ConfigureExceptionHandling();

            // 启动身份选择窗体
            Application.Run(new IdentitySelectionForm());
        }

        /// <summary>
        /// 配置全局异常处理
        /// </summary>
        private static void ConfigureExceptionHandling()
        {
            // 处理UI线程异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 处理UI线程异常
            Application.ThreadException += (sender, e) =>
            {
                HandleException(e.Exception);
            };

            // 处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                HandleException(e.ExceptionObject as Exception);
            };
        }

        /// <summary>
        /// 统一处理异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        private static void HandleException(Exception ex)
        {
            if (ex != null)
            {
                string errorMessage = $"发生未处理的异常：\n\n{ex.Message}\n\n" +
                                    $"异常类型：{ex.GetType().Name}\n" +
                                    $"堆栈跟踪：\n{ex.StackTrace}";

                MessageBox.Show(errorMessage, "系统错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 记录到日志文件（简单实现）
                try
                {
                    string logPath = "Data/error.log";
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(logPath));
                    System.IO.File.AppendAllText(logPath,
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}\n\n");
                }
                catch
                {
                    // 忽略日志写入错误
                }
            }
            else
            {
                MessageBox.Show("发生未知错误", "系统错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}