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
                    BIT(0, B);
                    return;
                case 0x41:
                    BIT(0, C);
                    return; 
                case 0x42:
                    BIT(0, D);
                    return;
                case 0x43:
                    BIT(0, E);
                    return;
                case 0x44:
                    BIT(0, H);
                    return;
                case 0x45:
                    BIT(0, L);
                    return;
                case 0x46:
                    BIT(0, Memory.Read(HL));
                    return;
                case 0x47:
                    BIT(0, A);
                    return;
                case 0x48:
                    BIT(1, B);
                    return;
                case 0x7C:
                    BIT(7, H);
                    return; 
            }

        }

        public void BIT(byte Value, byte Reg) // z01- 
        {
            //todo: flag z
            FlagZ = (Reg & Value) == 0; 
            FlagH = false;
            FlagN = true; 
        }
    }
}
