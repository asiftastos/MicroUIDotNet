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
        private ShaderDesc colorShaderDesc;
        private Matrix4 ortho;

        public MicroUIDemo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            colorShaderDesc = new ShaderDesc
            {
                Name = "Color",
                vsPath = "Assets/Shaders/color.vs",
                fsPath = "Assets/Shaders/color.fs"
            };

            microUI = new MicroUI();
            microUI.Renderer = new MicroUIRenderer();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            LoadShader(ref colorShaderDesc);

            ortho = Matrix4.CreateOrthographicOffCenter(0.0f, ClientSize.X, 0.0f, ClientSize.Y, 0.1f, 1.0f);
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

            microUI.End();

            GL.UseProgram(colorShaderDesc.id);
            GL.UniformMatrix4(GL.GetUniformLocation(colorShaderDesc.id, "ortho"), false, ref ortho);
            microUI.Renderer.Flush();

            SwapBuffers();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            if(e.Key == Keys.Escape)
                Close();
        }

        private void LoadShader(ref ShaderDesc shaderDesc)
        {
            string vsSource = File.ReadAllText(shaderDesc.vsPath);
            int vsId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vsId, vsSource);
            GL.CompileShader(vsId);
            Console.WriteLine($"Vertex log: {GL.GetShaderInfoLog(vsId)}");

            string fsSource = File.ReadAllText(shaderDesc.fsPath);
            int fsId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fsId, fsSource);
            GL.CompileShader(fsId);
            Console.WriteLine($"Fragment log: {GL.GetShaderInfoLog(fsId)}");

            shaderDesc.id = GL.CreateProgram();
            GL.AttachShader(shaderDesc.id, vsId);
            GL.AttachShader(shaderDesc.id, fsId);
            GL.LinkProgram(shaderDesc.id);
            Console.WriteLine($"Program log: {GL.GetProgramInfoLog(shaderDesc.id)}");
        }
    }
}
