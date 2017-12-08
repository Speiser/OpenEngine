using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Game"/> class.
    /// </summary>
    public class Game : GameWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="width">Width of the game window.</param>
        /// <param name="height">Height of the game window.</param>
        /// <param name="title">Title of the game window.</param>
        /// <param name="behaviour">The game behaviour.</param>
        public Game(int width, int height, string title, GameBehaviour behaviour) : base(width, height)
        {
            Behaviour = behaviour;
            GameState.Start();
            Input.Start(this);

            base.Title = title;
        }

        public GameBehaviour Behaviour { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Behaviour.Start();

            // Enable Textures
            GL.Enable(EnableCap.Texture2D);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            Behaviour.Update();
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
    }
}
