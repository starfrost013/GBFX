using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// GBComponent
    /// 
    /// November 29, 2021
    /// 
    /// Handles basec
    /// </summary>
    public abstract class GBComponent
    {
        public abstract string ClassName { get; }

        /// <summary>
        /// Current GB Addr space.
        /// </summary>
        public DMGMem Memory { get; set; }

        public virtual void PowerOn(DMGMem AddrSpace) // make memory static?
        {
            Memory = AddrSpace;
        }

    }
}
