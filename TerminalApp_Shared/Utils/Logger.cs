using System;
using System.IO;
using System.Reflection;

namespace TerminalApp_Shared.Utils
{
    /// <summary>
    /// シンプルなログ出力クラス
    /// </summary>
    public static class Logger
    {
        // ログファイルのパスを静的に設定
        private static readonly string logFilePath = Path.Combine(AppContext.BaseDirectory, "log.txt");

        /// <summary>
        /// デバッグログを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        public static void Debug(string message)
        {
            WriteLog("DEBUG", message);
        }

        /// <summary>
        /// 情報ログを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// 警告ログを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        public static void Warn(string message)
        {
            WriteLog("WARN", message);
        }

        /// <summary>
        /// エラーログを出力します。
        /// </summary>
        /// <param name="message">出力するメッセージ</param>
        public static void Error(string message)
        {
            WriteLog("ERROR", message);
        }

        /// <summary>
        /// 実際のログ出力処理
        /// </summary>
        /// <param name="level">ログレベル</param>
        /// <param name="message">出力するメッセージ</param>
        private static void WriteLog(string level, string message)
        {
            try
            {
                var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                // ログファイルに追記する
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

                // デバッグ出力用（Visual Studio出力ウィンドウに表示）
                Console.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                // ログ出力でエラーが発生した場合
                Console.WriteLine($"ログ出力中にエラー: {ex.Message}");
            }
        }
    }
}
