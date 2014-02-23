using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LinqToTwitter;
using System.IO;

namespace Collector {
    public class Twitter {

        private TwitterContext twitterCtx { get; set; }

        private Tweets _Data;

        public Tweets Data {
            get { return _Data; }
            private set { _Data = value; }
        }


        public Twitter(string twitterFile) {
            Data = new Tweets();
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
            twitterCtx = new TwitterContext(auth);
        }

        public async void getTestSet() {
            
        }

        public async void populateData(int count) {
            Tweet test;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Kevin\Documents\tweets.txt", true)) {

                await (from strm in twitterCtx.Streaming
                       where strm.Type == StreamingType.Sample
                       select strm)
                    .StartAsync(async strm => {
                        try {
                            test = JsonConvert.DeserializeObject<Tweet>(strm.Content);
                            if (test != null)
                                if (test.text != null && (test.lang.Contains("en"))) {
                                    _Data.add(new TweetData {
                                        text = test.text,
                                        created_at = test.created_at
                                    });

                                    Console.WriteLine(test.text + " " + test.lang);
                                    count++;
                                }
                            if (count >= 1000)
                                strm.CloseStream();
                        }
                        catch (JsonException) {

                        }
                    });
            }
        }

        public async void testStream() {
            var count = 0;
            Console.WriteLine("Testing streaming");
            Tweet test;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Kevin\Documents\tweets.txt", true)) {

                await (from strm in twitterCtx.Streaming
                       where strm.Type == StreamingType.Sample
                       select strm)
                    .StartAsync(async strm => {
                        try {
                            test = JsonConvert.DeserializeObject<Tweet>(strm.Content);
                            if (test != null)
                                if (test.text != null && (test.lang.Contains("en"))) {
                                    
                                    
                                    Console.WriteLine(test.text + " " + test.lang);
                                    count++;
                                }
                            if (count >= 1000)
                                strm.CloseStream();
                        }
                        catch (JsonException) {

                        }
                    });
            }
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
           
            var count = 0;
            Console.WriteLine("Testing streaming");
            Tweet test;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Kevin\Documents\tweets.txt", true)) {


                await (from strm in twitterCtx.Streaming
                       where strm.Type == StreamingType.Sample
                       select strm)
                    .StartAsync(async strm => {
                        try {
                            test = JsonConvert.DeserializeObject<Tweet>(strm.Content);
                            if (test != null)
                                if (test.text != null && (test.lang.Contains("en"))) {
                                    

                                    Console.WriteLine(test.text + " " + test.lang);

                                    count++;
                                }
                            if (count >= 1000)
                                strm.CloseStream();
                        }
                        catch (JsonException) {

                        }
                    });
            }
        }
    }

    public class Tweets : IEnumerable<TweetData> {
        private List<TweetData> tweets;

        public Tweets() {
            tweets = new List<TweetData>();
        }

        public void add(TweetData tweet) {
            tweets.Add(tweet);
        }
    
        public IEnumerator<TweetData> GetEnumerator(){
            return this.tweets.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
            return this.tweets.GetEnumerator();
        }
    }

    public class TweetData {
        public string text { get; set; }
        public string created_at { get; set; }
        public bool calmness { get; set; }
    }


    public class TwitterInfo {
        public string ConsumerKey { get; set; }
        public string ConsumerSercert { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    
    public class Tweet
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool truncated { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public object in_reply_to_user_id { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public User user { get; set; }
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public Entities entities { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string filter_level { get; set; }
        public string lang { get; set; }
    }

    public class User
    {
        public long id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public bool _protected { get; set; }
        public int followers_count { get; set; }
        public int friends_count { get; set; }
        public int listed_count { get; set; }
        public string created_at { get; set; }
        public int favourites_count { get; set; }
        public int utc_offset { get; set; }
        public string time_zone { get; set; }
        public bool geo_enabled { get; set; }
        public bool verified { get; set; }
        public int statuses_count { get; set; }
        public string lang { get; set; }
        public bool contributors_enabled { get; set; }
        public bool is_translator { get; set; }
        public bool is_translation_enabled { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_banner_url { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool profile_use_background_image { get; set; }
        public bool default_profile { get; set; }
        public bool default_profile_image { get; set; }
        public object following { get; set; }
        public object follow_request_sent { get; set; }
        public object notifications { get; set; }
    }

    public class Entities
    {
        public object[] hashtags { get; set; }
        public object[] symbols { get; set; }
        public object[] urls { get; set; }
        public object[] user_mentions { get; set; } 
    }


}
