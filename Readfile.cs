using xmcloudimport.Model;
using Newtonsoft.Json;

namespace xmcloudimport
{
    internal static class Readfile
    {

        public static void WriteMediaToFile()
        {
            List<WpPage> wppages = JsonConvert.DeserializeObject<List<WpPage>>(File.ReadAllText(Config.ImportFile));
            MediaImport.MainImport(wppages);
        }
        public static void ReadJson()
        {
            List<WpPage> wppages = JsonConvert.DeserializeObject<List<WpPage>>(File.ReadAllText(Config.ImportFile));
            Import.MainImport(wppages);
        }
    }
}
