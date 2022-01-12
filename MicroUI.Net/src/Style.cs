using System;
using System.Numerics;

namespace MicroUIDotNet
{
    public class Style
    {
        public enum StyleColors
        {
            TEXT,
            BORDER,
            WINDOW_BACKGROUND,
            TITLE_BACKGROUND,
            TITLE_TEXT,
            PANEL_BACKGROUND,
            BUTTON,
            BUTTON_HOVER,
            BUTTON_FOCUS,
            BASE,
            BASE_HOVER,
            BASE_FOCUS,
            SCROLL_BASE,
            SCROLL_THUMB
        }

        public Vector2 Size { get; set; }
        public int Padding { get; set; }
        public int Spacing { get; set; }
        public int Indent { get; set; }
        public int TitleHeight { get; set; }
        public int ScrollBarSize { get; set; }
        public int ThumbSize { get; set; }
        public Color[] Colors { get; set; }

        public Style()
        {
        }

        public readonly static Style Default = new Style()
        {
            Size = new Vector2(68, 10),
            Padding = 5,
            Spacing = 4,
            Indent = 24,
            TitleHeight = 24,
            ScrollBarSize = 12,
            ThumbSize = 8,
            Colors = new Color[]
            {
                new Color(230, 230, 230, 255),
                new Color(25, 25, 25, 255),
                new Color(50, 50, 50, 255),
                new Color(25, 25, 25, 255),
                new Color(240, 240, 240, 255),
                new Color(0, 0, 0, 0),
                new Color(75, 75, 75, 255),
                new Color(95, 95, 95, 255),
                new Color(115, 115, 115, 255),
                new Color(30, 30, 30, 255),
                new Color(35, 35, 35, 255),
                new Color(40, 40, 40, 255),
                new Color(43, 43, 43, 255),
                new Color(30, 30, 30, 255)
            }
        };
    }
}
