using Complexity.Main;
using MathNet.Symbolics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    public class ExpressionD {
        //Instance variables
        Expression infix, postfix;

        #region Static Expression Management
        private static ArrayList expressions = new ArrayList();
        private static ArrayList symbols;
        private static Dictionary<string, FloatingPoint> symbolValues;

        static ExpressionD() {
            //Populate symbols with some values
            symbols = new ArrayList();
            symbols.Add(new SymbolD("time", () => Global.GetElapsedTime()));

            Recalculate();
        }

        /// <summary>
        /// Compiles all the current values of suymbols into a dictionay
        /// for use in evaluation.
        /// </summary>
        public static void Recalculate() {
            symbolValues = new Dictionary<string, FloatingPoint>();
            foreach (SymbolD symbol in symbols) {
                symbolValues[symbol.name] = symbol.GetValue();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr"></param>
        public ExpressionD(string expr) {
            expressions.Add(this);
            infix = Infix.ParseOrThrow(expr);
        }

        /// <summary>
        /// Converts the Infix expression to RPN in preperation for evaluation
        /// Currently does nothing as I'm not sure how to do this using Math.NET
        /// Symbolics. Need to figure this out because evaluating a postfix expr
        /// is faster than infix, O(nLogn) vs O(n).
        /// </summary>
        public void ConvertToRPN() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Eval() {
            return Evaluate.Evaluate(symbolValues, infix).RealValue;
        }

        /// <summary>
        /// Returns this expression in infix notation.
        /// </summary>
        /// <returns></returns>
        public string AsInfix() {
            return Infix.Print(infix);
        }
    }

    class SymbolD {
        public readonly string name;
        private double value;
        private Eval eval;

        public SymbolD(string name, Eval eval) {
            this.name = name;
            this.eval = eval;
        }

        public double GetValue() {
            return eval();
        }

        public delegate double Eval();
    }
}
