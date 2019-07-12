using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UploadFilesApp.Models
{
    public class MaterialVersionModel
    {
        public Guid MaterialId { get; set; }
        public Guid Id { get; set; }
        public int VersionNumber { get; set; }
        public string MaterialName { get; set; }
        public DateTime? MaterialUploadDate { get; set; }
        public string MaterialType { get; set; }
        public string MaterialPath { get; set; }
        public int MaterialSize { get; set; }
        public MaterialModel MaterialModel { get; set; }
        
    }
}