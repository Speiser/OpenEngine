using System.Collections.Generic;

namespace OpenEngine
{
    public class GameState
    {
        public static List<GameObject> GameObjects { get; set; }
        public static Camera Camera { get; set; }

        public static void Update()
        {
            GameObjects.ForEach(x => x.Update());
            Camera.Update();
            Input.Update();
        }
    }
}
