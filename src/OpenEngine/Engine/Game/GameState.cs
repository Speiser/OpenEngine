using System.Collections.Generic;

namespace OpenEngine
{
    public class GameState
    {
        public static List<GameObject> GameObjects { get; set; }
        public static Camera Camera { get; set; }

        public static void Start()
        {
            Camera = new Camera();
            GameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Called once every frame.
        /// </summary>
        public static void Update()
        {
            GameObjects.ForEach(x => x.Update());
            Camera.Update();
            Input.Update();
        }
    }
}
