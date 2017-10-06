using System.Collections.Generic;

namespace OpenEngine
{
    public class Level
    {
        private readonly List<GameObject> _gameObjects;

        public Level(List<GameObject> gameObjects)
        {
            _gameObjects = gameObjects;
        }

        public void Draw()
        {
            foreach (var obj in _gameObjects)
            {
                obj.GetComponent<Texture2D>().Draw(obj.Transform.Scale, obj.Transform.Position);
            }
        }
    }
}
