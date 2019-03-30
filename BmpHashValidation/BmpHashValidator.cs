using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace BmpHashValidation {
    public class BmpHashValidator : IDisposable {

        public void WriteHash(string source, string target) {
            const int hashLen = 16;

            byte[] bytes = File.ReadAllBytes(source);
            int imageStartsFrom = BitConverter.ToInt32(bytes.Skip(10).Take(4).ToArray(), 0);
            byte[] imgBytes = bytes.Skip(imageStartsFrom).ToArray();

            var clearedImageBytes = Smear(imgBytes, Enumerable.Range(0, hashLen).Select(x => (byte) 0).ToArray());
            var clearedBytes = new byte[][] {
                bytes.Take(imageStartsFrom).ToArray(),
                    clearedImageBytes
            }.SelectMany(b => b).ToArray();
            File.WriteAllBytes("cleared.bmp", clearedBytes);
            Log(clearedImageBytes, "cleared.txt");

            byte[] hash = MD5.Create().ComputeHash(imgBytes);
            Log(hash, "hash.txt");

            var hashedImageBytes = Smear(clearedImageBytes, hash);
            var hashedBytes = new byte[][] {
                bytes.Take(imageStartsFrom).ToArray(),
                    hashedImageBytes
            }.SelectMany(b => b).ToArray();
            File.WriteAllBytes(target, hashedBytes);
            Log(hashedImageBytes, "hashed.txt");
        }

        private byte[] Smear(byte[] data, byte[] smeared) {
            for (int i = 0; i < smeared.Length; i++) {
                string bits = Convert.ToString(smeared[i], 2);
                while (bits.Length < 8) bits = new string(bits.Prepend('0').ToArray());

                for (int j = 0; j < 8; j++) {
                    string dataBits = Convert.ToString(data[i * 8 + j], 2);
                    while (dataBits.Length < 8) dataBits = new string(dataBits.Prepend('0').ToArray());
                    dataBits = new string(dataBits.Take(7).ToArray());

                    dataBits += bits[j];
                    data[i * 8 + j] = Convert.ToByte(dataBits, 2);
                }
            }

            return data;
        }
        private byte[] GetSmearedDataFrom(byte[] data, int smearedDataLen) {
            var res = new List<byte>();

            for (int i = 0; i < smearedDataLen; i++) {
                string smearedBits = "";
                for (int j = 0; j < 8; j++) {
                    string dataBits = Convert.ToString(data[i * 8 + j], 2);
                    smearedBits += dataBits.Last();
                }
                res.Add(Convert.ToByte(smearedBits, 2));
            }
            return res.ToArray();
        }

        public bool HashIsValid(string pathToBmp) {
            const int hashLen = 16;

            byte[] bytes = File.ReadAllBytes(pathToBmp);
            int imageStartsFrom = BitConverter.ToInt32(bytes.Skip(10).Take(4).ToArray(), 0);

            byte[] imgBytes = bytes.Skip(imageStartsFrom).ToArray();
            Log(imgBytes, "test-image.txt");
            byte[] smearedHash = GetSmearedDataFrom(imgBytes, hashLen);
            Log(smearedHash, "test-smeared-hash.txt");

            var clearedImageBytes = Smear(imgBytes, Enumerable.Range(0, hashLen).Select(x => (byte) 0).ToArray());
            Log(clearedImageBytes, "test-cleared.txt");
            byte[] hash = MD5.Create().ComputeHash(clearedImageBytes);
            Log(hash, "test-hash.txt");

            return Enumerable.SequenceEqual(hash, smearedHash);
        }

        private void Log(byte[] data, string path) {
            File.WriteAllLines(path, data.Select(b => b.ToString()));
        }
        public void Dispose() { }
    }
}
