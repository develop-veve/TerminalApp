using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerminalApp_Shared.Utils;

namespace TerminalApp_Shared
{
    /// <summary>
    /// 演算子の優先順位を示す列挙型
    /// </summary>
    public enum OperatorPrecedence
    {
        Low = 1,    // 低い優先順位（例: 加算、減算）
        High = 2    // 高い優先順位（例: 乗算、除算）
    }

    /// <summary>
    /// 標準的な計算機クラス
    /// </summary>
    public class Calculator : CalculatorBase
    {
        private readonly Dictionary<string, OperatorPrecedence> _operatorPrecedence;
        private readonly Dictionary<string, Func<double, double, double>> _operators;

        /// <summary>
        /// コンストラクタ
        /// 標準的な演算子（加算、減算、乗算、除算）を初期化します。
        /// </summary>
        public Calculator()
        {
            _operatorPrecedence = new Dictionary<string, OperatorPrecedence>
            {
                { "+", OperatorPrecedence.Low },
                { "−", OperatorPrecedence.Low },
                { "×", OperatorPrecedence.High },
                { "÷", OperatorPrecedence.High }
            };

            _operators = new Dictionary<string, Func<double, double, double>>
            {
                { "+", (a, b) => a + b },
                { "−", (a, b) => a - b },
                { "×", (a, b) => a * b },
                { "÷", (a, b) => b != 0 ? a / b : throw new DivideByZeroException("0で割ることはできません") }
            };
        }

        /// <summary>
        /// 数式を評価し、計算結果を返します。
        /// </summary>
        /// <param name="expression">計算式（例: "3 + 5 × 2"）</param>
        /// <returns>計算結果</returns>
        /// <exception cref="ArgumentException">数式が空、無効、不完全な場合にスローされます。</exception>
        public override double EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("数式が空または無効です。");

            Logger.Debug($"[入力] 数式: {expression}");

            if (!expression.Any(char.IsDigit))
                throw new ArgumentException("数式に数字が含まれていません。");

            var tokens = TokenizeExpression(expression);

            if (tokens.Count < 3)
                throw new ArgumentException("不完全な数式です。");

            double result = EvaluateTokens(tokens);

            return result;
        }

        /// <summary>
        /// 数式をトークン（要素）に分割します。
        /// </summary>
        protected override List<string> TokenizeExpression(string expression)
        {
            var pattern = @"(\d+(\.\d+)?)|[+\−×÷]";
            var tokens = System.Text.RegularExpressions.Regex.Matches(expression, pattern)
                .Select(m => m.Value)
                .ToList();

            return tokens;
        }

        /// <summary>
        /// 演算子がトークンかを判定
        /// </summary>
        protected override bool IsOperator(string token) => _operators.ContainsKey(token);

        /// <summary>
        /// 演算子の優先順位を比較します。
        /// </summary>
        protected override bool HasHigherPrecedence(string op1, string op2)
        {
            return _operatorPrecedence[op1] >= _operatorPrecedence[op2];
        }

        /// <summary>
        /// 演算子を適用し計算します。
        /// </summary>
        protected override double ApplyOperator(string op, double left, double right)
        {
            if (_operators.TryGetValue(op, out var operation))
            {
                return operation(left, right);
            }

            throw new InvalidOperationException($"無効な演算子です: {op}");
        }

        /// <summary>
        /// トークンを評価し、計算結果を返します。
        /// </summary>
        private double EvaluateTokens(List<string> tokens)
        {
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

            while (operators.Count > 0)
            {
                var op = operators.Pop();
                var right = values.Pop();
                var left = values.Pop();
                values.Push(ApplyOperator(op, left, right));
            }

            return values.Pop();
        }
    }
}
