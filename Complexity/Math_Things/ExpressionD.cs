using Complexity.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Complexity.Math_Things {
    public class ExpressionD {
        #region Static Expression MGMT
        private static readonly int LEFT_ASSOC = 0;
        private static readonly int RIGHT_ASSOC = 1;

        private static readonly string OPS_REGEX;
        private static readonly string VAR_REGEX = "[a-zA-Z_]+[0-9]*[a-zA-Z_]*";
        private static readonly string DEL_REGEX = "[\\(\\)]";
        private static Dictionary<string, Symbol> SYMBOLS, FUNCTIONS, OPERATORS;

        static ExpressionD() {
            SYMBOLS = new Dictionary<string, Symbol>() {
                {"time", new Symbol("time", false, 0, LEFT_ASSOC, 0, (a) => Global.GetElapsedTime())},
                {"pi", new Symbol("pi", false, 0, LEFT_ASSOC, 0, (a) => Math.PI)},
                {"e", new Symbol("e", false, 0, LEFT_ASSOC, 0, (a) => Math.E)}
            };

            FUNCTIONS = new Dictionary<string, Symbol>() {
                {"sin", new Symbol("sin", true, 1, LEFT_ASSOC, 10, (a) => Math.Sin(a[0]))},
                {"cos", new Symbol("cos", true, 1, LEFT_ASSOC, 10, (a) => Math.Cos(a[0]))},
                {"tan", new Symbol("tan", true, 1, LEFT_ASSOC, 10, (a) => Math.Tan(a[0]))},
                {"asin", new Symbol("asin", true, 1, LEFT_ASSOC, 10, (a) => Math.Asin(a[0]))},
                {"acos", new Symbol("acos", true, 1, LEFT_ASSOC, 10, (a) => Math.Acos(a[0]))},
                {"atan", new Symbol("atan", true, 1, LEFT_ASSOC, 10, (a) => Math.Atan(a[0]))},
                {"sinh", new Symbol("sinh", true, 1, LEFT_ASSOC, 10, (a) => Math.Sinh(a[0]))},
                {"cosh", new Symbol("cosh", true, 1, LEFT_ASSOC, 10, (a) => Math.Cosh(a[0]))},
                {"tanh", new Symbol("tanh", true, 1, LEFT_ASSOC, 10, (a) => Math.Tanh(a[0]))},
                {"sqrt", new Symbol("sqrt", true, 1, LEFT_ASSOC, 10, (a) => Math.Sqrt(a[0]))},
                {"log", new Symbol("log", true, 1, LEFT_ASSOC, 10, (a) => Math.Log(a[0]))},

                {"abs", new Symbol("abs", true, 1, LEFT_ASSOC, 10, (a) => Math.Abs(a[0]))},
                {"ceil", new Symbol("ceil", true, 1, LEFT_ASSOC, 10, (a) => Math.Ceiling(a[0]))},
                {"floor", new Symbol("floor", true, 1, LEFT_ASSOC, 10, (a) => Math.Floor(a[0]))},
                {"round", new Symbol("round", true, 1, LEFT_ASSOC, 10, (a) => Math.Round(a[0]))},
                {"sign", new Symbol("sign", true, 1, LEFT_ASSOC, 10, (a) => Math.Sign(a[0]))},
            };

            OPERATORS = new Dictionary<string, Symbol>() {
                {"+", new Symbol("+", true, 2, LEFT_ASSOC, 1, (a) => a[1] + a[0])},
                {"-", new Symbol("-", true, 2, LEFT_ASSOC, 1, (a) => a[1] - a[0])},
                {"*", new Symbol("*", true, 2, LEFT_ASSOC, 5, (a) => a[1] * a[0])},
                {"/", new Symbol("/", true, 2, LEFT_ASSOC, 5, (a) => a[1] / a[0])},
                {"%", new Symbol("%", true, 2, LEFT_ASSOC, 5, (a) => a[1] % a[0])},
                {"^", new Symbol("^", true, 2, RIGHT_ASSOC, 9, (a) => Math.Pow(a[1],a[0]))}
            };

            //Assemble operator regex
            OPS_REGEX = "";
            foreach (KeyValuePair<string, Symbol> entry in OPERATORS) {
                OPS_REGEX += Regex.Escape(entry.Key) + "|";
            }
            OPS_REGEX = OPS_REGEX.Substring(0, OPS_REGEX.Length - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddSymbol(string name, double value) {
            SYMBOLS.Add(name, new Symbol(name, value));
        }

        public static void SetSymbolValue(string name, double value) {
            SYMBOLS[name] = new Symbol(name, value);
        }

        public static double GetSymbolValue(string name) {
            return SYMBOLS[name].eval(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveSymbol(string name) {
            SYMBOLS.Remove(name);
        }

        #endregion

        private CloneableStack<Symbol> expression;
        private string infix;

        public ExpressionD(string infix) {
            this.infix = infix;
            expression = InfixToRPN(Parse(infix));
        }

        //Test if is an operator
        private bool IsOperator(string token) {
            return (OPERATORS.ContainsKey(token) || FUNCTIONS.ContainsKey(token));
        }

        /// <summary>
        /// Test associativity
        /// </summary>
        /// <param name="token">Token name</param>
        /// <param name="type">Associativity to test</param>
        /// <returns></returns>
        private bool IsAssociative(string token, int type) {
            if (!IsOperator(token)) {
                throw new Exception("Invalid token: " + token);
            }

            if (ToSymbol(token).assoc == type) {
                return true;
            }

            return false;
        }

        //Compare precedence of two operators
        private int CmpPrecedence(string token1, string token2) {
            if (!IsOperator(token1) || !IsOperator(token2)) {
                throw new Exception("Invalid tokens: " + token1 + " " + token2);
            }
            return ToSymbol(token1).precedence - ToSymbol(token2).precedence;
        }

        /// <summary>
        /// Converts an infix expression represented an array of strings to a
        /// corresponding RPN expression as a stack.
        /// </summary>
        /// <param name="inputTokens"></param>
        /// <returns></returns>
        private CloneableStack<Symbol> InfixToRPN(string[] inputTokens) {
            //ArrayList outList = new ArrayList();
            CloneableStack<Symbol> result = new CloneableStack<Symbol>(0);
            Stack<string> stack = new Stack<string>();

            //for all the input tokens read the next token
            foreach (string token in inputTokens) {
                if (IsOperator(token)) {
                    //If token is an operator
                    while (stack.Count != 0 && IsOperator(stack.Peek())) {
                        if ((IsAssociative(token, LEFT_ASSOC) && CmpPrecedence(token, stack.Peek()) <= 0)
                            || (IsAssociative(token, RIGHT_ASSOC) && CmpPrecedence(token, stack.Peek()) < 0)) {
                            result.Push(ToSymbol(stack.Pop()));
                            continue;
                        }
                        break;
                    }
                    //Push the new operator on the stack
                    stack.Push(token);
                } else if (token.Equals("(")) {
                    stack.Push(token);
                } else if (token.Equals(")")) {
                    while (stack.Count != 0 && !stack.Peek().Equals("(")) {
                        result.Push(ToSymbol(stack.Pop()));
                    }
                    stack.Pop();
                } else {
                    result.Push(ToSymbol(token));
                }
            }

            while (stack.Count != 0) {
                result.Push(ToSymbol(stack.Pop()));
            }

            CloneableStack<Symbol> actualResult = new CloneableStack<Symbol>(result.Count());
            while (result.Count() > 0) {
                actualResult.Push(result.Pop());
            }
            return actualResult;
        }

        public void SetInfix(string infix) {
            this.infix = infix;
            expression = InfixToRPN(Parse(infix));
        }

        public string[] Parse(string input) {
            //Trim whitepace, there has to be a better way
            input = input.Replace(" ", "");
            input = input.Replace("\t", "");
            input = input.Replace("\n", "");
            string[] tokens = Regex.Split(input, @"(" + VAR_REGEX + "|" + DEL_REGEX + "|" + OPS_REGEX + ")");

            //Convert and test
            ArrayList _tokens = new ArrayList();
            int parens = 0;
            foreach (string token in tokens) {
                if (IsDefined(token) || IsNumeric(token) || IsVariable(token)) {
                    _tokens.Add(token);
                } else if (token.CompareTo("(") == 0) {
                    _tokens.Add(token);
                    parens++;
                } else if (token.CompareTo(")") == 0) {
                    _tokens.Add(token);
                    parens--;
                } else if (token.CompareTo("") == 0) {
                } else {
                    throw new Exception("Undefined symbol " + token);
                }
            }

            if (parens != 0) {
                throw new Exception("Cannot compile expression, incorrect parethesis format");
            }

            return (string[])_tokens.ToArray(typeof(string));
        }

        /// <summary>
        /// Attempts to either look up the symbol or convert it to one
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private Symbol ToSymbol(string symbol) {
            if (SYMBOLS.ContainsKey(symbol)) {
                return SYMBOLS[symbol];
            } else if (OPERATORS.ContainsKey(symbol)) {
                return OPERATORS[symbol];
            } else if (FUNCTIONS.ContainsKey(symbol)) {
                return FUNCTIONS[symbol];
            } else if (IsNumeric(symbol)) {
                return new Symbol(double.Parse(symbol));
            } else if (IsVariable(symbol)) {
                return new Symbol(symbol);
            }

            throw new Exception("ToSymbol : " + symbol + " cannot be converted to a Symbol");
        }

        /// <summary>
        /// Tests if the given input can be pared to a double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsNumeric(string s) {
            try {
                double d = double.Parse(s);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        private bool IsVariable(string s) {
            if (s.CompareTo("") == 0) {
                return false;
            }
            return (Regex.Match(s, "^" + VAR_REGEX + "$") != null);
        }

        /// <summary>
        /// Tests if a given input is contained in any of the Symbol Dictionaries
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsDefined(string symbol) {
            return (SYMBOLS.ContainsKey(symbol)
                || OPERATORS.ContainsKey(symbol)
                || FUNCTIONS.ContainsKey(symbol));
        }

        /// <summary>
        /// Evaluates the expression
        /// </summary>
        /// <returns>The result of evaluation</returns>
        public double Evaluate() {
            //Create a clone of expression because we don't want to mess with it
            CloneableStack<Symbol> expr = expression.Clone();
            Stack<Symbol> tempStack = new Stack<Symbol>();
            double[] values;

            while (expr.Count() > 0) {
                if (!expr.Peek().isOperator) {
                    tempStack.Push(expr.Pop());
                } else {
                    //Pop values, tempStack should only be 0 op symbols
                    values = new double[expr.Peek().nOps];
                    for (int i = 0; i < values.Length; i++) {
                        values[i] = tempStack.Pop().eval(null);
                    }
                    tempStack.Push(new Symbol(expr.Pop().eval(values)));
                }
            }

            return tempStack.Pop().eval(null);
        }

        public string ToString() {
            return infix;
        }
    }


    public class Symbol {
        public readonly string name;
        public readonly bool isOperator;
        public readonly int nOps, assoc, precedence;
        public readonly Eval eval;

        /// <summary>
        /// This is used to create a dynamic variable who's value and/or existence 
        /// is not known until runtime
        /// </summary>
        /// <param name="name"></param>
        public Symbol(string name) {
            this.name = name;
            isOperator = false;
            nOps = 0;
            assoc = 0;
            precedence = 0;
            eval = (a) => ExpressionD.GetSymbolValue(name);
        }

        /// <summary>
        /// Simple constructor for numbers only
        /// </summary>
        /// <param name="value"></param>
        public Symbol(double value) {
            name = "" + value;
            isOperator = false;
            nOps = 0;
            assoc = 0;
            precedence = 0;
            eval = (a) => value;
        }

        /// <summary>
        /// Convience constructor for creating variables with constant value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Symbol(string name, double value) {
            this.name = name;
            isOperator = false;
            nOps = 0;
            assoc = 0;
            precedence = 0;
            eval = (a) => value;
        }

        /// <summary>
        /// General Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isOperator"></param>
        /// <param name="noOps"></param>
        /// <param name="assoc"></param>
        /// <param name="precedence"></param>
        /// <param name="eval"></param>
        public Symbol(string name, bool isOperator, int noOps, int assoc, int precedence, Eval eval) {
            this.name = name;
            this.isOperator = isOperator;
            this.nOps = noOps;
            this.assoc = assoc;
            this.precedence = precedence;
            this.eval = eval;
        }

        public delegate double Eval(double[] args);
    }
}
