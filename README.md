# OpenEngine
- **This is a WIP prototype!**  
- **Expect API changes**
- **This is a side project**

## Installation
Clone and build

## Example Game

### Create a `OpenEngineWindow`
```csharp
Debug.HideConsole();

new OpenEngineWindow(1280, 720, "TestGame", new MyGameBehaviour())
        .Start();
```

### Create a `GameBehaviour`
```csharp
public class MyGameBehaviour : GameBehaviour
{
    // Called once on game start
    public override void Start()
    {
        base.Start();

        // Load textures
        var floorTexture = Texture2D.LoadTexture("Floor.jpg");

        // Create a GameObject
        var floorTemplate = new GameObject();
        floorTemplate.AddComponent(floorTexture);

        // Initialize a few floor GameObjects
        for (int i = -1000; i < 1001; i += 100)
        {
            for (int j = -1000; j < 1001; j += 100)
            {
                var obj = floorTemplate.Clone();
                obj.Transform.Position = new Vector2D(i, j);
                obj.AddComponent(new FloorBehaviour(obj));

                // Add the GameObjects to the GameState.
                GameState.GameObjects.Add(obj);
            }
        }

        // Create a player GameObject
        var player = new GameObject();
        player.AddComponent(Texture2D.LoadTexture("Player.jpg"));
        player.AddComponent(new PlayerBehaviour(player));

        GameState.GameObjects.Add(player);
    }
    
    // Called once every frame
    public override void Update()
    {
        base.Update();

        // Move the camera on key press
        if (Input.KeyDown(Key.W))
            GameState.Camera.Move(new Vector2D(0, -3));

        if (Input.KeyDown(Key.A))
            GameState.Camera.Move(new Vector2D(-3, 0));

        if (Input.KeyDown(Key.S))
            GameState.Camera.Move(new Vector2D(0, 3));

        if (Input.KeyDown(Key.D))
            GameState.Camera.Move(new Vector2D(3, 0));
    }
}
```

### Create some `Behaviour`s

#### `PlayerBehaviour`
```csharp
public class PlayerBehaviour : Behaviour
{
    private Texture2D _texture;

    public PlayerBehaviour(GameObject obj) : base(obj) { }

    public override void Start()
    {
        // Cache the texture on start
        _texture = Object.GetComponent<Texture2D>();
    }

    public override void Update()
    {
        Object.Transform.Position = GameState.Camera.Position;
        _texture.Draw(
            new Vector2D(-Object.Transform.Position.X,
                         -Object.Transform.Position.Y));
    }
}
```

#### `FloorBehaviour`
```csharp
public class FloorBehaviour : Behaviour
{
    private Texture2D _texture;

    public FloorBehaviour(GameObject obj) : base(obj) { }

    public override void Start()
    {
        _texture = Object.GetComponent<Texture2D>();
    }

    public override void Update()
    {
        _texture.Draw(Object.Transform.Position);
    }
}
```