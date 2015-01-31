using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    public class CloneableStack<T> {
        private T[] data;
        private int at = 0;
        private int expandAmount = 5;

        public CloneableStack(int size) {
            data = new T[size];
        }

        protected CloneableStack(T[] data, int at, int expandAmount) {
            this.data = data;
            this.at = at;
            this.expandAmount = expandAmount;
        }

        public void Push(T item) {
            if (at >= data.Length) {
                ExpandData();
            }
            data[at] = item;
            at++;
        }

        public T Pop() {
            if (at > 0) {
                at--;
                return data[at];
            }
            throw new Exception("Stack is empty");
        }

        public T Peek() {
            return data[at-1];
        }

        public int Count() {
            return at;
        }

        public CloneableStack<T> Clone() {
            T[] _data = new T[data.Length];
            for (int i = 0; i < at; i++) {
                _data[i] = data[i];
            }

            return new CloneableStack<T>(_data, at, expandAmount);
        }

        private void ExpandData() {
            T[] _data = new T[data.Length + expandAmount];

            for (int i = 0; i < data.Length; i++) {
                _data[i] = data[i];
            }

            data = _data;
        }
    }
}
