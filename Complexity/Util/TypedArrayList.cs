using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexity.Util {
    public class TypedArrayList<T> {
        private ArrayList data;

        public TypedArrayList() {
            data = new ArrayList();
        }

        public TypedArrayList(int size) {
            data = new ArrayList(size);
        }

        public TypedArrayList(T[] data) {
            this.data = new ArrayList(data);
        }

        public TypedArrayList(ArrayList data) {
            this.data = data;
        }

        public void Add(T item) {
            data.Add(item);
        }

        public void Set(int index, T item) {
            data[index] = item;
        }

        public void SetFromArray(ArrayList data) {
            this.data = data;
        }

        public T Get(int index) {
            return (T)data[index];
        }

        public T Last() {
            return (T)data[data.Count - 1];
        }

        public void RemoveAt(int index) {
            data.RemoveAt(index);
        }

        public T[] ToArray() {
            return (T[])data.ToArray(typeof(T));
        }

        public int Count() {
            return data.Count;
        }
    }
}
