using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenEngine
{
    /// <summary>
    /// The <see cref="Game"/> class.
    /// </summary>
    public class Game : GameWindow
    {
        private readonly GameBehaviour behaviour;
        private readonly List<Camera> cameras = new List<Camera>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="width">Width of the game window.</param>
        /// <param name="height">Height of the game window.</param>
        /// <param name="title">Title of the game window.</param>
        /// <param name="behaviour">The game behaviour.</param>
        /// <param name="camera">Main camera.</param>
        public Game(
            int width,
            int height,
            string title,
            GameBehaviour behaviour,
            Camera camera = null) : base(width, height)
        {
            this.cameras.Add(camera ?? new Camera(Vector2.Zero));
            Input.Start(this);
            this.GameObjects = new List<GameObject>();
            base.Title = title;
            this.behaviour = behaviour;
            this.behaviour.Game = this;
        }

        public List<GameObject> GameObjects { get; set; }

        public Camera MainCamera => this.cameras[0];

        public Camera[] Cameras => this.cameras.ToArray();

        protected override void OnLoad(EventArgs e)
        {
            // Enable Textures
            GL.Enable(EnableCap.Texture2D);

            base.OnLoad(e);

            this.behaviour.Start();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            this.MainCamera.Update();
            this.behaviour.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(this.MainCamera.SkyboxColor);

            this.InitRenderFrame();
            this.MainCamera.ApplyTransform();

            this.GameObjects.ForEach(x => x.Update());
            
            Input.Update();

            this.SwapBuffers();
            this.behaviour.Render();
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
                 this.MainCamera.ZNear,
                 this.MainCamera.ZFar);
        }
    }
}
