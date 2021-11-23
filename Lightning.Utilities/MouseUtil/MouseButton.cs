using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// MouseButton
    /// 
    /// July 8, 2021
    /// 
    /// Defines an enumeration of mouse buttons. Designed to be an SDL independent equivalent of SDL.SDL_MouseButtonEvent
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// Defines the left mouse button.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Defines the middle mouse button.
        /// </summary>
        Middle = 1,

        /// <summary>
        /// Defines the right mouse button.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Defines the back mouse button on some mice.
        /// </summary>
        Back = 3,

        /// <summary>
        /// Defines the forward mouse button on some mice.
        /// </summary>
        Forward = 4
    }
}
