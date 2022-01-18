using System;

namespace MicroUIDotNet
{
    public interface IRenderer : IDisposable
    {
        void RenderStroke(MicroUIPath path);

        void RenderFill(MicroUIPath path);

        void Flush();
    }
}
