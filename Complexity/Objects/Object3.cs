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
    /// Represent a 3 Dimentional Object that can be rendered
    /// </summary>
    public abstract class Object3 : Renderable {
        protected double[] geometry;
        protected double[] vertexColor;

        //These get rewitten for drawing and calculations.
        //rot = how it rotates
        //trans = how it moves
        //origin = center of the object
        //position = where the origin is
        protected VectorExpr rot, trans, origin, color;
        protected double[] position;
        protected ExpressionD scale;

        //These store the original values, everything is calculated from these
        protected MatrixD geo;

        //Other values
        protected string name;

        /// <summary>
        /// 
        /// </summary>
        public Object3() {
            Init();
        }

        public Object3(Dictionary<string, string> args) {
            Init();

            //Process Arguments
            #region Set Rotational Values
            //Process arguments
            if (args.ContainsKey("rotation")) {
                rot = new VectorExpr(new string[] {
                args["rotation"], args["rotation"], args["rotation"] });
            }

            if (args.ContainsKey("xrotation")) {
                rot.setExprAt(0, args["xrotation"]);
            }

            if (args.ContainsKey("yrotation")) {
                rot.setExprAt(1, args["yrotation"]);
            }

            if (args.ContainsKey("zrotation")) {
                rot.setExprAt(2, args["zrotation"]);
            }
            #endregion

            #region Set Color Values
            if (args.ContainsKey("color")) {
                color = new VectorExpr(new string[] {
                    args["color"], args["color"], args["color"], args["color"]
                });
            }

            if (args.ContainsKey("rcolor")) {
                color.setExprAt(0, args["rcolor"]);
            }

            if (args.ContainsKey("gcolor")) {
                color.setExprAt(1, args["gcolor"]);
            }

            if (args.ContainsKey("bcolor")) {
                color.setExprAt(2, args["bcolor"]);
            }

            if (args.ContainsKey("acolor")) {
                color.setExprAt(3, args["acolor"]);
            }
            #endregion

            if (args.ContainsKey("scale")) {
                scale = new ExpressionD(args["scale"]);
            }

            if (args.ContainsKey("name")) {
                name = args["name"];
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Init() {
            geo = MatrixD.OfArray(new Double[,] {
                {0, 0, 0}
            }).Transpose();

            geometry = new double[] { 0, 0, 0 };
            position = new double[] { 0, 0, 0 };
            vertexColor = new double[] { 0, 0, 0, 0 };
            origin = new VectorExpr(new string[] { "0", "0", "0" });
            rot = new VectorExpr(new string[] { "0", "0", "0" });
            trans = new VectorExpr(new string[] { "0", "0", "0" });
            color = new VectorExpr(new string[] { "1", "0", "1", "0" });
            scale = new ExpressionD("1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        public void SetTranslation(VectorExpr vec) {
            trans = vec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void setOrigin(VectorExpr origin) {
            this.origin = origin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(double[] position) {
            if (position.Length != 3) {
                throw new Exception("Invalid number of elements in position array, need exactly 3.");
            }
            this.position = position;
        }

        /// <summary>
        /// This method is in charge of perfomring all the logic & calculations
        /// This probably needs to be improved
        /// </summary>
        public virtual void Recalculate() {
            //Recalculate the vector values
            rot.Recalculate();
            trans.Recalculate();
            origin.Recalculate();
            color.Recalculate();

            //Transform geometry matrix
            // = new MatrixD(geo.RowCount, geo.ColumnCount)
            MatrixD _geo;
            _geo = MatrixD.TranslateMatrix(origin.values, geo);
            _geo = MatrixD.ScaleMatrix(scale.Eval(), _geo);
            _geo = MatrixD.RotateMatrix(rot.values, _geo);
            _geo = MatrixD.TranslateMatrix(position[0], position[1], position[2], _geo);
            _geo = MatrixD.TranslateMatrix(trans.values, _geo);

            geometry = _geo.ToColumnWiseArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a shallow copy of this object</returns>
        public virtual Object Clone() {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Note, there is something called a VertexBuffer that could potentially
        /// be used to improve efficiency.
        /// </summary>
        public abstract void Draw();
    }
}
