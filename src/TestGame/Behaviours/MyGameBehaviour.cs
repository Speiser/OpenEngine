using OpenEngine;

namespace TestGame
{
    public class MyGameBehaviour : GameBehaviour
    {
        public override void Start()
        {
            base.Start();
            var floorTexture = Texture2D.LoadTexture("Floor.jpg");
            var floorTemplate = new GameObject();
            floorTemplate.AddComponent(floorTexture);

            for (int i = -1000; i < 1001; i += 100)
            {
                for (int j = -1000; j < 1001; j += 100)
                {
                    var obj = floorTemplate.Clone();
                    obj.Transform.Position = new Vector2D(i, j);
                    obj.AddComponent(new FloorBehaviour(obj));
                    GameState.GameObjects.Add(obj);
                }
            }

            // Player
            var player = new GameObject();
            player.AddComponent(Texture2D.LoadTexture("Player.jpg"));
            player.AddComponent(new PlayerBehaviour(player));

            GameState.GameObjects.Add(player);
        }

        public override void Update()
        {
            base.Update();
            Debug.Log(GameState.Camera.Position.X + ":" + GameState.Camera.Position.Y);
            if (Input.KeyDown(Key.W)) GameState.Camera.Move(new Vector2D(0, -3));
            if (Input.KeyDown(Key.A)) GameState.Camera.Move(new Vector2D(-3, 0));
            if (Input.KeyDown(Key.S)) GameState.Camera.Move(new Vector2D(0, 3));
            if (Input.KeyDown(Key.D)) GameState.Camera.Move(new Vector2D(3, 0));
        }
    }
}
