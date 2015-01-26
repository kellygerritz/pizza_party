using System;


using Complexity.Objects;
using System.Collections;
using System.Threading;

namespace Complexity {
    /// <summary>
    /// Contains all the information that a render window needs to draw.
    /// </summary>
    public class Scene {
        private ArrayList objects;

        //Accessable members
        public float cameraRotation = 0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public Scene() {
                Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init() {
            objects = new ArrayList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Add(Object3 obj) {
            objects.Add(obj);
        }

        /// <summary>
        /// Recalculate the properties of all objects in the scene
        /// </summary>
        public void Recalculate() {
            foreach (Object3 obj in objects) {
                obj.Recalculate();
            }
        }

        public void Draw() {
            foreach (Object3 obj in objects) {
                obj.Draw();
            }
        }
    }
}
