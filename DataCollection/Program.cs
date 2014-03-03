using CommandLine;
using CommandLine.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collector;

namespace DataCollection {
    class Program {
        static void Main(string[] args) {
            List<string> terms = new List<string>();
            var options = new Options();
            var result = CommandLine.Parser.Default.ParseArguments(args, options);
            
            Console.WriteLine(options.ini);
            Console.WriteLine(options.twitterStore);
            Console.WriteLine(options.newsStore);
            Console.WriteLine(options.tweetCount);
            foreach (var x in options.terms) {
                Console.WriteLine(x);
            }
            var twit = new Twitter(options.ini);
            var news = new News();
            string date = DateTime.Now.ToString("M-d-yyyy");
            if (!Directory.Exists(options.twitterStore + date)) {
                Directory.CreateDirectory(options.twitterStore + date);
            }
            string time = DateTime.Now.ToString("HH-mm-ss");
            foreach (var x in options.terms) {
                string path = options.twitterStore+date;
                string fileName = time+"-"+x+".json";
                string file = Path.Combine(path, fileName);
                //Console.WriteLine(file);
                twit.populateData(x, options.tweetCount);
                while (twit.Data.Count() < options.tweetCount - 1) {
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("Topic:"+x +" count="+twit.Data.Count());
                }
                twit.writeDateToFile(fileName);
            }
            Console.ReadLine();
            
            
        }
    }

    class Options {
        [Option('i', "ini", Required = true,
          HelpText = "Ini file for twitter feed.")]
        public string ini { get; set; }

        [Option('c', "count", Required = true,
          HelpText = "Ini file for twitter feed.")]
        public int tweetCount { get; set; }

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
