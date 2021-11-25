using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// GBFX/RAGE (Really Average GB Emulator)
    /// 
    /// BitUtil (Provides bit utilities for RAGE)
    /// 
    /// November 24, 2021
    /// </summary>
    public static class BitUtil
    {
        public static bool GetBit(byte BitNumber, byte TheByte)
        {
            if (BitNumber < 0
            || BitNumber > 7)
            {
                throw new InvalidOperationException("There's only 8 bits in a byte!");
            }
            else
            {
                return (TheByte & (1 << BitNumber)) != 0;
            }
        }
    }
}
