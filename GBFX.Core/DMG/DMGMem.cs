using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// DMG address space.
    /// </summary>
    public class DMGMem
    {

        public DMGMem()
        {
            IORegisters = new List<Mappable>();
            MappingSetup();
        }

        public void MappingSetup()
        {
            Logging.Log($"Loading address space mappings...");
        }

        #region Address Space Values
        /// <summary>
        /// 0000-3fff: 16KB ROM Bank 0
        /// 
        /// Game code is usually loaded to 0150 by bootrom but this can be changed in the header
        /// 0-100 is usually bootrom (see <see cref="DMGBoot"/>.
        /// </summary>
        public byte[] ROM0 = new byte[0x4000];

        /// <summary>
        /// 4000-7fff: ROM Bank (user-switchable)
        /// </summary>
        public byte[] ROM1 = new byte[0x4000];

        /// <summary>
        /// 8000-9fff: VRAM
        /// </summary>
        public byte[] VRAM = new byte[0x2000];

        /// <summary>
        /// A000-BFFF: ExtRAM 
        /// 
        /// Usually used for MBC or SRAM
        /// </summary>
        public byte[] ExtRam = new byte[0x2000];

        /// <summary>
        /// C000-CFFF: WRAM
        /// </summary>
        public byte[] WRAMDMG = new byte[0x1000];

        /// <summary>
        /// D000-DFFF: 
        /// 
        /// Not used (DMG)
        /// Bankable WRAM (4KB) (CGB)
        /// </summary>
        public byte[] WRAMCGB = new byte[0x1000];

        /// <summary>
        /// Object attribute memory (0x9F)
        /// </summary>
        public byte[] OAM = new byte[0x100];

        // EchoRAM implemented as mapping 
        // IO registers are implemented as mappableregisters

        public byte[] HRAM = new byte[0x80];

        #endregion

        #region Mappables

        /// <summary>
        /// TEMP - mappablecollection
        /// 
        /// simple until it works
        /// </summary>
        private List<Mappable> IORegisters { get; set; }


        /// <summary>
        /// Reads from RAM.
        /// </summary>
        /// <param name="Position">The position to read from. </param>
        public byte Read(ushort Position)
        {
            //todo: mapping code

            string UnimpString = $"Attempt to read from unimplemented memory region 0x{Position.ToString("X2")}), returning 0xFF";
            // TODO: rambounds for cgb

            // One day this will go to the MBCs.
            if (Position <= 0x3FFF) 
            {
                return ROM0[Position];
            }
            else if (Position > 0x4000 && Position <= 0x7FFF)
            {
                return ROM1[Position & 0x3FFF];
            }
            else if (Position > 0x8000 && Position <= 0x9FFF)
            {
                return VRAM[Position & 0x1FFF];
            }
            else if (Position > 0x9FFF && Position <= 0xBFFF)
            {
                return ExtRam[Position & 0x1FFF];
            }
            else if (Position > 0xBFFF && Position <= 0xCFFF
            || Position > 0xE000 && Position < 0xFDFF) // EchoRAM
            {
                return WRAMDMG[Position & 0xFFF];
            }
            else if (Position > 0xCFFF && Position <= 0xDFFF)
            {
                return WRAMCGB[Position & 0xFFF];
            }
            else if (Position > 0xFDFF && Position <= 0xFE9F)
            {
                return OAM[Position & 0x9F];
            }
            else if (Position > 0xFE9F && Position <= 0xFEFF)
            {
                return 0x00; // Not implemented in original hardware. 
            }
            else if (Position > 0xFF7F && Position <= 0xFFFE)
            {
                return HRAM[Position & 0x7F];
            }
            else
            {
                Logging.Log(UnimpString);
                return 0xFF; 
            }
        }


        /// <summary>
        /// Reads a 16-bit word from RAM. (TODO: CPU-AWARE) 
        /// </summary>
        /// <param name="Position">The position to read from.</param>
        /// <returns>A value containing the 16-bit value read from RAM.</returns>
        public ushort Read16(ushort Position)
        {
            byte B1 = Read(Position);
            byte B2 = Read((ushort)(Position + 1));

            // DMG is little endian 
            ushort Value = BitConverter.ToUInt16(new byte[2] { B1, B2 });
            return Value; 
        }

        /// <summary>
        /// Writes to RAM
        /// </summary>
        /// <param name="Position">The position to write to. </param>
        public void Write(ushort Position, byte Value)
        {
            //todo: mapping code
            //ex: Echo RAM, I/O Registers

            string UnimpString = $"Attempt to write to unimplemented memory region 0x{Position.ToString("X2")}) (value {Value})";
            // TODO: rambounds for cgb

            // One day this will go to the MBCs.

            // also todo: don't write to rom (split into ROMWrite and Write)
            if (Position <= 0x3FFF)
            {
                ROM0[Position] = Value; 
            }
            else if (Position > 0x3FFF && Position <= 0x7FFF)
            {
                ROM1[Position & 0x3FFF] = Value;
            }
            else if (Position > 0x7FFF && Position <= 0x9FFF)
            {
                VRAM[Position & 0x1FFF] = Value;
            }
            else if (Position > 0x9FFF && Position <= 0xBFFF)
            {
                ExtRam[Position & 0x1FFF] = Value;
            }
            else if (Position > 0xBFFF && Position <= 0xCFFF)
            {
                WRAMDMG[Position & 0xFFF]= Value;
            }
            else if (Position > 0xCFFF && Position <= 0xDFFF)
            {
                WRAMCGB[Position & 0xFFF] = Value;
            }
            else if (Position > 0xFDFF && Position <= 0xFE9F)
            {
                OAM[Position & 0x9F] = Value;
            }
            else if (Position > 0xFE9F && Position <= 0xFEFF)
            {
                return; // does nothing????
            }
            else if (Position > 0xFF7F && Position <= 0xFFFE)
            {
                HRAM[Position & 0x7F] = Value; 
            }
            else
            {
                Logging.Log(UnimpString);
                return; 
            }
        }

        public void Write(ushort Position, ushort Value)
        {
            // not the most intelligent way to write a 16bit word
            // x86 and DMG/lr35902 use same endianness
            // should work

            byte[] Bytes = BitConverter.GetBytes(Value);

            foreach (byte Byte in Bytes)
            {
                Write(Position, Byte); 
            }

        }

        #endregion

        #region Temporary Code until MappingCollection Implementation

        public Mappable GetMappingAtPoint(ushort Start, ushort End)
        {
            foreach (Mappable Mapping in IORegisters)
            {
                if (Mapping.Start == Start
                && Mapping.End == End)
                {
                    return Mapping;
                }
            }

            return null; 
        }

        #endregion
    }
}
