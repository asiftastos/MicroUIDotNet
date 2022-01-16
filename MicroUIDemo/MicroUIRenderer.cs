using System;
using System.Collections.Generic;
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

        public MicroUIRenderer()
        {
            vboList = new List<float>();
            eboList = new List<uint>();
            _calls = new List<MicroUICall>();

            vao =  GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteVertexArray(vao);
        }

        public void Flush()
        {
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

        public void RenderStroke(float[] verts, bool convex)
        {
            int count = verts.Length;
            MicroUICall call = new MicroUICall();
            call.type = PathType.LINE;
            vboList.AddRange(verts);
            call.offset = eboList.Count;
            call.length = count / 2;
            call.convex = convex;
            int index = call.offset;
            for(int i = 0; i < call.length; i++)
            {
                eboList.Add((uint)index);
                index++;
            }
            _calls.Add(call);
        }

        public void RenderFill()
        {
        }
    }
}
