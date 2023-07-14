using xmcloudimport.Model;
using System.Net;

namespace xmcloudimport
{
    internal class MediaImport
    {

        public static void MainImport(List<WpPage> wppages)
        {
            foreach (var page in wppages)
            {
                if (page.image == "false")
                {
                    continue;
                }
                try
                {
                    string path = Helpers.GetFilenameFromURl(page.image);
                    if (SaveImage(path, null, true))
                    {
                        WebClient client = new WebClient();
                        byte[] imageData = client.DownloadData(page.image);
                        SaveImage(path, imageData, false);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fail to ptocess {page.image} from {page.title}");
                    throw e;
                }
            }
        }

        private static bool SaveImage(string path, byte[] image, bool testexsist)
        {
            //.Create(basemediadirectory + path, image);
            if (!System.IO.File.Exists(Config.MediaFileFolder + path))
            {
                if (testexsist || image == null)
                {
                    return true;
                }
                Console.WriteLine($"save {path}");
                new FileInfo(Config.MediaFileFolder + path).Directory.Create();
                using (System.IO.FileStream fs = System.IO.File.Create(Config.MediaFileFolder + path))
                {
                    fs.Write(image);
                }
            } else
            {
                return false;
            }
            return true;
        }
    }
}
