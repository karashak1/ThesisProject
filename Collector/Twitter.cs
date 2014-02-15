using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Collector {
    public class Twitter {

        public string Data { get; set; }

        public Twitter() {
            Data = "";
        }

        public async void getData() {
            var client = new HttpClient {
                BaseAddress = new Uri("http://cs.newpaltz.edu/~plotkinm/2012Grad/Final/api/"),
                DefaultRequestHeaders = { { "accept", "application/json" } }
            };

            var temp = await client.GetStringAsync("Cities.php?");
            Data = temp;
        }
    }


    public class TwitterInfo {
        public string ConsumerKey { get; set; }
        public string ConsumerSercert { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

}
