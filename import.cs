using xmcloudimport;
using xmcloudimport.Model;
using Newtonsoft.Json;
using System.Web;

public class Import
{
    public static void MainImport(List<WpPage> wppages)
    {
        try
        {
            var cookies = Helpers.LogIn();

            var tags = new Tags();
            var media = new xmcloudimport.Media();

            foreach (var page in wppages)
            {
                var newsdateTime = DateTimeOffset.FromUnixTimeSeconds(page.date).UtcDateTime;
                var title = HttpUtility.HtmlDecode(page.title);
                var pageObj = new JSSPageModel
                {
                    ItemName = Helpers.GetItemNameFromURL(page.url.relative),
                    TemplateID = Config.JSSPageGuid,
                    //no branching support
                    Title = title,
                    __Renderings = "<r xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><d id=\"{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}\" l=\"{96E5F4BA-A2CF-4A4C-A4E7-64DA88226362}\" /></r>",
                    __FinalRenderings = "<r xmlns:p=\"p\" xmlns:s=\"s\" p:p=\"1\"><d id=\"{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}\"><r uid=\"{5F463C9D-54AB-4276-9B2D-82790507354B}\" s:ds=\"local:/Data/Image 1\" s:id=\"{AB2EDBA0-3960-4F12-B765-579DC231894A}\" s:par=\"GridParameters=%7B7465D855-992E-4DC2-9855-A03250DFA74B%7D&amp;FieldNames=%7BA563DD79-B4A2-49E2-8B1A-949233183386%7D&amp;Styles&amp;CacheClearingBehavior=Clear%20on%20publish&amp;RenderingIdentifier&amp;CSSStyles&amp;DynamicPlaceholderId=1\" s:ph=\"headless-main\" /></d></r>\r\n",
                    Content = "<p>" + page.intro + "</p>" + page.content.expanded_nostructure
                };
                //Add SXA Page Data
                var dataObj = new ItemModel
                {
                    ItemName = "Data",
                    TemplateID = "{1C82E550-EBCD-4E5D-8ABD-D50D0809541E}",
                };
                var ImageComponentObj = new JSSEAImageModel
                {
                    ItemName = "Image",
                    TemplateID = "{D885DF8C-B2D6-4007-B34B-2BBAFB527304}",
                };
                if (page.image != "false" && !string.IsNullOrEmpty(page.image))
                {
                    var path = Helpers.GetFilenameFromURl(page.image);
                    var mediaguid = media.UseMedia(Config.MediaRoot + path, cookies);
                    if (mediaguid != null)
                    {
                        ImageComponentObj.Image = $"<image mediaid=\"{mediaguid.ToString()}\" />";
                    }
                }
                foreach (var tag in page.tags)
                {
                    if (!string.IsNullOrEmpty(pageObj.Tags))
                    {
                        pageObj.Tags += "|";
                    }
                    try
                    {
                        pageObj.Tags += tags.CreateOrUseTag(@Config.TagRoot+"/"+ Helpers.ProposeValidItemName(tag.name), cookies, tag.name).ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"fail to use tag {tag.name} = {Helpers.ProposeValidItemName(tag.name)}:" + ex.Message);
                        throw ex;
                    }
                }

                var requestBody = JsonConvert.SerializeObject(pageObj);
                try
                {  
                    var parenturl = Config.ContentRoot;
                    Helpers.AddItem(parenturl, cookies, requestBody);
                    var datapath = parenturl + "/" + pageObj.ItemName;
                    Helpers.AddItem(datapath, cookies, JsonConvert.SerializeObject(dataObj));
                    Helpers.AddItem(datapath + "/" + dataObj.ItemName, cookies, JsonConvert.SerializeObject(ImageComponentObj));
                } catch (Exception ex)
                {
                    Console.WriteLine($"Item {pageObj.ItemName} exception:"+ex.Message);
                }

                Console.WriteLine($"Item {pageObj.ItemName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred. Message: {ex.Message}.\r\n StackTrace: {ex.StackTrace}.\r\n InnerException: {ex.InnerException}");
        }

        Console.ReadKey();
    }
}