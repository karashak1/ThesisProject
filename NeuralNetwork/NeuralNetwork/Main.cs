using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Collector;
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
            Console.WriteLine(ini);
            var twit = new Twitter(ini);
            //twit.testStream();
            var testCount = 100;
            twit.populateData(testCount);
            
            while (twit.Data.Count() < testCount - 1) {
                Console.WriteLine(twit.Data.Count());
                System.Threading.Thread.Sleep(5000);
            }
            twit.writeDateToFile(@"C:\Users\Kevin\Documents\TestJson\data.txt");
            //twit.testStream("Disney");
            
            Tweets data = new Tweets();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamReader sr = new StreamReader(@"C:\Users\Kevin\Documents\TestJson\data.txt"))
            using (JsonTextReader jr = new JsonTextReader(sr)) {
                if(!jr.Read() || jr.TokenType != JsonToken.StartArray) {
                    throw new Exception("Expected start of array");
                }
                while (jr.Read()) {
                    if (jr.TokenType == JsonToken.EndArray) break;
                    var item = serializer.Deserialize<TweetData>(jr);
                    data.add(item);
                }
            }
            Console.WriteLine("done");
            foreach (var tweet in data) {
                Console.WriteLine(tweet.text);
            }
            
            //twit.getData();
            //while (twit.Data.Length <= 1) ;
            //Console.WriteLine(twit.Data+"\n");

            //Twitter.testStream(ini);
            var news = new News();
            news.getGoogleNews("Disney");
            foreach (var article in news.Data) {
                Console.WriteLine(article.Title+" "+article.publishData.ToString() );
            }
            Console.WriteLine("Done with everything");
            Console.ReadLine();
        }

    }
}
