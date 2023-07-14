using Newtonsoft.Json;
using System.Net;


namespace xmcloudimport
{
    internal class Tags
    { 

        Dictionary<string,Guid> urls = new Dictionary<string,Guid>();

        public Guid CreateOrUseTag(string path, CookieContainer cookies, string name)
        {
            Guid value;
            if (urls.TryGetValue(path, out value)) {
                return value;
            }
            
            var readvalue = Helpers.GetItem(path, cookies,Config.Language);
            if (readvalue != null)
            {
                urls.Add(path, (Guid)readvalue);
                return (Guid)readvalue;
            }

            var pageObj = new TagModel
            {
                ItemName = Helpers.GetItemNameFromURL(path),
                TemplateID = "{6B40E84C-8785-49FC-8A10-6BCA862FF7EA}",
                Title = name
            };

            var requestBody = JsonConvert.SerializeObject(pageObj);

            string parent = Helpers.RemoveItemNameFromURL(path);
            var guid = Helpers.AddItem(parent, cookies, requestBody);
            urls.Add(path, guid);
            return guid;
        }


    }
}
