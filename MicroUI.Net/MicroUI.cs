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
                for(int i = path.start; i < path.start + path.count; i++)
                {
                    Commands cmd = (Commands)commands[i];
                    Renderer.AddVertex(commands[i + 1], commands[i + 2]);
                    i += 2;
                }
            }
        }

        public bool BeginWindow()
        {
            return true;
        }

        public void EndWindow()
        {
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
            _currentPath.start = commands.Count;
            MoveTo(x, y);
            LineTo(x + w, y);
            LineTo(x +w, y + h);
            LineTo(x, y + h);
            commands.Add((float)Commands.CLOSE);
            _currentPath.count =  (commands.Count - 1) - _currentPath.start;
            _pathQueue.Enqueue(_currentPath);
        }

        public void BeginPath(PathType pathType)
        {
            _currentPath = new MicroUIPath() { type = pathType };
        }
    }
}
