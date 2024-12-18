using System;
using System.Collections.Generic;

namespace TerminalApp_Shared
{
    public abstract class CalculatorBase
    {
        /// <summary>
        /// 数式を解析して結果を計算する
        /// </summary>
        /// <param name="expression">計算式（例: "3 + 5 × 2"）</param>
        /// <returns>計算結果</returns>
        public abstract double EvaluateExpression(string expression);

        /// <summary>
        /// トークンに分割する
        /// </summary>
        protected abstract List<string> TokenizeExpression(string expression);

        /// <summary>
        /// 演算子を適用して結果を計算する
        /// </summary>
        protected abstract double ApplyOperator(string op, double left, double right);

        /// <summary>
        /// 演算子かどうかを判定
        /// </summary>
        protected abstract bool IsOperator(string token);

        /// <summary>
        /// 演算子の優先順位を判定
        /// </summary>
        protected abstract bool HasHigherPrecedence(string op1, string op2);
    }
}
