using System;
using System.Linq;
using System.Text;

namespace LRSR_Tests
{
    public class Ring
    {
        public string Data { get; }

        public Ring(string s)
        {
            Data = s;
        }

        public char this[int i] => Data[i % Data.Length];

        public override bool Equals(object obj)
        {
            if (obj is Ring)
            {
                var ring = obj as Ring;
                var sb = new StringBuilder(Data);

                for (int i = 0; i < Data.Length; i++)
                {
                    if (sb.ToString() != ring.Data)
                    {
                        sb.Append(sb.ToString().First())
                            .Remove(0, 1);
                    }
                    else return true;
                }

                return false;
            }

            return base.Equals(obj);
        }
    }
}
