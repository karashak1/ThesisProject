using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Collector;
using System.Net.Http;
using TweetSharp.Serialization;
using TweetSharp;
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
            //var twit = new Twitter();
            //twit.getData();
            //while (twit.Data.Length <= 1) ;
            //Console.WriteLine(twit.Data+"\n");

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            //var blah = JsonConvert.DeserializeObject<TwitterInfo>(ini);
            TwitterInfo blah;
            using (StreamReader sr = new StreamReader(ini)) 
            using (JsonTextReader jr = new JsonTextReader(sr)) {
                blah = serializer.Deserialize<TwitterInfo>(jr);
            }
            Console.WriteLine(blah.ConsumerKey);
            var info = new TwitterClientInfo();
            info.ConsumerKey = blah.ConsumerKey;
            info.ConsumerSecret = blah.ConsumerSercert;
            var client = new TwitterService(info);

            client.AuthenticateWith(blah.AccessToken, blah.AccessTokenSecret);
            client.SendTweet("this is a test");
            var news = new News();
            news.getNews();
            Console.ReadLine();
        }
    }
}
