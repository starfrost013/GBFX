using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBFX.Core
{
    /// <summary>
    /// Game Boy
    /// 
    /// ©1989-1998 Nintendo
    /// </summary>
    public class DMG
    {
        /// <summary>
        /// Determines if the Game Boy is powered on. 
        /// </summary>
        public bool PoweredOn { get; set; }

        /// <summary>
        /// Sharp LR35902 CPU
        /// </summary>
        public DMGCPU CPU { get; set; }

        public DMGPPU PPU { get; set; }

        public const string ClassName = "DMG"; 

        public DMG()
        {
            ErrorManager.Init();
            Logging.Log($"DMG Core Init, core version {DMGCPU.DMGCPU_CORE_VERSION}...", ClassName);
            CPU = new DMGCPU();
        }

        public void PowerOn()
        {
            Logging.Log("Powering on Game Boy...", ClassName);
            PoweredOn = true;

            // todo: literally everything 

            // Boot the CPU.
            CPU.Boot(); 

            while (PoweredOn)
            {
                // CPU Main
                CPU.ExecuteNext(); 
            }
        }
    }
}
