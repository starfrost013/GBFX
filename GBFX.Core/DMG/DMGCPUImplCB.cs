using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// Implements Prefix-CB instructions (Sharp LR35902)
    /// </summary>
    public partial class DMGCPU
    {
        public void ExecuteNext_CB(byte CBInstr)
        {
            switch (CBInstr)
            {
                case 0x40:
                    Bit(0, B);
                    return;
                case 0x41:
                    Bit(0, C);
                    return; 
                case 0x42:
                    Bit(0, D);
                    return;
                case 0x43:
                    Bit(0, E);
                    return;
                case 0x44:
                    Bit(0, H);
                    return;
                case 0x45:
                    Bit(0, L);
                    return;
                case 0x46:
                    Bit(0, Memory.Read(HL));
                    return;
                case 0x47:
                    Bit(0, A);
                    return;
                case 0x48:
                    Bit(1, B);
                    return;
                case 0x49:
                    Bit(1, C);
                    return;
                case 0x4A:
                    Bit(1, D);
                    return;
                case 0x4B:
                    Bit(1, E);
                    return;
                case 0x4C:
                    Bit(1, H);
                    return;
                case 0x4D:
                    Bit(1, L);
                    return;
                case 0x4E:
                    Bit(1, Memory.Read(HL));
                    return;
                case 0x4F:
                    Bit(1, A);
                    return;
                case 0x50:
                    Bit(2, B);
                    return;
                case 0x51:
                    Bit(2, C);
                    return;
                case 0x52:
                    Bit(2, D);
                    return;
                case 0x53:
                    Bit(2, E);
                    return;
                case 0x54:
                    Bit(2, H);
                    return;
                case 0x55:
                    Bit(2, L);
                    return;
                case 0x56:
                    Bit(2, Memory.Read(HL));
                    return;
                case 0x57:
                    Bit(2, A);
                    return;
                case 0x58:
                    Bit(3, B);
                    return;
                case 0x59:
                    Bit(3, C);
                    return;
                case 0x5A:
                    Bit(3, D);
                    return; 
                case 0x5B:
                    Bit(3, E);
                    return;
                case 0x5C:
                    Bit(3, H);
                    return;
                case 0x5D:
                    Bit(3, L);
                    return;
                case 0x5E:
                    Bit(3, Memory.Read(HL));
                    return;
                case 0x5F:
                    Bit(3, A);
                    return;
                case 0x60:
                    Bit(4, B);
                    return;
                case 0x61:
                    Bit(4, C);
                    return;
                case 0x62:
                    Bit(4, D);
                    return;
                case 0x63:
                    Bit(4, E);
                    return;
                case 0x64:
                    Bit(4, H);
                    return;
                case 0x65:
                    Bit(4, L);
                    return;
                case 0x66:
                    Bit(4, Memory.Read(HL));
                    return;
                case 0x67:
                    Bit(4, A);
                    return;
                case 0x68:
                    Bit(5, B);
                    return;
                case 0x69:
                    Bit(5, C);
                    return;
                case 0x6A:
                    Bit(5, D);
                    return;
                case 0x6B:
                    Bit(5, E);
                    return;
                case 0x6C:
                    Bit(5, H);
                    return;
                case 0x6D:
                    Bit(5, L);
                    return;
                case 0x6E:
                    Bit(5, Memory.Read(HL));
                    return;
                case 0x6F:
                    Bit(5, A);
                    return;
                case 0x70:
                    Bit(6, B);
                    return;
                case 0x71:
                    Bit(6, C);
                    return;
                case 0x72:
                    Bit(6, D);
                    return;
                case 0x73:
                    Bit(6, E);
                    return;
                case 0x74:
                    Bit(6, H);
                    return;
                case 0x75:
                    Bit(6, L);
                    return;
                case 0x76:
                    Bit(6, Memory.Read(HL));
                    return;
                case 0x77:
                    Bit(6, A);
                    return;
                case 0x78:
                    Bit(7, B);
                    return;
                case 0x79:
                    Bit(7, C);
                    return;
                case 0x7A:
                    Bit(7, D);
                    return;
                case 0x7B:
                    Bit(7, E);
                    return;
                case 0x7C:
                    Bit(7, H);
                    return;
                case 0x7D:
                    Bit(7, L);
                    return;
                case 0x7E:
                    Bit(7, Memory.Read(HL));
                    return;
                case 0x7F:
                    Bit(7, A);
                    return;
            }

        }

        public void Bit(byte Value, byte Reg) // z01- 
        {
            //todo: flag z
            FlagZ = (Reg & Value) == 0; 
            FlagH = false;
            FlagN = true; 
        }
    }
}
