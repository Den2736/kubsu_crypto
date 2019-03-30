using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DiffieHellman {
    public class DiffieHellmanParticipant {
        public string Name { get; set; }
        public int Base { get; set; }
        public int Mod { get; set; }
        public BigInteger PartnerMix { get; set; }
        private string _log { get; set; }

        private int _secret { get; set; }
        private BigInteger _mix { get; set; }
        private BigInteger _sharedKey { get; set; }

        public DiffieHellmanParticipant(string name) {
            Name = name;
            _log = $"{Name}.log";
            File.WriteAllText(_log, "");
        }

        public void BroadcastGeneratorsIn(string filename) {
            File.AppendAllLines(filename, new string[] {
                "Base = " + Base.ToString(),
                    "Mod = " + Mod.ToString()
            });
        }
        public void GetGeneratorsFrom(string filename) {
            var lines = File.ReadAllLines(filename);
            string baseLine = lines.First(s => s.ToLower().Contains("base"));
            string modLine = lines.First(s => s.ToLower().Contains("mod"));

            Base = int.Parse(baseLine.Split('=').Last());
            Mod = int.Parse(modLine.Split('=').Last());
            Log($"Received base({Base}) and mod({Mod})");
        }

        public void GenerateSecret() {
            _secret = new Random().Next(0, 10000);
            Log($"My secret: {_secret}");
        }

        public void Mix() {
            _mix = BigInteger.Pow(Base, _secret) % Mod;
            Log($"My mix: {_mix}");
        }

        public void BroadcastMixIn(string filename) {
            File.AppendAllText(filename, $"[{Name}].Mix = {_mix}{Environment.NewLine}");
        }
        public void GetPartnersMixFrom(string filename) {
            var lines = File.ReadAllLines(filename);
            var reMix = new Regex(@"\[\w+].Mix");
            var reName = new Regex(@"\[\w+]");

            var lineWithPartnerMix = lines.First(s =>
                reMix.Matches(s).Any()
                && reName.Match(s).Value.Replace("[", "").Replace("]", "") != Name);
            PartnerMix = BigInteger.Parse(lineWithPartnerMix.Split('=').Last().Trim());
            Log($"Partner's mix: {PartnerMix}");
        }

        public void GenerateSharedKey() {
            _sharedKey = BigInteger.Pow(PartnerMix, _secret) % Mod;
            Log($"SharedKey: {_sharedKey}");
        }

        public override string ToString() => Name;

        private void Log(string message) {
            File.AppendAllText(_log, message + Environment.NewLine);
        }
    }
}
