using GaluaField;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AES
{
    // for watch use: block.Select(row => row.Select(b => Convert.ToString(b, 16)))

    public partial class AES
    {
        private const int TextBlockSize = 128;
        private const int SessionKeykSize = 128;
        private readonly byte[] Key;
        private int RoundCount =>
            Key.Length * 8 == 128 ? 10
            : Key.Length * 8 == 192 ? 12
            : 14;

        private GF GF { get; set; }

        public AES(byte[] key)
        {
            Key = key;
            GF = new GF("100011011");  // x8 + x4 + x3 + x + 1
        }

        public byte[] Encrypt(byte[] data)
        {
            var blocks = SplitByBlocks(data);
            var keyProvider = new AESKeyProvider(Key);

            for (int blockIdx = 0; blockIdx < blocks.Count(); blockIdx++)
            {
                blocks[blockIdx] = AddRoundKey(blocks[blockIdx], Key);
            }

            for (int round = 1; round <= RoundsCount; round++)
            {
                for (int blockIdx = 0; blockIdx < blocks.Count(); blockIdx++)
                {
                    blocks[blockIdx] =
                        AddRoundKey(
                            MixCoumns(
                                ShiftRows(
                                    SubBytes(blocks[blockIdx]))), keyProvider.SessionKeys[round]);
                }
            }

            return blocks.SelectMany(block => block.SelectMany(b => b)).ToArray();
        }

        private byte[][] SubBytes(byte[][] block)
        {
            return Enumerable.Range(0, block.Length)
                .Select(row => block[row]
                    .Select(b => SubBytesTransformationTable.TransformationFor(b))
                    .ToArray())
                .ToArray();
        }
        private byte[][] ShiftRows(byte[][] block)
        {
            return Enumerable.Range(0, block.Length)
                .Select(row => block[row]
                    .Select((b, i) => block[row][(i + row) % block.Length])
                    .ToArray())
                .ToArray();
        }
        private byte[][] MixCoumns(byte[][] block)
        {
            var c = new byte[][]
            {
                new byte[]{ 0x02, 0x03, 0x01, 0x01 },
                new byte[]{ 0x01, 0x02, 0x03, 0x01 },
                new byte[]{ 0x01, 0x01, 0x02, 0x03 },
                new byte[]{ 0x03, 0x01, 0x01, 0x02 }
            };

            var r = new byte[][]
            {
                new byte[block.Length],
                new byte[block.Length],
                new byte[block.Length],
                new byte[block.Length],
            };

            for (int i = 0; i < block.Length; i++)
            {
                var column = block.Select(row => row[i]).ToArray();
                for (int j = 0; j < block.Length; j++)
                {
                    var row = c[j];
                    byte sum = 0;
                    for (int k = 0; k < block.Length; k++)
                    {
                        sum ^= GF.Mult(column[k], row[k]);
                    }
                    r[j][i] = sum;
                }
            }
            return r;
        }
        private byte[][] AddRoundKey(byte[][] block, byte[] key)
        {
            List<byte[]> keyWords = SplitKeyByWords(key);

            return block
                .Select((row, rowIdx) =>
                    row.Select((b, i) => (byte)(b ^ keyWords[i][rowIdx]))
                    .ToArray())
                .ToArray();
        }

        public byte[] Decrypt(byte[] data)
        {
            var blocks = SplitByBlocks(data);
            var keyProvider = new AESKeyProvider(Key);

            for (int round = RoundsCount; round > 0; round--)
            {
                for (int blockIdx = 0; blockIdx < blocks.Count(); blockIdx++)
                {
                    blocks[blockIdx] =
                        InvSubBytes(
                            InvShiftRows(
                               InvMixCoumns(
                                   AddRoundKey(blocks[blockIdx], keyProvider.SessionKeys[round]))));
                }
            }

            for (int blockIdx = 0; blockIdx < blocks.Count(); blockIdx++)
            {
                blocks[blockIdx] = AddRoundKey(blocks[blockIdx], Key);
            }

            return blocks.SelectMany(block => block.SelectMany(b => b)).ToArray();
        }

        private byte[][] InvMixCoumns(byte[][] block)
        {
            var c = new byte[][]
            {
                new byte[]{ 0x0e, 0x0b, 0x0d, 0x09 },
                new byte[]{ 0x09, 0x0e, 0x0b, 0x0d },
                new byte[]{ 0x0d, 0x09, 0x0e, 0x0b },
                new byte[]{ 0x0b, 0x0d, 0x09, 0x0e }
            };

            var r = new byte[][]
            {
                new byte[block.Length],
                new byte[block.Length],
                new byte[block.Length],
                new byte[block.Length],
            };

            for (int i = 0; i < block.Length; i++)
            {
                var column = block.Select(row => row[i]).ToArray();
                for (int j = 0; j < block.Length; j++)
                {
                    var row = c[j];
                    byte sum = 0;
                    for (int k = 0; k < block.Length; k++)
                    {
                        sum ^= GF.Mult(column[k], row[k]);
                    }
                    r[j][i] = sum;
                }
            }
            return r;
        }
        private byte[][] InvShiftRows(byte[][] block)
        {
            return Enumerable.Range(0, block.Length)
                .Select(row => block[row]
                    .Select((b, i) => block[row][(i - (row - block.Length)) % block.Length])
                    .ToArray())
                .ToArray();
        }
        private byte[][] InvSubBytes(byte[][] block)
        {
            return Enumerable.Range(0, block.Length)
                .Select(row => block[row]
                    .Select(b => SubBytesTransformationTable.InverseTransformationFor(b))
                    .ToArray())
                .ToArray();
        }


        private List<byte[][]> SplitByBlocks(byte[] data)
        {
            List<byte[][]> blocks = new List<byte[][]>();
            int blockRank = 4 * 4;
            int rowSize = (int)Math.Sqrt(blockRank);

            List<byte> bytes = new List<byte>(data);
            if (bytes.Count % blockRank != 0)
            {
                // add some trash
                for (int i = 0; i < bytes.Count % blockRank; i++)
                {
                    bytes.Add(0);
                }
            }

            var blocksArrays = new List<List<byte>>();
            for (int blockIdx = 0; blockIdx < bytes.Count / blockRank; blockIdx++)
            {
                blocksArrays.Add(bytes.GetRange(blockIdx * blockRank, blockRank));
            }

            foreach (var blockArray in blocksArrays)
            {
                List<byte[]> block = new List<byte[]>();
                for (int row = 0; row < rowSize; row++)
                {
                    block.Add(blockArray.GetRange(row * rowSize, rowSize).ToArray());
                }

                blocks.Add(block.ToArray());
            }

            return blocks;
        }
        private List<byte[]> SplitKeyByWords(byte[] key)
        {
            var keyWords = new List<byte[]>();
            var keyWordLen = key.Length / 4;

            for (int i = 0; i < 4; i++)
            {
                var word = new List<byte>();
                for (int j = 0; j < keyWordLen; j++)
                {
                    word.Add(key[i * keyWordLen + j]);
                }
                keyWords.Add(word.ToArray());
            }

            return keyWords;
        }
    }
}
