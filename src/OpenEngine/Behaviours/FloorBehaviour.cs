namespace OpenEngine
{
    public class FloorBehaviour : Behaviour
    {
        private Texture2D _texture;

        public FloorBehaviour(GameObject obj) : base(obj) {}

        public override void Start()
        {
            _texture = Object.GetComponent<Texture2D>();
        }

        public override void Update()
        {
            _texture.Draw(Object.Transform.Position);
        }
    }
}
