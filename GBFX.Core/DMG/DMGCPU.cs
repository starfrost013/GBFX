using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// Implements the Sharp LR35902
    /// </summary>
    public partial class DMGCPU
    {

        public DMGCPU()
        {
            Memory = new DMGMem();
        }

        #region Debug Information

        /// <summary>
        /// Cycle count for DEBUG
        /// </summary>
        public int Cycles { get; set; }

        /// <summary>
        /// The version of the DMG CPU core.
        /// </summary>
        public const int DMGCPU_CORE_VERSION = 18;


        public static string ClassName => "CPU Core";
        #endregion

        #region State

        /// <summary>
        /// Determines if the CPU is halted using the HALT instruction.
        /// </summary>
        public bool Halted { get; set; }

        /// <summary>
        /// Determines if the CPU is stopped using our simplified implementation of the STOP instruction.
        /// </summary>
        public bool Stopped { get; set; }

        #endregion 

        #region Memory

        public DMGMem Memory { get; set; }

        #endregion

        #region Registers
        /// <summary>
        /// The A register.
        /// </summary>
        private byte A { get; set; }

        /// <summary>
        /// The B register.
        /// </summary>
        private byte B { get; set; }

        /// <summary>
        /// The C register.
        /// </summary>
        private byte C { get; set; }

        /// <summary>
        /// The D register.
        /// </summary>
        private byte D { get; set; }

        /// <summary>
        /// TheE register.
        /// </summary>
        private byte E { get; set; }

        /// <summary>
        /// The F register.
        /// </summary>
        private byte F { get; set; }

        /// <summary>
        /// The H register.
        /// </summary>
        private byte H { get; set; }

        /// <summary>
        /// The L register.
        /// </summary>
        private byte L { get; set; }

        /// <summary>
        /// The program counter - determines where the CPU currently is.
        /// </summary>
        private ushort PC { get; set; }

        /// <summary>
        /// The stack pointer - determines where the stack currently is.
        /// </summary>
        private ushort SP { get; set; }

        #endregion

        #region Register pair implementation

        /// <summary>
        /// AF register pair
        /// </summary>
        private ushort AF { get { return (ushort)(A << 8 | F); } set { A = (byte)(value >> 8); F = (byte)(value & 0xF0); } }

        /// <summary>
        /// BC register pair
        /// </summary>
        private ushort BC { get { return (ushort)(B << 8 | C); } set { B = (byte)(value >> 8); C = (byte)(value); } }

        /// <summary>
        /// DE register pair
        /// </summary>
        private ushort DE { get { return (ushort)(D << 8 | E); } set { D = (byte)(value >> 8); E = (byte)(value); } }

        /// <summary>
        /// HL register pair
        /// </summary>
        private ushort HL { get { return (ushort)(H << 8 | L); } set { H = (byte)(value >> 8); L = (byte)(value); } }

        #endregion

        #region Flags - contain result of MOST RECENT instr!

        /// <summary>
        /// Zero flag
        /// </summary>
        private bool FlagZ { get { return (F & 0x80) != 0; } set { F = value ? (byte)(F | 0x80) : (byte)(F & ~0x80); } } // bit 1 

        /// <summary>
        /// Subtraction flag
        /// </summary>
        private bool FlagN { get { return (F & 0x40) != 0; } set { F = value ? (byte)(F | 0x40) : (byte)(F & ~0x40); } } // bit 2 

        /// <summary>
        /// Half-Carry flag (BCD!!!)
        /// </summary>
        private bool FlagH { get { return (F & 0x20) != 0; } set { F = value ? (byte)(F | 0x20) : (byte)(F & ~0x20); } } // bit 3 

        /// <summary>
        /// Carry flag
        /// </summary>
        private bool FlagC { get { return (F & 0x10) != 0; } set { F = value ? (byte)(F | 0x10) : (byte)(F & ~0x10); } } // bit 4 

        #endregion
    }
}
