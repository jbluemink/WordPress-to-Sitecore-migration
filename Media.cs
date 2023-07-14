using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace xmcloudimport
{
    internal class Media
    { 

        Dictionary<string,Guid> urls = new Dictionary<string,Guid>();

        public Guid? UseMedia(string path, CookieContainer cookies)
        {
            Guid value;
            if (urls.TryGetValue(path, out value)) {
                return value;
            }
            
            var readvalue = Helpers.GetItem(Helpers.RemoveMediaExtension(path), cookies,Config.Language);
            if (readvalue != null)
            {
                urls.Add(path, (Guid)readvalue);
                return (Guid)readvalue;
            }
            
            return null;
        }


    }
}
