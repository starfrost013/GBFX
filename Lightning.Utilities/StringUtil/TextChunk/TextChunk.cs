using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// TextChunk
    /// 
    /// June 13, 2021 (modified June 16, 2021)
    /// 
    /// Defines a text chunk. A text chunk is a chunk of text separated from other chunks by a newline or a space.
    /// </summary>
    public class TextChunk
    {
        public string Text { get; set; }

        public int Length => Text.Length; 

        public TextChunk()
        {
            Text = "";
        }
    }
}
