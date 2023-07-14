
namespace xmcloudimport
{
    internal static class Config
    {
        /* Do not use trailing / or \   */
        internal static string ImportFile = @"importdata.json";
        // use https://xxxxxx.sitecorecloud.io  or a local xm-cloud,  or with Sitecore XP adjust the GUID below and in the import
        internal static string SitecoreUrl = "https://xmcloudcm.localhost";
        internal static string ContentRoot = @"/sitecore/content/Home";
        internal static string MediaRoot = @"/sitecore/media library/Images";
        internal static string TagRoot = @"/sitecore/content/Home";
        internal static string MediaFileFolder = @"C:\tmp\importmedia";
        internal static string SitecoreUser = "admin";
        internal static string SitecorePassword = "b";
        internal static string Language = "en";

        //guid  /sitecore/templates/Project/[your Headless Site]/Page   note the {76036F5E-CBCE-46D1-AF0A-4143F9B557AA} do not contain the Content and sxatags field, but is usable to show the rest
        internal static string JSSPageGuid = "{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}";


    }
}
