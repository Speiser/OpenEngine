using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenEngine
{
    public class Game : GameWindow
    {
        private int _frameCount;
        private readonly List<GameObject> _gameObjects;
        private Level _currentLevel;

        public Game(int width, int height, string title) : base(width, height)
        {
            this.Camera = new Camera();
            _gameObjects = new List<GameObject>();
            Input.Init(this);

            base.Title = title;
        }

        public Camera Camera { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var floorTexture = Texture2D.LoadTexture("Floor.jpg");
            var floorTemplate = new GameObject();
            floorTemplate.AddComponent(floorTexture);

            _gameObjects.Add(floorTemplate);
            _currentLevel = new Level(_gameObjects);

            // Enable Textures
            GL.Enable(EnableCap.Texture2D);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            this.HandleInput();

            this.Camera.Update();
            Input.Update();

            if (_frameCount == 30)
            {
                Console.Title = (int)this.RenderFrequency + " FPS";
                Console.Clear();
                Debug.Log($"Camera Position:  {this.Camera.Position.X}/{this.Camera.Position.Y}");
                Debug.Log($"GameObject Count: {_gameObjects.Count}");
                _frameCount = -1;
            }

            _frameCount++;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Camera.SkyboxColor);

            this.InitRenderFrame();
            this.Camera.ApplyTransform();

            _currentLevel.Draw();

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
                 this.Camera.ZNear,
                 this.Camera.ZFar);
        }

        private void HandleInput()
        {
            if (Input.KeyDown(Key.W)) Camera.Move(new Vector2(0, -1));
            if (Input.KeyDown(Key.A)) Camera.Move(new Vector2(-1, 0));
            if (Input.KeyDown(Key.S)) Camera.Move(new Vector2(0, 1));
            if (Input.KeyDown(Key.D)) Camera.Move(new Vector2(1, 0));
        }
    }
}
