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
            
            var twit = new Twitter(options.ini);
            Console.WriteLine("Connected to twitter");
            var news = new News();
            string date = DateTime.Now.ToString("M-d-yyyy");
            if (!Directory.Exists(options.twitterStore + date)) {
                Directory.CreateDirectory(options.twitterStore + date);
            }
            if (!Directory.Exists(options.newsStore + date)) {
                Directory.CreateDirectory(options.newsStore + date);
            }
            //TODO do the same for the news articles
            
            string time = DateTime.Now.ToString("HHmmss");
            
            foreach (var x in options.terms) {
                string path = options.twitterStore +date;
                string fileName = time+"-"+x+".txt";
                string file = Path.Combine(path, fileName);
                twit.populateData(x, options.tweetCount);
                while (twit.Data.Count() < options.tweetCount - 1) {
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("Topic:"+x +" count="+twit.Data.Count());
                }
                Console.WriteLine("Collection for " + x + " done, writing to file.");
                twit.writeDateToFile(file);
                Console.WriteLine("Writing file done");
            }
            
            Console.WriteLine("collecting generic data");
            twit.populateData(1000);
            while (twit.Data.Count() < 1000 - 1) {
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("Count=" + twit.Data.Count());
            }
            twit.writeDateToFile(Path.Combine(options.twitterStore+date, time+".txt"));
            foreach (var x in options.terms) {
                string path = options.newsStore + date;
                string fileName = time + "-" + x + ".txt";
                string file = Path.Combine(path, fileName);
                news.getGoogleNews(x);
                news.writeDateToFile(file);
            }
            Console.WriteLine("Collection Done");
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
