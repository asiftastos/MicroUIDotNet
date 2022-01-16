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

        public MicroUIRenderer()
        {
            vboList = new List<float>();
            eboList = new List<uint>();

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

        public void AddVertex(float x, float y)
        {
            vboList.Add(x);
            vboList.Add(y);
            uint elements = (uint)(vboList.Count / 2);  //per 2 elements in vbo we have 1 element (vertex) in ebo
            eboList.Add(elements);
        }
        public void AddTriangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            
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
            GL.DrawElements(BeginMode.LineLoop, eboList.Count, DrawElementsType.UnsignedInt, 0);

            vboList.Clear();
            eboList.Clear();
        }
    }
}
