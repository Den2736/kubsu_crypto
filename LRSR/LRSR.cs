using System;
using System.Linq;
using System.Text;

namespace LRSR
{
    public class LRSR
    {
        public string Output(string pn)
        {
            var result = new StringBuilder();
            pn = new string(pn.Remove(0, 1).Reverse().ToArray());

            string initial = GetInitial(pn.Length);
            string current = initial;
            string roundResult = "";

            do
            {
                if (result.ToString().Length > Math.Pow(2, pn.Length) - 1)
                {
                    // wrong initial (it's not irreducible already)
                    initial = GetInitial(pn.Length);
                    current = initial;
                    result.Clear();
                }

                roundResult = MultAndXor(current, pn);
                result.Append(current.First());
                current = Shove(roundResult, current);
            }
            while (current != initial);

            return result.ToString();
        }

        public bool IsIrreducible(string pn)
        {
            return Output(pn).Length == Math.Pow(2, pn.Length-1) - 1;
        }

        private string Shove(string shoved, string s)
        {
            return s.Insert(s.Length, shoved).Remove(0, shoved.Length);
        }

        private string MultAndXor(string pn1, string pn2)
        {
            var p1 = pn1.Select(c => c == '1').ToList();
            var p2 = pn2.Select(c => c == '1').ToList();

            var mult = p1.Select((c, i) => c && p2[i]);
            bool xor = mult.Aggregate((b1, b2) => b1 ^ b2);
            if (xor) return "1";
            return "0";
        }

        private string GetInitial(int len)
        {
            var rnd = new Random();
            var sb = new StringBuilder();
            while (!sb.ToString().Contains("1"))
            {
                sb.Clear();
                for (int i = 0; i < len; i++)
                {
                    sb.Append(rnd.Next(2));
                }
            }
            return sb.ToString();
        }
    }
}
