using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// DMGIO
    /// 
    /// Holds DMG I/O registers.
    /// </summary>
    public static class DMGIO // nonstatic?
    {
        /// <summary>
        /// FF00 - Joypad
        /// 
        /// https://gbdev.io/pandocs/Joypad_Input.html
        /// Bit5 - read action buttons
        /// Bit4 - read direction buttons
        /// Bit3 - Down (D) / Start (A)     0=selected
        /// Bit2 - Up (D) / Select (A)      0=selected
        /// Bit1 - Left (D) / B (A)         0=selected
        /// Bit0 - Right (D) / A (A)        0=selected
        /// </summary>
        public static Mappable JOYP { get; set;  }

        /// <summary>
        /// FF10 - Sound Channel 1 - Sweep
        /// </summary>
        public static Mappable NR10 { get; set;  }

        /// <summary>
        /// FF11 - Sound Channel 1 - Sound Length (bit 5-0 write only)/Wave Pattern Duty (bit 7-6 r/w)
        /// </summary>
        public static Mappable NR11 { get; set; }

        /// <summary>
        /// FF12 - Sound Channel 1 - Volume Envelope
        /// </summary>
        public static Mappable NR12 { get; set; }

        /// <summary>
        /// FF13 - Sound Channel 1 - Frequency Low (write only)
        /// </summary>
        public static Mappable NR13 { get; set; }

        /// <summary>
        /// FF14 - Sound Channel 1 - Frequency High (read/write)
        /// </summary>
        public static Mappable NR14 { get; set; }

        /// <summary>
        /// FF16 - Sound Channel 2 - Sound Length (bit 5-0 write only)/Wave Pattern Duty (bit 7-6 r/w)
        /// </summary>
        public static Mappable NR21 { get; set; }

        /// <summary>
        /// FF17 - Sound Channel 2 - Volume Envelope
        /// </summary>
        public static Mappable NR22 { get; set; }

        /// <summary>
        /// FF18 - Sound Channel 2 - Frequency Low (write only)
        /// </summary>
        public static Mappable NR23 { get; set; }

        /// <summary>
        /// FF19 - Sound Channel 2 - Frequency High (read/write)
        /// </summary>
        public static Mappable NR24 { get; set; }

        /// <summary>
        /// FF1A - Sound Channel 3 - On/Off
        /// </summary>
        public static Mappable NR30 { get; set; }
        
        /// <summary>
        /// FF1B - Sound Channel 3 - Sound Length (bit 5-0 write only)/Wave Pattern Duty (bit 7-6 r/w)
        /// </summary>
        public static Mappable NR31 { get; set; }

        /// <summary>
        /// FF1C - Sound Channel 3 - Volume Envelope
        /// </summary>
        public static Mappable NR32 { get; set; }

        /// <summary>
        /// FF1D - Sound Channel 3 - Frequency Low (write only)
        /// </summary>
        public static Mappable NR33 { get; set; }

        /// <summary>
        /// FF1E - Sound Channel 3 - Frequency High (read/write)
        /// </summary>
        public static Mappable NR34 { get; set; }

        /// <summary>
        /// FF20 - Sound Channel 4 - Sound Length (write)
        /// </summary>
        public static Mappable NR41 { get; set; }

        /// <summary>
        /// FF21 - Sound Channel 4 - Volume Envelope (R/W)
        /// </summary>
        public static Mappable NR42 { get; set; }

        /// <summary>
        /// FF22 - Sound Channel 4 - Polynomial Counter (R/W) 
        /// </summary>
        public static Mappable NR43 { get; set; }

        /// <summary>
        /// FF23 - Sound Channel 4 - Initial / Counter (R/W)
        /// </summary>
        public static Mappable NR44 { get; set; }

        /// <summary>
        /// FF24 - Channel control / Channel On-Off / Channel Volume (R/W)
        /// </summary>
        public static Mappable NR50 { get; set; }

        /// <summary>
        /// FF25 - Panning / Sound output terminal selection
        /// </summary>
        public static Mappable NR51 { get; set; }

        /// <summary>
        /// FF26 - Panning - Sound ON/OFF - CANNOT WRITE TO OTHER SOUND REGISTERS IF BIT7 IS 0!
        /// </summary>

        public static Mappable NR52 { get; set; }

        /// <summary>
        /// FF40 - LCD Control
        /// </summary>
        public static Mappable LCDC { get; set; }

        /// <summary>
        /// FF41 - LCD Status
        /// </summary>
        public static Mappable STAT { get; set; }

        /// <summary>
        /// FF42 - BG Scroll Y
        /// </summary>
        public static Mappable ScrollY { get; set; }

        /// <summary>
        /// FF43 - BG Scroll X
        /// </summary>
        public static Mappable ScrollX { get; set; }

        /// <summary>
        /// FF44 - LCD Y Coordinate (read only)
        /// </summary>
        public static Mappable LY { get; set; }

        /// <summary>
        /// FF45
        /// </summary>
        public static Mappable LYC { get; set; } 

        /// <summary>
        /// FF46 - (DMA Control)
        /// </summary>
        public static Mappable DMA { get; set; } 

        /// <summary>
        /// FF47 - Background Palette
        /// </summary>

        public static Mappable BGP { get; set; }

        /// <summary>
        /// FF48 - Object Palette 0 (DMG)
        /// </summary>
        public static Mappable OBP0 { get; set; }

        /// <summary>
        /// FF49 - Object Palette 1 (DMG)
        /// </summary>
        public static Mappable OBP1 { get; set; }

        /// <summary>
        /// FF4A - Window Y Position
        /// </summary>
        public static Mappable WY { get; set; }

        /// <summary>
        /// FF4B - Window X Position
        /// </summary>
        public static Mappable WX { get; set; }
    }
}
