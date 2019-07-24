using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadFilesApp.Models
{
    public class ListMaterialVersion
    {
        public int VersionNumber { get; set; }
        public string MaterialName { get; set; }
        public string MaterialUploadDate { get; set; }
    }
}