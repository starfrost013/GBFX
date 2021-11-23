using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    public static class ScreenUtil
    {

        /// <summary>
        /// Converts a WPF position-independent font size to a pixel size.
        /// 
        /// WPF ALWAYS uses 96dpi!!!!
        /// </summary>
        /// <param name="WPFFontSize"></param>
        /// <returns></returns>
        public static double WPFToPixels(double WPFFontSize) => WPFFontSize * (96.0 / 72.0);
    }
}
