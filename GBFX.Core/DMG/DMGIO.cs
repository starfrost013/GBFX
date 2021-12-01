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
        public static Mappable ScrollY { get; set; } // FF42

        public static Mappable ScrollX { get; set; } // FF43

        public static Mappable LY { get; set; } // FF44, read-only

        public static Mappable LYC { get; set; } // FF45

        public static Mappable DMA { get; set; } // FF46 (Direct Memory Access)


    }
}
