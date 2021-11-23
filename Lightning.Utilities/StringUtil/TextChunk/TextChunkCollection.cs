using NuRender.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    public class TextChunkCollection : IEnumerable
    {

        public List<TextChunk> Chunks { get; set; }

        public int Length { get; set; }

        public TextChunkCollection()
        {
            Chunks = new List<TextChunk>();
        }

        public TextChunkCollection(List<TextChunk> ProspectiveTextChunkList)
        {
            Chunks = new List<TextChunk>();

            foreach (TextChunk TextChunk in ProspectiveTextChunkList)
            {
                Chunks.Add(TextChunk);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();

        }

        public TextChunkCollectionEnumerator GetEnumerator()
        {
            return new TextChunkCollectionEnumerator(Chunks);
        }

        public void Add(object Obj)
        {
            Type ObjType = Obj.GetType();

            if (ObjType != typeof(TextChunk))
            {
                throw new Exception("Attempted to add a non-TextChunk to a TextChunkCollection");
            }
            else
            {
                TextChunk TC = (TextChunk)Obj;

                if (TC.Text == null)
                {
                    throw new Exception("Attempted to add a TextChunk with no Text to a TextChunkCollection");
                }
                else
                {
                    Length += TC.Length;
                    Chunks.Add(TC);
                }
            }
        }

        /// <summary>
        /// Concatenates the contents of this TextChunkCollection.
        /// </summary>
        /// <returns></returns>
        public string Concatenate()
        {
            StringBuilder SB = new StringBuilder(); 

            foreach (TextChunk Chk in Chunks)
            {
                SB.Append(Chk.Text);
            }

            return SB.ToString(); 
        }

        public int Count => Chunks.Count; 
    
        public TextChunk this[int i] => Chunks[i];

        
    }

    public class TextChunkCollectionEnumerator : IEnumerator
    {

        public TextChunkCollectionEnumerator(List<TextChunk> ProspectiveTextChunkList) => Chunks = ProspectiveTextChunkList;

        public List<TextChunk> Chunks { get; set; }

        private int Position = -1;
        public TextChunk Current
        {
            get
            {
                return Chunks[Position];
            }
            set
            {
                throw new InvalidOperationException(); 
            }

        }

        public bool MoveNext()
        {
            Position++;
            return (Position < Chunks.Count);
        }

        public void Reset() => Position = -1;

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
}
