using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MicroUIDotNet;

namespace MicroUIDemo
{
    internal class MicroUIDemo : GameWindow
    {
        private MicroUI microUI;

        public MicroUIDemo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            microUI = new MicroUI();
            microUI.Renderer = new MicroUIRenderer(ClientSize.ToVector2());
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            microUI.Renderer.Dispose();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            microUI.Begin();

            microUI.BeginPath(PathType.LINE);
            microUI.Rectangle(1.0f, 0.0f, 100.0f, 100.0f);
            microUI.Stroke();

            microUI.BeginPath(PathType.LINE);
            microUI.Rectangle(100.0f, 400.0f, 100.0f, 100.0f);
            microUI.Stroke();

            microUI.BeginPath(PathType.LINE);
            microUI.MoveTo(400.0f, 400.0f);
            microUI.LineTo(500.0f, 400.0f);
            microUI.LineTo(500.0f, 500.0f);
            microUI.StrokePaint(MicroUIColor.FromRGBA(255, 0, 0, 255));
            microUI.Stroke();

            microUI.End();

            microUI.Renderer.Flush();

            SwapBuffers();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            if(e.Key == Keys.Escape)
                Close();
        }
    }
}
