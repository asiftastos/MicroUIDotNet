using System;

namespace MicroUIDotNet.Input
{
    [Flags]
    public enum Key
    {
        SHIFT = 0,
        CTRL = 1,
        ALT = 2,
        BACKSPACE = 4,
        RETURN = 8
    }
}
