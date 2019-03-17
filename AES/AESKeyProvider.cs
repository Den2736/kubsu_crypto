using GaluaField;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AES
{
    /// <summary>
    /// Provides session (round) keys
    /// </summary>
    public partial class AESKeyProvider
    {
        private readonly byte[] Key;
        private int RoundCount =>
            Key.Length * 8 == 128 ? 10
            : Key.Length * 8 == 192 ? 12
            : 14;

        public List<byte[]> SessionKeys { get; private set; }
        private int KeyWordLen => Key.Length / 4;

        private GF GF { get; set; }

        public AESKeyProvider(byte[] key)
        {
            Key = key;
            GF = new GF("100011011");  // x8 + x4 + x3 + x + 1
            SessionKeys = new List<byte[]>() { key };
            GenSessionKeys();
        }

        private void GenSessionKeys()
        {
            for (int round = 1; round <= RoundsCount; round++)
            {
                var keyWords = SplitKeyByWords(SessionKeys[round-1]);
                byte[] temp = GetTempWord(keyWords.Last(), round);
                int extendedKeyLen = keyWords.Count + 4;

                for (int i = keyWords.Count; i < extendedKeyLen; i++)
                {
                    byte[] word = null;

                    if (i % 4 != 0)
                    {
                        word = keyWords[i - 1].Select((b, j) => (byte)(b ^ keyWords[i - 4][j])).ToArray();
                    }
                    else
                    {
                        word = temp.Select((t, j) => (byte)(t ^ keyWords[i - 4][j])).ToArray();
                    }

                    keyWords.Add(word);
                }

                SessionKeys.Add(keyWords.GetRange(4, 4).SelectMany(b => b).ToArray());
            }
        }

        public byte[] GetTempWord(byte[] word, int round)
        {
            byte[] rcon = RCon(round);
            return SubWord(RotWord(word)).Select((b, i) => (byte)(b ^ rcon[i])).ToArray();
        }
        private byte[] RotWord(byte[] word)
        {
            return word.Select((b, i) => word[(i + 3) % KeyWordLen]).ToArray();
        }
        private byte[] SubWord(byte[] word)
        {
            return word.Select(b => SubBytesTransformationTable.TransformationFor(b)).ToArray();
        }
        private byte[] RCon(int round)
        {
            int grade = round - 1;
            string pn = "1" + new string(Enumerable.Range(0, grade).Select(x => '0').ToArray());
            string mod = GF.Mod(pn);

            return Enumerable.Range(0, KeyWordLen).Select(x => x == 0 ? Convert.ToByte(mod, 2) : (byte)0).ToArray();
        }

        private List<byte[]> SplitKeyByWords(byte[] key)
        {
            var keyWords = new List<byte[]>();

            for (int i = 0; i < 4; i++)
            {
                var word = new List<byte>();
                for (int j = 0; j < KeyWordLen; j++)
                {
                    word.Add(key[i * KeyWordLen + j]);
                }
                keyWords.Add(word.ToArray());
            }

            return keyWords;
        }
    }
}
