using MicroUIDotNet;
using OpenTK.Mathematics;

namespace MicroUIDemo
{
    public struct MicroUICall
    {
        public PathType type;
        public int offset;
        public int length;
        public bool convex;
        public Vector4 color;
    }
}