namespace MicroUIDotNet
{
    public struct MicroUIColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public static MicroUIColor FromRGB(byte r, byte g, byte b)
        {
            MicroUIColor c = new MicroUIColor();
            c.R = r / 255;
            c.G = g / 255;
            c.B = b / 255;
            c.A = 1.0f;
            return c;
        }

        public static MicroUIColor FromRGBA(byte r, byte g, byte b, byte a)
        {
            MicroUIColor c = MicroUIColor.FromRGB(r, g, b);
            c.A = a / 255;
            return c;
        }
    }
}