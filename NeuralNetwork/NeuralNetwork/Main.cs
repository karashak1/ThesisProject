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

            testStream(ini);
            //var news = new News();
            //news.getNews();
            Console.ReadLine();
        }

        public static async void testStream(string twitterFile) {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            TwitterInfo LoginInfo;
            using (StreamReader sr = new StreamReader(twitterFile))
            using (JsonTextReader jr = new JsonTextReader(sr)) {
                LoginInfo = serializer.Deserialize<TwitterInfo>(jr);
            }


            var auth = new SingleUserAuthorizer {
                CredentialStore = new SingleUserInMemoryCredentialStore {
                    ConsumerKey = LoginInfo.ConsumerKey,
                    ConsumerSecret = LoginInfo.ConsumerSercert,
                    OAuthToken = LoginInfo.AccessToken,
                    OAuthTokenSecret = LoginInfo.AccessTokenSecret
                }
            };
            var twitterCtx = new TwitterContext(auth);
            /*
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
             */
            var count = 0;
            Console.WriteLine("Testing streaming");
            Tweet test;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Kevin\Documents\tweets.txt", true)) {
                

                await (from strm in twitterCtx.Streaming
                       where strm.Type == StreamingType.Sample
                       select strm)
                    .StartAsync(async strm => {
                        //file.WriteLine(strm.Content + "\n");
                        try{
                            test = JsonConvert.DeserializeObject<Tweet>(strm.Content);
                            if (test != null)
                                if(test.text != null && (test.lang.Contains("en")))
                                    Console.WriteLine(test.text +" " + test.lang);
                            if (count++ >= 100)
                                strm.CloseStream();
                        }
                        catch (JsonException) {

                        }
                    });
            }
        }
    }
}
