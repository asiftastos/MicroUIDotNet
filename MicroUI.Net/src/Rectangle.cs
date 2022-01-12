using System;
using System.Numerics;

namespace MicroUIDotNet
{
    public struct Rectangle
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.W = width;
            this.H = height;
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle rectangle &&
                   X == rectangle.X &&
                   Y == rectangle.Y &&
                   W == rectangle.W &&
                   H == rectangle.H;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, W, H);
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public void Expand(int n)
        {
            X -= n;
            Y -= n;
            W += (n * 2);
            H += (n * 2);
        }

        public Rectangle Intersect(Rectangle r2)
        {
            int x1 = Math.Max(X, r2.X);
            int y1 = Math.Max(Y, r2.Y);
            int x2 = Math.Min(X + W, r2.X + r2.W);
            int y2 = Math.Min(Y + H, r2.Y + r2.H);
            if(x2 < x1) { x2 = x1; }
            if(y2 < y1) { y2 = y1; }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public bool OverlapsVector2(Vector2 v)
        {
            return v.X >= X && v.X < X + W && v.Y >= Y && v.Y < Y + H;
        }

        public override string ToString()
        {
            return $"[{X},{Y},{W},{H}]";
        }
    }
}
