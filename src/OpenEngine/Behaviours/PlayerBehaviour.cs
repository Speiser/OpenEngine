namespace OpenEngine
{
    public class PlayerBehaviour : Behaviour
    {
        private Texture2D _texture;

        public PlayerBehaviour(GameObject obj) : base(obj) {}

        public override void Start()
        {
            _texture = Object.GetComponent<Texture2D>();
        }

        public override void Update()
        {
            Object.Transform.Position = GameState.Camera.Position;
            _texture.Draw(-Object.Transform.Position);
        }
    }
}
