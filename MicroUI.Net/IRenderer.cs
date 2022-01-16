using System;

namespace MicroUIDotNet
{
    public interface IRenderer : IDisposable
    {
        void RenderStroke(float[] verts, bool convex);

        void RenderFill();

        void Flush();
    }
}
