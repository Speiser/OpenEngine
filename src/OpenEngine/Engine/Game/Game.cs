using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Linq;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Game"/> class.
    /// </summary>
    public class Game : GameWindow
    {
        private int _frameCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="width">Width of the game window.</param>
        /// <param name="height">Height of the game window.</param>
        /// <param name="title">Title of the game window.</param>
        public Game(int width, int height, string title) : base(width, height)
        {
            GameState.Camera = new Camera();
            GameState.GameObjects = new List<GameObject>();
            Input.Init(this);

            base.Title = title;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            #region NON ENGINE PART
            var floorTexture = Texture2D.LoadTexture("Floor.jpg");
            var floorTemplate = new GameObject();
            floorTemplate.AddComponent(floorTexture);
            floorTemplate.UpdateAction = obj => obj.GetComponent<Texture2D>().Draw(obj.Transform.Position);

            for (int i = -1000; i < 1001; i += 100)
            {
                for (int j = -1000; j < 1001; j += 100)
                {
                    var obj = floorTemplate.Clone();
                    obj.Transform.Position = new Vector2(i, j);
                    GameState.GameObjects.Add(obj);
                }
            }

            // Player
            var player = new GameObject();
            player.AddComponent(Texture2D.LoadTexture("Player.jpg"));
            player.UpdateAction = obj =>
            {
                obj.Transform.Position = GameState.Camera.Position;
                obj.GetComponent<Texture2D>().Draw(-obj.Transform.Position);
            };

            GameState.GameObjects.Add(player);
            #endregion

            // Enable Textures
            GL.Enable(EnableCap.Texture2D);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            HandleInput();

            if (_frameCount == 30)
            {
                Console.Title = (int)this.RenderFrequency + " FPS";
                Console.Clear();
                Debug.Log($"Camera Position:  {GameState.Camera.Position.X}/{GameState.Camera.Position.Y}");
                Debug.Log($"Player Position: {GameState.GameObjects.Last().Transform.Position.X}/{GameState.GameObjects.Last().Transform.Position.Y}");
                Debug.Log($"GameObject Count: {GameState.GameObjects.Count}");
                _frameCount = -1;
            }

            _frameCount++;
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(GameState.Camera.SkyboxColor);

            this.InitRenderFrame();

            GameState.Camera.ApplyTransform();
            GameState.Update();

            this.SwapBuffers();
        }

        private void InitRenderFrame()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(
                -this.Width / 2f,
                 this.Width / 2f,
                 this.Height / 2f,
                -this.Height / 2f,
                 GameState.Camera.ZNear,
                 GameState.Camera.ZFar);
        }
        private static void HandleInput()
        {
            if (Input.KeyDown(Key.W)) GameState.Camera.Move(new Vector2(0, -1));
            if (Input.KeyDown(Key.A)) GameState.Camera.Move(new Vector2(-1, 0));
            if (Input.KeyDown(Key.S)) GameState.Camera.Move(new Vector2(0, 1));
            if (Input.KeyDown(Key.D)) GameState.Camera.Move(new Vector2(1, 0));
        }
    }
}
