using System;
using System.Collections.Generic;
using System.Numerics;

namespace MicroUIDotNet
{
    public class MicroUI
    {
        private Queue<MicroUIPath> _pathQueue;
        private List<float> commands;
        private MicroUIPath _currentPath;

        public IRenderer Renderer { get; set; }

        public MicroUI()
        {
            commands = new List<float>();
            _pathQueue = new Queue<MicroUIPath>();
        }

        public void Begin()
        {
            commands.Clear();
        }

        public void End()
        {
            while(_pathQueue.Count > 0)
            {
                MicroUIPath path = _pathQueue.Dequeue();
                switch(path.type)
                {
                    case PathType.LINE:
                        RenderStroke(path);
                        break;
                    case PathType.FILL:
                        RenderFill(path);
                        break;
                    default:
                        break;
                }
            }
        }


        public void MoveTo(float x, float y)
        {
            commands.AddRange(new [] { (float)Commands.MOVETO, x, y });
        }

        public void LineTo(float x, float y)
        {
            commands.AddRange(new [] { (float)Commands.LINETO, x, y });
        }

        public void Rectangle(float x, float y, float w, float h)
        {
            MoveTo(x, y);
            LineTo(x + w, y);
            LineTo(x + w, y + h);
            LineTo(x, y + h);
            _currentPath.convex = true;
        }

        public void BeginPath(PathType pathType)
        {
            _currentPath = new MicroUIPath() { type = pathType };
            _currentPath.start = commands.Count;
        }

        public void Stroke()
        {
            commands.Add((float)Commands.CLOSE);
            _currentPath.count = (commands.Count - 1) - _currentPath.start;
            _pathQueue.Enqueue(_currentPath);
        }

        public void Fill()
        {
        }

        private void RenderStroke(MicroUIPath path)
        {
            int length = path.start + path.count;
            List<float> verts = new List<float>();
            for (int i = path.start; i < length; i++)
            {
                Commands cmd = (Commands)commands[i];
                if (cmd == Commands.CLOSE)
                    break;

                verts.Add(commands[i + 1]);
                verts.Add(commands[i + 2]);
                i += 2;
            }
            Renderer.RenderStroke(verts.ToArray(), path.convex);
        }

        private void RenderFill(MicroUIPath path)
        {
        }
    }
}
