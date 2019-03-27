using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace BmpHashValidation {
    public class BmpHashValidator : IDisposable {

        public void WriteHashInto (string pathToBmp) {
            byte[] bytes = File.ReadAllBytes (pathToBmp);
            byte[] hash = GetPictureHash (pathToBmp);

            Log (bytes, "bmp-before.txt");
            Log (hash, "hash.txt");

            bytes[6] = hash[0];
            bytes[7] = hash[1];
            bytes[8] = hash[2];
            bytes[9] = hash[3];

            bytes[30] = hash[4];
            bytes[31] = hash[5];
            bytes[32] = hash[6];
            bytes[33] = hash[7];

            bytes[46] = hash[8];
            bytes[47] = hash[9];
            bytes[48] = hash[10];
            bytes[49] = hash[11];
            bytes[50] = hash[12];
            bytes[51] = hash[13];
            bytes[52] = hash[14];
            bytes[53] = hash[15];

            File.WriteAllBytes (pathToBmp, bytes);
            Log (bytes, "bmp-after.txt");
        }
        private byte[] GetPictureHash (string pathToBmp) {
            byte[] data = File.ReadAllBytes (pathToBmp).Skip (54).ToArray ();

            using (var md5 = MD5.Create ()) {
                return md5.ComputeHash (data);
            }
        }

        private byte[] GetHashFrom (string pathToBmp) {
            byte[] bytes = File.ReadAllBytes (pathToBmp);
            byte[] hash = new byte[16];

            hash[0] = bytes[6];
            hash[1] = bytes[7];
            hash[2] = bytes[8];
            hash[3] = bytes[9];

            hash[4] = bytes[30];
            hash[5] = bytes[31];
            hash[6] = bytes[32];
            hash[7] = bytes[33];
            hash[8] = bytes[46];
            hash[9] = bytes[47];
            hash[10] = bytes[48];
            hash[11] = bytes[49];
            hash[12] = bytes[50];
            hash[13] = bytes[51];
            hash[14] = bytes[52];
            hash[15] = bytes[53];

            return hash;
        }

        public bool HashIsValid (string pathToBmp) {
            byte[] hash = GetHashFrom (pathToBmp);
            byte[] factHash = GetPictureHash (pathToBmp);

            Log (hash, "test-hash.txt");
            Log (factHash, "test-fact-hash.txt");

            return Enumerable.SequenceEqual (hash, factHash);
        }

        private void Log (byte[] data, string path) {
            File.WriteAllLines (path, data.Select (b => b.ToString ()));
        }
        public void Dispose () { }
    }
}
