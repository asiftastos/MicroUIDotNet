using System;
using System.Collections.Generic;
using System.IO;
using MicroUIDotNet;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MicroUIDemo
{
    public class MicroUIRenderer : IRenderer
    {
        private int vao;
        private int vbo;
        private int ebo;

        private List<float> vboList;
        private List<uint> eboList;

        private List<MicroUICall> _calls;

        private ShaderDesc colorShaderDesc;
        private Matrix4 ortho;

        public MicroUIRenderer(Vector2 winSize)
        {
            vboList = new List<float>();
            eboList = new List<uint>();
            _calls = new List<MicroUICall>();

            vao =  GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            colorShaderDesc = new ShaderDesc
            {
                Name = "Color",
                vsPath = "Assets/Shaders/color.vs",
                fsPath = "Assets/Shaders/color.fs"
            };

            LoadShader(ref colorShaderDesc);

            ortho = Matrix4.CreateOrthographicOffCenter(0.0f, winSize.X, 0.0f, winSize.Y, 0.1f, 1.0f);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteVertexArray(vao);
        }

        public void Flush()
        {
            GL.UseProgram(colorShaderDesc.id);
            GL.UniformMatrix4(GL.GetUniformLocation(colorShaderDesc.id, "ortho"), false, ref ortho);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vboList.Count * sizeof(float), vboList.ToArray(), BufferUsageHint.DynamicDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, eboList.Count * sizeof(uint), eboList.ToArray(), BufferUsageHint.DynamicDraw);

            foreach(var call in _calls)
            {
                if (call.type == PathType.LINE)
                {
                    GL.Uniform4(GL.GetUniformLocation(colorShaderDesc.id, "color"), call.color);
                    BeginMode mode = call.convex ? BeginMode.LineLoop : BeginMode.LineStrip;
                    GL.DrawElements(mode, call.length, DrawElementsType.UnsignedInt, call.offset * sizeof(uint));
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, call.offset, call.length);
                }

            }

            vboList.Clear();
            eboList.Clear();
            _calls.Clear();
        }

        public void RenderStroke(MicroUIPath path)
        {
            int count = path.verts.Count;
            MicroUICall call = new MicroUICall();
            call.type = PathType.LINE;
            vboList.AddRange(path.verts);
            call.offset = eboList.Count;
            call.length = count / 2;
            call.convex = path.convex;
            call.color = new Vector4(path.paint.color.R, path.paint.color.G, path.paint.color.B, path.paint.color.A);
            int index = call.offset;
            for(int i = 0; i < call.length; i++)
            {
                eboList.Add((uint)index);
                index++;
            }
            _calls.Add(call);
        }

        public void RenderFill(MicroUIPath path)
        {
        }

        private void LoadShader(ref ShaderDesc shaderDesc)
        {
            string vsSource = File.ReadAllText(shaderDesc.vsPath);
            int vsId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vsId, vsSource);
            GL.CompileShader(vsId);
            //Console.WriteLine($"Vertex log: {GL.GetShaderInfoLog(vsId)}");

            string fsSource = File.ReadAllText(shaderDesc.fsPath);
            int fsId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fsId, fsSource);
            GL.CompileShader(fsId);
            //Console.WriteLine($"Fragment log: {GL.GetShaderInfoLog(fsId)}");

            shaderDesc.id = GL.CreateProgram();
            GL.AttachShader(shaderDesc.id, vsId);
            GL.AttachShader(shaderDesc.id, fsId);
            GL.LinkProgram(shaderDesc.id);
            //Console.WriteLine($"Program log: {GL.GetProgramInfoLog(shaderDesc.id)}");
        }
    }
}
