using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Complexity.Util;

namespace Complexity.Objects {
    /// <summary>
    /// Represent a 3 Dimentional Object that can be rendered.
    /// SimpleObject -> Does not recalculate, must be modified externally.
    /// ComplexObject -> Has a Recalculate method and can update itself
    /// </summary>
    public abstract class Object3 : Renderable {
        //Point Matrix, vertex by vertex information
        protected PointMatrix vertecies;
        protected MatrixD originalGeo;
        protected string name;

        /// <summary>
        /// 
        /// </summary>
        public Object3() {
            Init();
        }

        /// <summary>
        /// Set object attributes from a Dictionary. 
        /// </summary>
        /// <param name="args"></param>
        public virtual void SetAttributes(Dictionary<string, string> args) {
            if (args.ContainsKey("name")) {
                name = args["name"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Init() {
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void Recalculate();

        /// <summary>
        /// Need to perform recursive clone of vertecies PointMatrix
        /// </summary>
        /// <returns>a shallow copy of this object</returns>
        public virtual Object3 Clone() {
            PointMatrix newVerts = new PointMatrix(vertecies.ToArray());
            for (int i = 0; i < newVerts.ColumnCount; i++) {
                newVerts.Set(i, vertecies.Get(i).Clone());
            }

            Object3 result = (Object3)MemberwiseClone();
            result.SetVertecies(newVerts);
            return result;
        }

        public void SetVertecies(PointMatrix vertecies) {
            this.vertecies = vertecies;
        }

        /// <summary>
        /// Note, there is something called a VertexBuffer that could potentially
        /// be used to improve efficiency.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Converts a 2D double array into a PointMatrix
        /// </summary>
        /// <param name="geometry"></param>
        protected virtual void ConvertGeometry(double[,] geometry) {
            vertecies = new PointMatrix(geometry);
        }
    }
}
