using System;
using System.Collections.Generic;
using System.Linq;

namespace BBS {
    public class BBS_Algorithm {

        static void Main (string[] args) {
            int period = 0;
            var pdn = GeneratePDN (3, 5, out period);
            pdn.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"Period = {period}");

            pdn = GeneratePDN (29, 31, out period);
            pdn.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"Period = {period}");

            pdn = GeneratePDN (109, 113, out period);
            pdn.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"Period = {period}");

            pdn = GeneratePDN (5, 113, out period);
            pdn.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"Period = {period}");

            pdn = GeneratePDN (3079, 3067, out period);
            pdn.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"Period = {period}");
        }

        public static List<long> GeneratePDN (int p, int q, out int period) {
            var pdn = new List<long> () { new Random ().Next (2, p * q) };

            do {
                pdn.Add ((long) Math.Pow (pdn.Last (), 2) % (p * q));
            } while (pdn.Count (x => x == pdn.Last ()) <= 1);

            period = pdn.LastIndexOf (pdn.Last ()) - pdn.IndexOf (pdn.Last ());

            pdn.RemoveAt(pdn.Count - 1);
            return pdn;
        }
    }
}