using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class NewsModel
{
    public string ItemName { get; set; }

    public string TemplateID { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Author { get; set; }

    [JsonProperty("Publication Date")]
    public string PublicationDate { get; set; }

    public string MetaTitle { get; set; }

    [JsonProperty("Page keywords")]
    public string PageKeywords {get; set;}

    [JsonProperty("Page description")]
    public string PageDescription { get; set; }

    public string OpenGraphTitle { get; set; }
    public string OpenGraphDescription { get; set; }
    public string OpenGraphImageUrl { get; set; }

    public string Tags { get; set; }
    public string __Renderings { get; set; }

    [JsonProperty("__Final Renderings")]
    public string __FinalRenderings { get; set; }

}