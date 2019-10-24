using CommandLine;
using CommandLine.Text;
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
            //TODO - arg check
            //TODO - split by challenges

            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));


            


        }

        static void RunOptionsAndReturnExitCode(Options opts)
        {
            switch (opts.Challenge) { 
                case 1:
                    //-c 01 -x 49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d
                    Challenge01 c = new Challenge01();
                    c.DoChallenge01(opts.Value1);
                    break;
                default:
                    Console.WriteLine("Invalid options specified.");
                    break;
            }
        }

    }
}
