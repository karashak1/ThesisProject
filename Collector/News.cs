using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace Collector
{
    public class News
    {
        public News() {

        }

        public void getNews() {
            var url = "http://news.google.com?q=Disney&output=rss";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            foreach (SyndicationItem item in feed.Items) {
                string subject = item.Title.Text;
                Console.WriteLine(subject);
                foreach (SyndicationLink link in item.Links) {
                    var output = link.Uri.ToString().Split('&');
                    foreach (var part in output)
                        if (part.Contains("url=http"))
                            Console.WriteLine(part.Split('=')[1]);
                }
                Console.WriteLine();
            }
        }

    }

}
