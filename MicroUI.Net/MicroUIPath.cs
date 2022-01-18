using System.Collections.Generic;

namespace MicroUIDotNet
{
    public struct MicroUIPath
    {
        public PathType type;
        public int start;
        public int count;
        public bool convex;
        public List<float> verts;
        public MicroUIPaint paint;
    }
}