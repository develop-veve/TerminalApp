using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TerminalApp_Shared.Utils
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// 数式が有効かを判定します。
        /// </summary>
        /// <param name="expression">検証する数式</param>
        /// <returns>有効なら true、それ以外は false</returns>
        public static bool IsValidExpression(string expression)
        {
            return !string.IsNullOrWhiteSpace(expression) && expression.Any(char.IsDigit);
        }

        /// <summary>
        /// 数式をトークンに分割します。
        /// </summary>
        /// <param name="expression">数式文字列</param>
        /// <returns>トークンのリスト</returns>
        public static List<string> Tokenize(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("数式が空または無効です。");

            var pattern = @"(\d+(\.\d+)?)|[+\−×÷]";
            var matches = Regex.Matches(expression, pattern);

            if (matches.Count == 0)
                throw new ArgumentException("数式が無効です。");

            return matches.Select(m => m.Value).ToList();
        }

        /// <summary>
        /// トークンリストが構文的に有効かを判定します。
        /// </summary>
        /// <param name="tokens">トークンのリスト</param>
        /// <returns>有効なら true、それ以外は false</returns>
        public static bool IsValidTokenSequence(List<string> tokens)
        {
            if (tokens == null || tokens.Count < 3)
                return false;

            // トークンが数値と演算子で交互に並んでいるかをチェック
            bool expectNumber = true;
            foreach (var token in tokens)
            {
                if (expectNumber && !double.TryParse(token, out _))
                    return false;

                if (!expectNumber && !"+−×÷".Contains(token))
                    return false;

                expectNumber = !expectNumber;
            }

            return !expectNumber; // 最後が数値で終わるべき
        }
    }
}
