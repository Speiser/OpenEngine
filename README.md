# OpenEngine

- **This is a WIP prototype!**  
- **Expect API changes**
- **This is a side project**

## Installation

- Install [OpenTK](https://www.nuget.org/packages/OpenTK/3.0.0) NuGet

```
PM> Install-Package OpenTK -Version 3.0.0
```

- Clone and build

## Example Game

### Create a new `Game`

```csharp
new Game(1280, 720, "TestGame", new MyGameBehaviour()).Run(60, 60);
```

### Create a `GameBehaviour`

A `GameBehaviour` can be used for instantiating `GameObject` and custom update or render actions.

```csharp
public class MyGameBehaviour : GameBehaviour
{
    public override void Start()
    {
        base.Start();

        this.Game.MainCamera.AddComponent(new CameraBehaviour(this.Game.MainCamera));

        // Floor
        var floorTemplate = new GameObject();
        floorTemplate.AddComponent(Texture2D.LoadTexture("Floor.jpg"));

        for (int i = -1000; i < 1001; i += 100)
        {
            for (int j = -1000; j < 1001; j += 100)
            {
                var obj = floorTemplate.Clone();
                obj.Transform.Position = new Vector2(i, j);
                this.Game.GameObjects.Add(obj);
            }
        }

        // Player
        var player = new GameObject();
        player.AddComponent(Texture2D.LoadTexture("Player.jpg"));
        player.AddComponent(new PlayerBehaviour(player));

        this.Game.GameObjects.Add(player);
    }
}
```

### Create some `Behaviour`s

#### `PlayerBehaviour`

```csharp
public class PlayerBehaviour : Behaviour
{
    public PlayerBehaviour(GameObject obj) : base(obj) {}

    public override void Update()
    {
        if (Input.KeyDown(Key.W)) this.GameObject.Transform.Position += new Vector2(0, -3);
        if (Input.KeyDown(Key.A)) this.GameObject.Transform.Position += new Vector2(-3, 0);
        if (Input.KeyDown(Key.S)) this.GameObject.Transform.Position += new Vector2(0, 3);
        if (Input.KeyDown(Key.D)) this.GameObject.Transform.Position += new Vector2(3, 0);
    }
}
```

#### `CameraBehaviour`

```csharp
public class CameraBehaviour : Behaviour
{
    private readonly Camera camRef;

    public CameraBehaviour(GameObject obj) : base(obj)
    {
        this.camRef = obj as Camera;
    }

    public override void Update()
    {
        if (Input.KeyDown(Key.Up))    this.camRef.Move(new Vector2(0, -3));
        if (Input.KeyDown(Key.Left))  this.camRef.Move(new Vector2(-3, 0));
        if (Input.KeyDown(Key.Down))  this.camRef.Move(new Vector2(0, 3));
        if (Input.KeyDown(Key.Right)) this.camRef.Move(new Vector2(3, 0));
    }
}
```
