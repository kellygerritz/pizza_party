using Complexity.Main;
using MathNet.Symbolics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    public class ExpressionC {
        //Instance variables
        Expression infix, postfix;

        #region Static Expression Management
        private static ArrayList expressions = new ArrayList();
        private static Dictionary<string, SymbolD> symbols;
        private static Dictionary<string, FloatingPoint> symbolValues;

        static ExpressionC() {
            //Populate symbols with some values
            symbols = new Dictionary<string, SymbolD>();
            symbols.Add("time", new SymbolD("time", () => Global.GetElapsedTime()));
            symbols.Add("pi", new SymbolD("pi", () => Math.PI));

            Recalculate();
        }

        /// <summary>
        /// Compiles all the current values of suymbols into a dictionay
        /// for use in evaluation.
        /// </summary>
        public static void Recalculate() {
            symbolValues = new Dictionary<string, FloatingPoint>();
            foreach (KeyValuePair<string, SymbolD> entry in symbols) {
                symbolValues[entry.Key] = entry.Value.GetValue();
            }
        }

        public static void AddSymbol(string name, SymbolD symbol) {
            symbols.Add(name, symbol);
            symbolValues.Add(name, symbol.GetValue());
        }

        public static void RemoveSymbol(string name) {
            symbols.Remove(name);
            symbolValues.Remove(name);
        }

        public static void SetSymbolValue(string name, double value) {
            symbolValues[name] = value;
        }

        public static double GetValue(string name) {
            if (symbolValues.ContainsKey(name)) {
                return symbolValues[name].RealValue;
            }
            throw new Exception("No such value " + name);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr"></param>
        public ExpressionC(string expr) {
            expressions.Add(this);
            infix = Infix.ParseOrThrow(expr);
        }

        /// <summary>
        /// Allows you to change this expression without creating a new one
        /// </summary>
        /// <param name="infix"></param>
        public void SetInfix(string infix) {
            this.infix = Infix.ParseOrThrow(infix);
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

    public class SymbolD {
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

        public void SetVal(Eval eval) {
            this.eval = eval;
        }

        public delegate double Eval();
    }
}
