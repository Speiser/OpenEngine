using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenEngine
{
    public class Game : GameWindow
    {
        private readonly InputHandler _input;
        private readonly float _zNear;
        private readonly float _zFar;

        private Dictionary<string, Texture2D> _textures;

        public Game(int width, int height, string title, float zNear = 0f, float zFar = 1f) : base(width, height)
        {
            Camera = new Camera();
            _input = new InputHandler(this);
            _textures = new Dictionary<string, Texture2D>();

            base.Title = title;
            _zNear = zNear;
            _zFar = zFar;
        }

        public Camera Camera { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Load Textures
            _textures["Floor"] = Texture2D.LoadTexture("Floor.jpg");

            // Enable Textures
            GL.Enable(EnableCap.Texture2D);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            this.HandleInput();

            Camera.Update();
            _input.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Camera.SkyboxColor);

            this.InitRenderFrame();
            Camera.ApplyTransform();

            _textures["Floor"].Draw(new Vector2(-100, 0));
            _textures["Floor"].Draw(new Vector2(0, 0));
            _textures["Floor"].Draw(new Vector2(100, 0));

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
                 _zNear,
                 _zFar);
        }

        private void HandleInput()
        {
            if (_input.KeyDown(Key.W)) Camera.Move(new Vector2(0, -1));
            if (_input.KeyDown(Key.A)) Camera.Move(new Vector2(-1, 0));
            if (_input.KeyDown(Key.S)) Camera.Move(new Vector2(0, 1));
            if (_input.KeyDown(Key.D)) Camera.Move(new Vector2(1, 0));
        }
    }
}
