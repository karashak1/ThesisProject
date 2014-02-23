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
            twit.testStream();
            //twit.populateData(1000);
            /*
            while (twit.Data.Count() < 999) {
                Console.WriteLine(twit.Data.Count());
                System.Threading.Thread.Sleep(5000);
            }
             */
            Console.WriteLine("done");
            //twit.getData();
            //while (twit.Data.Length <= 1) ;
            //Console.WriteLine(twit.Data+"\n");

            //Twitter.testStream(ini);
            var news = new News();
            var results = news.getNews("Disney");
            foreach (var article in results) {
                Console.WriteLine(article.Title+" "+article.publishData.ToString() );
            }
            Console.ReadLine();
        }

    }
}
