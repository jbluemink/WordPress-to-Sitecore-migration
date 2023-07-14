using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmcloudimport.Model
{
    internal class StandardItem
    {
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemPath { get; set; }
        public Guid ParentID { get; set; } 
        public Guid TemplateID { get; set; } 
        public string TemplateName { get; set; }
        public string ItemLanguage { get; set; }
        public string ItemVersion { get; set; } 
    }
}
