
namespace xmcloudimport.Model
{
    public class Author
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }


    public class Content
    {
        public string expanded_nostructure { get; set; }
    }

    public class WpPage
    {
        public int id { get; set; }
        public string title { get; set; }
        public string intro { get; set; }
        public Content content { get; set; }
        public string image { get; set; }
        public long date { get; set; }
        public Url url { get; set; }
        public Author author { get; set; }
        public List<Tag> tags { get; set; }
        public Seo seo { get; set; }
    }

    public class Seo
    {
        public string focus_keyword { get; set; }
        public string description { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Url url { get; set; }
    }

    public class Url
    {
        public string full { get; set; }
        public string relative { get; set; }
    }


}
