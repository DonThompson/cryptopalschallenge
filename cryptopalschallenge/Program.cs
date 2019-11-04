using CommandLine;
using System;

namespace cryptopalschallenge
{
    class Program
    {
        public class Options
        {
            [Option('c', "challenge", Required = true, HelpText = "Set challenge number.  01 through 64 (as available)")]
            public int Challenge { get; set; }

            [Option('x', "value1", Required = false, HelpText = "First value to be provided to the challenge, as needed.")]
            public string Value1 { get; set; }

            [Option('y', "value2", Required = false, HelpText = "Second value to be provided to the challenge, as needed.")]
            public string Value2 { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));
        }

        static void RunOptionsAndReturnExitCode(Options opts)
        {
            switch (opts.Challenge) { 
                case 1:
                    //-c 01 -x 49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d
                    Challenge01 c1 = new Challenge01();
                    Console.WriteLine(c1.DoChallenge01(opts.Value1));
                    break;
                case 2:
                    //-c 02 -x 1c0111001f010100061a024b53535009181c -y 686974207468652062756c6c277320657965
                    Challenge02 c2 = new Challenge02();
                    Console.WriteLine(c2.DoChallenge02(opts.Value1, opts.Value2));
                    break;
                case 3:
                    //-c 03 -x 1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736
                    Challenge03 c3 = new Challenge03();
                    Console.WriteLine(c3.DoChallenge03(opts.Value1));
                    break;
                case 4:
                    //-c 04
                    Challenge04 c4 = new Challenge04();
                    Console.WriteLine(c4.DoChallenge04());
                    break;
                case 5:
                    //-c 05 -x "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal" -y "ICE"
                    Challenge05 c5 = new Challenge05();
                    Console.WriteLine(c5.DoChallenge05(opts.Value1, opts.Value2));
                    break;
                default:
                    Console.WriteLine("Invalid options specified.");
                    break;
            }

        }

    }
}
