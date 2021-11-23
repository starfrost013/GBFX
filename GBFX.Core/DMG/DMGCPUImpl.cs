using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{   
    /// <summary>
    /// DMG CPU instruction implementation
    /// 
    /// Opcode register order: B, C, D, E, H, L, M, A
    /// 
    /// 16bit opcode register order: BC / DE / HL / SP
    /// </summary>
    public partial class DMGCPU // :GBComponent?
    {


        #region Power-On

        public void Boot()
        {

            Logging.Log("CPU Power-on in progress...");

            // The initial contents of WRAM, HRAM, and ExtRAM (if present)
            // are non-deterministic and highly dependent on model and even atmospheric conditions around the system.
            // We will fill it with random garbage for our purposes. The BootROM clears it anyway.


            Random Rnd = new Random();

            // Fill WRAMDMG with garbage.
            for (int i = 0; i < Memory.WRAMDMG.Length; i++)
            {
                byte Byte = Memory.WRAMDMG[i];

                Byte = (byte)Rnd.Next(0x00, 0x100); // 0x100 not included (weird c# quirk)
            }

            // fill WRAM section 2 (DMG) / Bankable RAM (CGB) with garbage..
            for (int i = 0; i < Memory.WRAMCGB.Length; i++)
            {
                byte Byte = Memory.WRAMCGB[i];

                Byte = (byte)Rnd.Next(0x00, 0x100); // 0x100 not included (weird c# quirk)
            }

            // fill HRAM with garbage..
            for (int i = 0; i < Memory.HRAM.Length; i++)
            {
                byte Byte = Memory.HRAM[i];

                Byte = (byte)Rnd.Next(0x00, 0x100); // 0x100 not included (weird c# quirk)
            }

            // and finally fill EXTRAM with garbage!
            for (int i = 0; i < Memory.ExtRam.Length; i++)
            {
                byte Byte = Memory.ExtRam[i];

                Byte = (byte)Rnd.Next(0x00, 0x100); // 0x100 not included (weird c# quirk)
            }


            Logging.Log("Loading Boot ROM...");

            Boot_LoadBootROM();

            Logging.Log("Booting NOW!");
        }

        private void Boot_LoadBootROM()
        {
            // Load the boot rom. 
            for (ushort i = 0; i < DMGBoot.BootROM.Length; i++)
            {
                byte DMGBootByte = DMGBoot.BootROM[i];

                Memory.Write(i, DMGBootByte);
            }
        }

        #endregion

        #region Main Execute Cycle
        public void ExecuteNext()
        {
            //todo: instructions take multiple cycles
            //so we need to fake this

            byte Opcode = Memory.Read(PC);

            Logging.Log($"Executing opcode 0x{Opcode.ToString("X2")} at {PC.ToString("X2")}");

            switch (Opcode)
            {
                case 0x00: // nop (do nothing) 
                    break;
                case 0x03: // inc rr    x3  8 cycles    N/A
                    BC++;
                    break;
                case 0x04: // inc b    04  4 cycles    z0h-
                    B = Inc(B);
                    break;
                case 0x05: // dec b    05  4 cycles    z1h-
                    B = Dec(B);
                    break;
                case 0x0B: // dec rr    x3  8 cycles    N/A
                    BC--;
                    break;
                case 0x0C: // inc c    0c  4 cycles    z0h-
                    C = Inc(C);
                    break;
                case 0x0D: // dec c    0d  4 cycles    z1h-
                    C = Dec(C);
                    break;
                case 0x10: // STOP
                    ErrorManager.ThrowError(ClassName, "InvalidOpcodeException", "https://pbs.twimg.com/media/E5jlgW9XIAEKj0t.png:large - not even trying this one");
                    break;
                case 0x13: // inc rr    x3  8 cycles    N/A
                    DE++;
                    break;
                case 0x14:
                    D = Inc(D); // inc d    14  4 cycles    z0h-
                    break;
                case 0x15:
                    D = Dec(D); // dec d    15  4 cycles    z1h-
                    break;
                case 0x18: // dec rr    x3  8 cycles    N/A
                    DE--;
                    break;
                case 0x1C: // inc e    1C  4 cycles    z0h-
                    E = Inc(E);
                    break;
                case 0x1D: // dec e    1D  4 cycles    z1h-
                    E = Dec(E);
                    break;
                case 0x23: // inc rr    x3  8 cycles    N/A
                    HL++;
                    break;
                case 0x24: // inc h    24  4 cycles    z0h-
                    H = Inc(H);
                    break;
                case 0x25: // dec h    25  4 cycles    z1h-
                    H = Dec(H);
                    break;
                case 0x2B: // dec rr    x3  8 cycles    N/A
                    HL--;
                    break;
                case 0x2C: // inc l    2C  4 cycles    z0h-
                    L = Inc(L);
                    break;
                case 0x2D: // dec l    2D  4 cycles    z1h-
                    L = Dec(L);
                    break;
                case 0x33: // inc rr    x3  8 cycles    N/A
                    SP++;
                    break;
                case 0x34: // inc (hl)    34  12 cycles    z0h-
                    byte AtHL = Memory.Read(HL);
                    Memory.Write(HL, AtHL++);
                    break;
                case 0x35: // dec (hl)    35  12 cycles    z0h-
                    byte AtHL2 = Memory.Read(HL);
                    Memory.Write(HL, AtHL2--);
                    break;
                case 0x3B: // dec rr    x3  8 cycles    N/A
                    SP--;
                    break;
                case 0x3C: // inc l    3C  4 cycles    z0h-
                    L = Inc(L);
                    break;
                case 0x3D: // dec (hl)    3D  4 cycles    z0h-
                    L = Dec(L);
                    break;
                case 0x40: // useless (ld b,b)    4 cycles   N/A
                    B = B;
                    break;
                case 0x41: // ld b,c  4 cycles    N/A
                    B = C;
                    break;
                case 0x42: // ld b,d  4 cycles    N/A
                    B = D;
                    break;
                case 0x43: // ld b,e  4 cycles    N/A 
                    B = E;
                    break;
                case 0x44: // ld b,h  4 cycles    N/A
                    B = H;
                    break;
                case 0x45: // ld b,l  4 cycles    N/A
                    B = L;
                    break;
                case 0x46: // ld b,(HL) 4 cycles  N/A
                    B = Memory.Read(HL);
                    break;
                case 0x47: // ld b,a  4 cycles    N/A
                    B = A;
                    break;
                case 0x48: // ld c,b  4 cycles    N/A
                    C = B;
                    break;
                case 0x49: // useless (ld c,c)  4 cycles    N/A
                    C = C;
                    break;
                case 0x4A: // ld c,d  4 cycles    N/A
                    C = D;
                    break;
                case 0x4B: // ld c,e  4 cycles    N/A
                    C = E;
                    break;
                case 0x4C: // ld c,h  4 cycles    N/A
                    C = H;
                    break;
                case 0x4D: // ld c,l  4 cycles    N/A
                    C = L;
                    break;
                case 0x4E: // ld c,(HL) 4 cycles  N/A
                    C = Memory.Read(HL);
                    break;
                case 0x4F: // ld c,a  4 cycles    N/A
                    C = A;
                    break;
                case 0x50: // ld d,b  4 cycles    N/A
                    D = B;
                    break;
                case 0x51: // ld d,c  4 cycles    N/A
                    D = C;
                    break;
                case 0x52: // useless (ld d,d) 4 cycles    N/A
                    D = D;
                    break;
                case 0x53: // ld d,e  4 cycles    N/A
                    D = E;
                    break;
                case 0x54: // ld d,h 4 cycles    N/A
                    D = H;
                    break;
                case 0x55: // ld d,l 4 cycles    N/A
                    D = L;
                    break;
                case 0x56: // ld d,(HL) 4 cycles  N/A
                    D = Memory.Read(HL);
                    break;
                case 0x57: // ld d,a 4 cycles    N/A
                    D = A;
                    break;
                case 0x58: // ld e,b 4 cycles    N/A
                    E = B;
                    break;
                case 0x59: // ld e,c 4 cycles    N/A
                    E = C;
                    break;
                case 0x5A: // ld e,d 4 cycles    N/A
                    E = D;
                    break;
                case 0x5B: // useless (ld e,e) 4 cycles    N/A
                    E = E;
                    break;
                case 0x5C: // ld e,h  4 cycles    N/A
                    E = H;
                    break;
                case 0x5D: // ld e,l  4 cycles    N/A
                    E = L;
                    break;
                case 0x5E: // ld e,(HL) 4 cycles  N/A
                    E = Memory.Read(HL);
                    break;
                case 0x5F: // ld e,a  4 cycles    N/A
                    E = A;
                    break;
                case 0x60: // ld h,b  4 cycles    N/A
                    H = B;
                    break;
                case 0x61: // ld h,c  4 cycles    N/A
                    H = C;
                    break;
                case 0x62: // ld h,d  4 cycles    N/A
                    H = D;
                    break;
                case 0x63: // ld h,e  4 cycles    N/A
                    H = E;
                    break;
                case 0x64: // useless (ld h,h) 4  cycles    N/A
                    H = H;
                    break;
                case 0x65: // ld h,l  4 cycles    N/A
                    H = L;
                    break;
                case 0x66: // ld h,(HL) 4 cycles  N/A
                    H = Memory.Read(HL);
                    break;
                case 0x67: // ld h,a  4 cycles    N/A
                    H = A;
                    break;
                case 0x68: // ld l,b  4 cycles    N/A
                    L = B;
                    break;
                case 0x69: // ld l,c  4 cycles    N/A
                    L = C;
                    break;
                case 0x6A: // ld l,d  4 cycles    N/A
                    L = D;
                    break;
                case 0x6B: // ld l,e  4 cycles    N/A
                    L = E;
                    break;
                case 0x6C: // ld l,h  4 cycles    N/A
                    L = H;
                    break;
                case 0x6D: // useless (ld l,l)  4 cycles    N/A
                    L = L;
                    break;
                case 0x6E: // ld h,(HL) 4 cycles  N/A
                    L = Memory.Read(HL);
                    break;
                case 0x6F: // ld l,a  4 cycles    N/A
                    L = A;
                    break;
                case 0x70:
                    Memory.Write(HL, B); // ld (hl, b)  8 cycles    N/A
                    break;
                case 0x71:
                    Memory.Write(HL, C); // ld (hl, c)  8 cycles    N/A
                    break;
                case 0x72:
                    Memory.Write(HL, D); // ld (hl, d)  8 cycles    N/A
                    break;
                case 0x73:
                    Memory.Write(HL, E); // ld (hl, e)  8 cycles    N/A
                    break;
                case 0x74:
                    Memory.Write(HL, H); // ld (hl, h)  8 cycles    N/A
                    break;
                case 0x75:
                    Memory.Write(HL, L); // ld (hl, l)  8 cycles    N/A
                    break;
                case 0x77:
                    Memory.Write(HL, A); // ld (hl, a)  8 cycles    N/A
                    break;
                case 0x78: // ld a,b  4 cycles    N/A
                    A = B;
                    break;
                case 0x79: // ld a,c  4 cycles    N/A
                    A = C;
                    break;
                case 0x7A: // ld a,d  4 cycles    N/A
                    A = D;
                    break;
                case 0x7B: // ld a,e  4 cycles    N/A
                    A = E;
                    break;
                case 0x7C: // ld a,h  4 cycles    N/A
                    A = H;
                    break;
                case 0x7D: // ld a,l  4 cycles    N/A
                    A = L;
                    break;
                case 0x7E: // ld a,(hl)  4 cycles    N/A
                    A = Memory.Read(HL); // Fix 2021-11-23
                    break;
                case 0x7F: // useless (ld a,a)  4 cycles    N/A
                    A = A;
                    break;
                case 0x80: // add a,b   4 cycles    z0hc
                    Add(B);
                    break;
                case 0x81: // add a,c   4 cycles    z0hc
                    Add(C);
                    break;
                case 0x82: // add a,d   4 cycles    z0hc
                    Add(D);
                    break;
                case 0x83: // add a,e   4 cycles    z0hc
                    Add(E);
                    break;
                case 0x84: // add a,h   4 cycles    z0hc
                    Add(H);
                    break;
                case 0x85: // add a,l   4 cycles    z0hc
                    Add(L);
                    break;
                case 0x86: // add a,(hl)   4 cycles    z0hc
                    Add(Memory.Read(HL));
                    break;
                case 0x87: // add a,a   4 cycles    z0hc
                    Add(A);
                    break;
                case 0x88:
                    
                case 0x90: // sub a,b  4 cycles    z1hc
                    Sub(B);
                    break;
                case 0x91: // sub a,c  4 cycles    z1hc
                    Sub(C);
                    break;
                case 0x92: // sub a,d  4 cycles    z1hc
                    Sub(D);
                    break;
                case 0x93: // sub a,e  4 cycles    z1hc
                    Sub(E);
                    break;
                case 0x94: // sub a,h  4 cycles    z1hc
                    Sub(H);
                    break;
                case 0x95: // sub a,l  4 cycles    z1hc
                    Sub(L);
                    break;
                case 0x96: // sub a,(hl)  4 cycles    z1hc
                    Sub(Memory.Read(HL));
                    break;
                case 0x97: // sub a,a  4 cycles    z1hc
                    Sub(A);
                    break;
                default: // invalid opcodes - implement glitch opcodes one day?
                    ErrorManager.ThrowError(ClassName, "InvalidOpcodeException", $"Attempted to execute invalid opcode 0x{Opcode.ToString("X2")} at 0x{PC.ToString("X2")}!\nMaybe implement these in the future.");
                    break;
            }

            PC++;
#if DEBUG
            PrintDebugInformation();
#endif
        }

        #endregion

        #if DEBUG
        public void PrintDebugInformation()
        {
            Logging.Log("CPU State:", ClassName); 
            Logging.Log($"PC=0x{PC.ToString("X2")}", ClassName);
            Logging.Log($"A=0x{A.ToString("X2")}, B=0x{B.ToString("X2")}", ClassName);
            Logging.Log($"C=0x{C.ToString("X2")}, D=0x{D.ToString("X2")}", ClassName);
            Logging.Log($"E=0x{E.ToString("X2")}, Flags=0x{F.ToString("X2")}", ClassName);
            Logging.Log($"L=0x{L.ToString("X2")}, BC=0x{BC.ToString("X2")}", ClassName);
            Logging.Log($"DE=0x{DE.ToString("X2")}, HL=0x{HL.ToString("X2")}", ClassName);
            Logging.Log($"SP=0x{SP.ToString("X2")}", ClassName);
            Logging.Log($"HALTed={Halted}, Cycles={Cycles}", ClassName);
            Logging.Log($"Flags: Z:{FlagZ}, N:{FlagN}, H:{FlagH}, C:{FlagC}", ClassName);
        }


        #endif

        #region Instruction Implementations

        public byte Inc(byte B) // Z0H-, 8-bit increment (8-bit registers ONLY!)
        {
            ushort Result = (byte)(B + 1);

            SetZ(Result); // set the zero flag if the result is zero
            
            FlagN = false; // N is false

            SetH(B, 1); // check for carry from bit 3

            return (byte)Result; 
        }

        public byte Dec(byte B) // Z0H-, 8-bit decreement (8-bit registers ONLY!)
        {
            ushort Result = (byte)(B - 1);

            SetZ(Result); // set the zero flag if the result is zero

            FlagN = true; // N is false

            SetH_Sub(B, 1); // check for carry from bit 3

            return (byte)Result;
        }

        public void Add(ushort B)  // Z0HC, 8-bit add 
        {
            ushort Result = (ushort)(A + B); // (the game boy only supports adding or subtracting from the A register)
            SetZ(Result);

            FlagN = false; // not subtracting

            SetH(B, 1);
            SetC(Result);
            A = (byte)Result;
            return;

        }

        public void Adc(ushort B)  // Z0HC, 8-bit add + carry
        {
            // manually carry

            int Carry = FlagC ? 1 : 0;

            ushort Result = (ushort)(A + B + Carry);
            SetZ(Result);

            FlagN = false; // not subtracting

            if (FlagC)
            {
                SetH_Carry(B, 1);
            }
            else
            {
                SetH(B, 1);
            }
            
            SetC(Result);
            A = (byte)Result;
            return;

        }


        public void Sub(ushort B)  // Z1HC, 8-bit dec 
        {
            ushort Result = (ushort)(A - B); // (the game boy only supports adding or subtracting from the A register)
            SetZ(Result);

            FlagN = false; //  subtracting
            SetH_Sub(B, 1);

            SetC(Result);
            A = (byte)Result;
            return;

        }

        public void Sbc(ushort B)  // Z0HC, 8-bit add + carry
        {
            // manually carry

            int Carry = FlagC ? 1 : 0;

            ushort Result = (ushort)(A - B - Carry);
            SetZ(Result);

            FlagN = true; // not subtracting

            if (FlagC)
            {
                SetH_SubCarry(B, 1);
            }
            else
            {
                SetH_Sub(B, 1);
            }

            SetC(Result);
            A = (byte)Result;
            return;

        }

        #endregion

        #region Support

        /// <summary>
        /// Sets the Zero flag based on the value of the <see cref="byte"/> <paramref name="A"/>.
        /// </summary>
        /// <param name="A"></param>
        public void SetZ(byte A) => FlagZ = ((byte)A == 0);

        /// <summary>
        /// Sets the Zero flag based on the value of the <see cref="ushort"/> <paramref name="A"/>.
        /// </summary>
        /// <param name="A"></param>
        public void SetZ(ushort A) => FlagZ = ((byte)A == 0);

        /// <summary>
        /// Sets the Carry flag based on the value of the <see cref="ushort"/> <paramref name="A"/>.
        /// </summary>
        /// <param name="A"></param>
        public void SetC(ushort A) => FlagZ = (A >> 4) == 0;

        /// <summary>
        /// Sets the Half-Carry flag based on the value of the two <see cref="byte"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="A"></param>
        public void SetH(byte N1, byte N2) => FlagH = ((N1 & 0xF) + (N2 & 0xF)) > 0xF;

        /// <summary>
        /// Sets the Half-Carry flag based on the value of the two <see cref="ushort"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH(ushort N1, ushort N2) => FlagH = ((N1 & 0xFFF) + (N2 & 0xFFF)) > 0xFFF;

        /// <summary>
        /// Sets the Half-Carry flag for a subtraction operation based on the value of the two <see cref="byte"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_Sub(byte N1, byte N2) => FlagH = (N1 & 0xF) < (N2 & 0xF);

        /// <summary>
        /// Sets the Half-Carry flag for a subtraction operation based on the value of the two <see cref="ushort"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_Sub(ushort N1, ushort N2) => FlagH = (N1 & 0xFFF) < (N2 & 0xFFF);

        /// <summary>
        /// Sets the Half-Carry flag for a carry operation based on the value of the two <see cref="byte"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_Carry(byte N1, byte N2) => FlagH = ((N1 & 0xF) + (N2 & 0xF)) >= 0xF; // carry implies >=

        /// <summary.
        /// Sets the Half-Carry flag for a carry operation based on the value of the two <see cref="ushorts"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_Carry(ushort N1, ushort N2) => FlagH = ((N1 & 0xFFF) + (N2 & 0xFFF)) >= 0xFFF; // carry implies >=

        /// <summary>
        /// Sets the Half-Carry flag for a subtraction operation based on the value of the two <see cref="byte"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_SubCarry(byte N1, byte N2)
        {
            int Carry = FlagC ? 1 : 0; // manual carry
            FlagH = (N1 & 0xF) < (N2 & 0xF) + Carry;
        }

        /// <summary>
        /// Sets the Half-Carry flag for a subtraction operation based on the value of the two <see cref="ushort"/>s <paramref name="N1"/> and <paramref name="N2"/>.
        /// </summary>
        /// <param name="N1"></param>
        /// <param name="N2"></param>
        public void SetH_SubCarry(ushort N1, ushort N2)
        {
            int Carry = FlagC ? 1 : 0; // manual carry
            FlagH = (N1 & 0xF) < (N2 & 0xF) + Carry;
        }


        // carry, subcarry


        #endregion
    }
}
