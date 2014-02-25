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

        private Articles _Data;

        public Articles Data {
            get { return _Data; }
            private set { _Data = value; }
        }
        
        public News() {
            Data = new Articles();
        }


        public void getGoogleNews(string topic) {
            var url = "http://news.google.com?q="+topic+"&output=rss";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            foreach (SyndicationItem item in feed.Items) {
                var article = new Article();
                article.Title = item.Title.Text;
                article.Summery = item.Summary.Text;
                article.publishData = item.PublishDate;
                foreach (SyndicationLink link in item.Links) {
                    var output = link.Uri.ToString().Split('&');
                    foreach (var part in output)
                        if (part.Contains("url=http"))
                            article.URL = part.Split('=')[1];
                }
                Data.add(article);
            }
        }
    }

    public class Articles : IEnumerable<Article> {
        List<Article> articles;

        public Articles() {
            this.articles = new List<Article>();
        }

        public void add(Article article) {
            this.articles.Add(article);
        }

        public IEnumerator<Article> GetEnumerator() {
            return this.articles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.articles.GetEnumerator();
        }
    }

    public class Article {
        public string Title { get; set; }
        public string Summery { get; set; }
        public string URL { get; set; }
        public bool calmness { get; set; }
        public DateTimeOffset publishData { get; set; }
    }

}
