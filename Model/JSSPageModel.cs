using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class JSSPageModel
{
    public string ItemName { get; set; }

    public string TemplateID { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    [JsonProperty("SxaTags")]
    public string Tags { get; set; }
    public string __Renderings { get; set; }

    [JsonProperty("__Final Renderings")]
    public string __FinalRenderings { get; set; }

}