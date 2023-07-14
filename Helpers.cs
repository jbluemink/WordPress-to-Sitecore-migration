using xmcloudimport.Model;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace xmcloudimport
{
    internal class Helpers
    {
        // like the Sitecore native methode but this one do not requere config, extend this with your own exceptions.
        public static string ProposeValidItemName(string s)
        {
            var value = s;
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length > 100)
            {
                value = s.Substring(0, 100);
            }
            value = value.Replace(".", "").Replace("(","").Replace(")","").Replace("é","e").Replace("&amp;","").Replace("ë","e").Replace("'","");
            return value;
        }
        public static string GetItemNameFromURL(string s)
        {
            var sarray = s.TrimEnd('/').Split("/");
            return ProposeValidItemName(sarray.Last());
        }
        public static string RemoveItemNameFromURL(string s)
        {
            int last = s.LastIndexOf("/");
            return s.Substring(0, last);
        }


        public static CookieContainer LogIn()
        {
            var authUrl = Config.SitecoreUrl + "/sitecore/api/ssc/auth/login";
            var authData = new Authentication
            {
                Domain = "sitecore",
                Username = Config.SitecoreUser,
                Password = Config.SitecorePassword
            };

            var authRequest = (HttpWebRequest)WebRequest.Create(authUrl);

            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";

            var requestAuthBody = JsonConvert.SerializeObject(authData);

            var authDatas = new UTF8Encoding().GetBytes(requestAuthBody);

            using (var dataStream = authRequest.GetRequestStream())
            {
                dataStream.Write(authDatas, 0, authDatas.Length);
            }

            CookieContainer cookies = new CookieContainer();

            authRequest.CookieContainer = cookies;

            var authResponse = authRequest.GetResponse();

            Console.WriteLine($"Login Status:\n\r{((HttpWebResponse)authResponse).StatusDescription}");

            authResponse.Close();

            return cookies;
        }

        public static Guid AddItem(string path, CookieContainer cookies, string? jsonPage)
        {

            var url = Config.SitecoreUrl + "/sitecore/api/ssc/item" + path.Replace(" ", "%20") + "?database=master&language="+Config.Language;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.CookieContainer = cookies;

            var data = new UTF8Encoding().GetBytes(jsonPage);

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(data, 0, data.Length);
            }

            var response = request.GetResponse();

            var location = response.Headers.Get("Location");
            //var ggg = location.Substring(location.IndexOf("item") + 5, 36);
            Guid guid = Guid.Parse(location.Substring(location.IndexOf("item")+5,36));
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseempty = reader.ReadToEnd();

            }
            return guid;
        }

        //path needs to start with /sitecore
        public static bool TestItemExists(string path, CookieContainer cookies)
        {

            var url = Config.SitecoreUrl + "/sitecore/api/ssc/item/?database=master&language="+Config.Language+"&path=" + System.Web.HttpUtility.UrlEncode(path);

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = cookies;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (WebException) {

            }
           
            return false;
        }

        public static Guid? GetItem(string path, CookieContainer cookies, string language)
        {

            var url = Config.SitecoreUrl + "/sitecore/api/ssc/item/?database=master&language=" +language+"&path=" + System.Web.HttpUtility.UrlEncode(path);

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = cookies;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        string json = reader.ReadToEnd();
                        StandardItem jsonobject = JsonConvert.DeserializeObject<StandardItem>(json);
                        return jsonobject.ItemID;
                    }
                }
            }
            catch (WebException)
            {

            }

            return null;
        }


        public static string RemoveMediaExtension(string path)
        {
            int dot = path.LastIndexOf(".");
            if (dot > 0)
            {
                return path.Substring(0, dot);
            }
            return path;
        }

        public static string mediaitemnameoptimizer(string path)
        {
            return path.Replace("%E2", "").Replace("%80", "").Replace("%99", "").Replace("%98", "").Replace("%C3", "").Replace("%AB", "e").Replace("%A9", "e").Replace("%C2", "").Replace("%B4", "").Replace("%AE","").Replace("@", "").Replace("%", "").Replace(" ", "");
        }

        public static string GetFilenameFromURl(string url)
        {
            //remove prefix for wordpress or umbraco or sitecore urls
            Uri uri = new Uri(url.Replace("/wp-content/uploads", "").Replace("/media/", "/").Replace("/-/media/", "/"));
            return mediaitemnameoptimizer(uri.AbsolutePath);
        }

    }
}
