using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PrimeGenerator = PrimeNumberGenerator.PrimeNumberGenerator;

namespace DiffieHellman {
    class Program {
        static DiffieHellmanParticipant cp; // current participant
        static string partnerName;
        static readonly string channel = "channel.txt";
        static readonly DirectoryInfo cd = new DirectoryInfo(Environment.CurrentDirectory);
        static void Main(string[] args) {
            Clear();

            Console.WriteLine("Hello, Diffie-Hellman participant!");
            Console.WriteLine("What's your name?");

            string name = Console.ReadLine();
            cp = new DiffieHellmanParticipant(name);

            Console.Write($"What do you want to do? 1 - if you want to start to communicate, or 2 - if someone wants to communicate with you:");
            string choice = Console.ReadLine();

            partnerName = FindPartners().FirstOrDefault(p => p != cp.Name);

            while (partnerName == null) {
                Console.WriteLine($"It looks like there is no one you could contact");
                Console.WriteLine("Press any key to seek one more time.");
                Console.ReadKey();
                partnerName = FindPartners().FirstOrDefault(p => p != cp.Name);
            }

            if (choice == "1") {
                Console.WriteLine($"Press any key to start communicate with {partnerName}");
                Console.ReadKey();

                cp.Base = new Random().Next(0, 10000);
                cp.Mod = (int) new PrimeGenerator().Generate();
                cp.BroadcastGeneratorsIn(channel);
                Console.WriteLine("Generators was shared.");
            }
            else {
                Console.WriteLine($"Press any key to communicate with {partnerName}");
                Console.ReadKey();

                cp.GetGeneratorsFrom(channel);
                Console.WriteLine("Generators was received.");
            }
            cp.GenerateSecret();
            Console.WriteLine($"Secret was generated.");

            cp.Mix();
            Console.WriteLine($"Mixed.");

            cp.BroadcastMixIn(channel);
            Console.WriteLine($"Mix was shared.");

            Console.WriteLine("Press any key to get partner's mix:");
            Console.ReadKey();

            cp.GetPartnersMixFrom(channel);
            Console.WriteLine($"Partner's mix was received.");

            cp.GenerateSharedKey();
            Console.WriteLine($"Got shared key!");
        }

        static void Clear() {
            var cd = new DirectoryInfo(Environment.CurrentDirectory);
            cd.GetFiles("*.log").ToList().ForEach(f => f.Delete());
            cd.GetFiles("*.txt").ToList().ForEach(f => f.Delete());

            File.WriteAllText(channel, "");
        }
        static IEnumerable<string> FindPartners() {
            return cd.GetFiles("*.log").Select(f => f.Name.Split('.').First().Trim());
        }
    }
}
