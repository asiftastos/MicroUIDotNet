using System;
using System.Numerics;
using MicroUIDotNet.Input;

namespace MicroUIDotNet
{
    public class MicroUI : IDisposable
    {
        private Vector2 _mousePosition;
        private Vector2 _lastMousePosition;
        private Vector2 _mouseDelta;
        private Vector2 _scrollDelta;
        private MouseButton _mouseDown;
        private MouseButton _mousePressed;
        private Key _keyDown;
        private Key _keyPressed;
        private string _inputText;

        private Style _style;

        private uint _hover;
        private uint _focus;
        private uint _lastId;
        private Rectangle _lastRect;
        private int _lastZindex;
        private int _updatedFocus;
        private int _frame;

        public ITextSize TextSize { get; set; }

        public uint Focus { set {
                _focus = value; 
                _updatedFocus = 1;
            } }

        public MicroUI()
        {
            _style = Style.Default;
        }

        public void Dispose()
        {
        }

        public void Begin()
        {
        }

        public void End()
        {
        }

        public void MouseMove(int x, int y)
        {
            _mousePosition = new Vector2(x, y);
        }

        public void MouseDown(int x, int y, MouseButton btn)
        {
            MouseMove(x, y);
            _mouseDown |= btn;
            _mousePressed |= btn;
        }

        public void MouseUp(int x, int y, MouseButton btn)
        {
            MouseMove(x, y);
            _mouseDown &= ~btn;
        }

        public void MouseScroll(int x, int y)
        {
            _mouseDelta.X += (float)x;
            _mouseDelta.Y += (float)y;
        }

        public void KeyDown(Key key)
        {
            _keyDown |= key;
            _keyPressed |= key;
        }

        public void KeyUp(Key key)
        {
            _keyDown &= ~key;
        }

        public void InputText(string text)
        {
            _inputText = text;
        }
    }
}
