using System;
using System.Linq;

namespace GaluaField
{
    /// <summary>
    /// Provides mathematical functions in Galua Field 
    /// with specified forming polynomial
    /// </summary>
    public class GF
    {
        public string FormingPolynomial { get; }

        public GF(string formingPolynomial)
        {
            if (formingPolynomial.Any(c => c != '0' && c != '1'))
            {
                throw new ArgumentException("There must be only 0 and 1 in Galua Field.");
            }
            FormingPolynomial = formingPolynomial;
        }

        public string Add(string lhs, string rhs)
        {
            return Mod(SimpleAdd(lhs, rhs));
        }
        public byte Add(byte lhs, byte rhs)
        {
            return Convert.ToByte(Add(Convert.ToString(lhs, 2), Convert.ToString(rhs, 2)), 2);
        }
        private string SimpleAdd(string lhs, string rhs)
        {
            while (lhs.Length < rhs.Length) lhs += '0';
            while (rhs.Length < lhs.Length) rhs += '0';

            string sum = new string(lhs.Select((c, i) => c != rhs[i] ? '1' : '0').ToArray());
            return RemoveInsignificantZeros(sum);
        }

        public string Mult(string lhs, string rhs)
        {
            return Mod(SimpleMult(lhs, rhs));
        }
        public byte Mult(byte lhs, byte rhs)
        {
            return Convert.ToByte(Mult(Convert.ToString(lhs, 2), Convert.ToString(rhs, 2)), 2);
        }
        private string SimpleMult(string lhs, string rhs)
        {
            if (rhs.Length > lhs.Length)
            {
                // swap
                string temp = rhs;
                rhs = lhs;
                lhs = temp;
            }

            var k = rhs.Select((rc, i) =>
                new string(
                    Enumerable.Range(0, i).Select(x => '0')
                    .Concat(
                        lhs.Select(lc => lc == rc && rc == '1' ? '1' : '0'))
                    .Concat(
                        Enumerable.Range(0, rhs.Length - i - 1).Select(x => '0'))
                .ToArray()));

            string sum = k.Aggregate((k1, k2) => SimpleAdd(k1, k2));
            return RemoveInsignificantZeros(sum);
        }

        public string Mod(string pn)
        {
            string k1 = pn;
            string k2 = "";

            while (k1.Length >= FormingPolynomial.Length)
            {
                k2 = FormingPolynomial;
                while (k2.Length < k1.Length) k2 += '0';

                k1 = new string(k1.Select((c, i) => c != k2[i] ? '1' : '0').ToArray());
                k1 = RemoveInsignificantZeros(k1);
            }

            return k1;
        }

        private string RemoveInsignificantZeros(string s)
        {
            while (s.First() == '0' && s.Length > 1)
            {
                s = s.Remove(0, 1);
            }

            return s;
        }
    }
}
