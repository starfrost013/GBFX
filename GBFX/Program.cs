using GBFX.Core; 
using System;

namespace GBFX
{
    /// <summary>
    /// GBFX
    /// 
    /// Research Game Boy emulator
    /// 
    /// ©2021 starfrost
    /// 
    /// Not going to be very accurate or good but a start. Hopefully we can get some games to run. 
    /// </summary>
    class Program
    {
        public static DMG GameBoy { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("GBFX Research GB Emulator");
            Console.WriteLine("©2021,2022 starfrost");

            GameBoy = new DMG();
            GameBoy.PowerOn();
        }
    }
}
