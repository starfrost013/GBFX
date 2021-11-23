using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// Mappable
    /// 
    /// Defines a mappable register
    /// 
    /// we don't need inheritance here
    /// </summary>
    public class Mappable
    {
        /// <summary>
        /// Friendly name of this register.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Start of this register.
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// End of this register.
        /// </summary>
        public ushort End { get; set; }

        /// <summary>
        /// If this register is mirrored, this is how it is mirrored.
        /// </summary>
        public ushort RedirAmount { get; set; }

        public byte[] Data { get; set; }

        public ushort Length
        {
            get
            {
                return (ushort)(End - Start);
            }
        }
    }
}
