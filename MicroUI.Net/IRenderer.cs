using System;

namespace MicroUIDotNet
{
    public interface IRenderer : IDisposable
    {
        void AddVertex(float x, float y);

        void AddTriangle(float x1, float y1, float x2, float y2, float x3, float y3);

        void Flush();
    }
}
