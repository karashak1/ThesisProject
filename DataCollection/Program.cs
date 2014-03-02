using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection {
    class Program {
        static void Main(string[] args) {
            List<string> terms = new List<string>();
            string ini = null;
            string twitterStore = null;
            string newsStore = null;
            var options = new Options();
            var result = CommandLine.Parser.Default.ParseArguments(args, options);
            
            Console.WriteLine(ini);
            Console.WriteLine(twitterStore);
            Console.WriteLine(newsStore);
            Console.ReadLine();
            //var twit = new Twitter(ini);
            //var news = new News();
        }
    }

    class Options {
        [Option('i', "ini", Required = true,
          HelpText = "Ini file for twitter feed.")]
        public string ini { get; set; }

        [Option('s', "twitter", Required = true,
          HelpText = "Directory to store twitter data.")]
        public string twitterStore { get; set; }

        [Option('n', "news", Required = true,
          HelpText = "Directory to store news data.")]
        public string newsStore { get; set; }

        [OptionList( 't',"terms", Required = true)]
        public IList<string> terms { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
