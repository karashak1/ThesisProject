using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Collector;
using System.Net.Http;
using Newtonsoft.Json;
using TweetinCore.Interfaces.TwitterToken;
using Streaminvi;
using TwitterToken;
using LinqToTwitter;

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
            TwitterInfo LoginInfo;
            using (StreamReader sr = new StreamReader(ini)) 
            using (JsonTextReader jr = new JsonTextReader(sr)) {
                LoginInfo = serializer.Deserialize<TwitterInfo>(jr);
            }
            /*
            Console.WriteLine(blah.ConsumerKey);
            var token = new Token(LoginInfo.AccessToken, LoginInfo.AccessTokenSecret, LoginInfo.ConsumerKey, LoginInfo.ConsumerSercert);
            SimpleStream stream = new SimpleStream("https://stream.twitter.com/1.1/statuses/sample.json");
            Console.WriteLine("stream state " + stream.StreamState);
            stream.StartStream(token, x => { Console.WriteLine(x.Text); });
            Console.WriteLine(stream.StreamState);
            var client = new HttpClient {
                BaseAddress = new Uri("https://stream.twitter.com/1.1/statuses/sample.json"),
                DefaultRequestHeaders = { { "accept", "application/json"} }
            };
            */

            var auth = new SingleUserAuthorizer {
                CredentialStore = new SingleUserInMemoryCredentialStore {
                    ConsumerKey = LoginInfo.ConsumerKey,
                    ConsumerSecret = LoginInfo.ConsumerSercert,
                    OAuthToken = LoginInfo.AccessToken,
                    OAuthTokenSecret = LoginInfo.AccessTokenSecret
                }
            };
            var twitterCtx = new TwitterContext(auth);
            var searchResponse =
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                     search.Query == "Hello" &&
                     search.SearchLanguage == "en"
                 select search).SingleOrDefault();
            if (searchResponse != null && searchResponse.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                    Console.WriteLine(
                    "User: {0}, Tweet: {1}, Lang: {2}",
                    tweet.User.ScreenNameResponse,
                    tweet.Text,
                    tweet.Lang));
            var news = new News();
            news.getNews();
            Console.ReadLine();
        }
    }
}
