using System;
using System.Collections.Generic;
using System.Linq;

namespace TerminalApp_Shared
{
    public static class CalculatorLogic
    {
        /// <summary>
        /// 数式を解析して結果を計算
        /// </summary>
        /// <param name="expression">計算式（例: "3 + 5 × 2"）</param>
        /// <returns>計算結果</returns>
        public static double EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("数式が無効です。");

            var tokens = TokenizeExpression(expression);
            var values = new Stack<double>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var number)) // 数値
                {
                    values.Push(number);
                }
                else if (IsOperator(token)) // 演算子
                {
                    while (operators.Count > 0 && HasHigherPrecedence(operators.Peek(), token))
                    {
                        var op = operators.Pop();
                        var right = values.Pop();
                        var left = values.Pop();
                        values.Push(ApplyOperator(op, left, right));
                    }
                    operators.Push(token);
                }
            }

            // 残りの演算を処理
            while (operators.Count > 0)
            {
                var op = operators.Pop();
                var right = values.Pop();
                var left = values.Pop();
                values.Push(ApplyOperator(op, left, right));
            }

            return values.Pop();
        }

        /// <summary>
        /// 数式をトークンに分割
        /// </summary>
        /// <param name="expression">数式文字列</param>
        /// <returns>トークンのリスト</returns>
        private static List<string> TokenizeExpression(string expression)
        {
            var tokens = new List<string>();
            var currentToken = "";

            foreach (var ch in expression)
            {
                if (char.IsDigit(ch) || ch == '.') // 数値または小数点
                {
                    currentToken += ch;
                }
                else if (IsOperator(ch.ToString()))
                {
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    tokens.Add(ch.ToString());
                }
                else if (ch == ' ') // 空白を無視
                {
                    continue;
                }
            }

            if (!string.IsNullOrEmpty(currentToken))
                tokens.Add(currentToken);

            return tokens;
        }

        /// <summary>
        /// 指定された文字が演算子かを判定
        /// </summary>
        private static bool IsOperator(string token)
        {
            return token == "+" || token == "−" || token == "×" || token == "÷";
        }

        /// <summary>
        /// 演算子の優先順位を判定
        /// </summary>
        private static bool HasHigherPrecedence(string op1, string op2)
        {
            if ((op1 == "×" || op1 == "÷") && (op2 == "+" || op2 == "−"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 演算子を適用して計算
        /// </summary>
        private static double ApplyOperator(string op, double left, double right)
        {
            return op switch
            {
                "+" => left + right,
                "−" => left - right,
                "×" => left * right,
                "÷" => right != 0 ? left / right : throw new DivideByZeroException("0で割ることはできません"),
                _ => throw new InvalidOperationException("無効な演算子です")
            };
        }
    }
}
