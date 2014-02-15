using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Collector;
using System.Net.Http;
using TweetSharp.Serialization;
using Newtonsoft.Json;

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
            //var twit = new Twitter();
            //twit.getData();
            //while (twit.Data.Length <= 1) ;
            //Console.WriteLine(twit.Data+"\n");
            
            var blah = JsonConvert.DeserializeObject<TwitterInfo>(ini);
            Console.WriteLine(blah.ConsumerKey);
            var news = new News();
            news.getNews();
            Console.ReadLine();
        }
    }
}
