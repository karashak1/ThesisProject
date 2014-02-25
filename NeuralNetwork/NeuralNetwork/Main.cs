using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Collector;



namespace NeuralNetwork {
    class Program {
        static void Main(string[] args) {
            String ini;
            if (args.Length < 1) {
                Console.WriteLine("Please enter the name of the ini file");
                ini = Console.ReadLine();
            }
            else
                ini = args[0];
            Console.WriteLine(ini);
            var twit = new Twitter(ini);
            //twit.testStream();
            var testCount = 100;
            //twit.populateData(testCount);
            /*
            while (twit.Data.Count() < testCount - 1) {
                Console.WriteLine(twit.Data.Count());
                System.Threading.Thread.Sleep(5000);
            }
            */
            twit.testStream("Disney");
            Console.WriteLine("done");

            //twit.getData();
            //while (twit.Data.Length <= 1) ;
            //Console.WriteLine(twit.Data+"\n");

            //Twitter.testStream(ini);
            var news = new News();
            news.getGoogleNews("Disney");
            foreach (var article in news.Data) {
                Console.WriteLine(article.Title+" "+article.publishData.ToString() );
            }
            Console.ReadLine();
        }

    }
}
