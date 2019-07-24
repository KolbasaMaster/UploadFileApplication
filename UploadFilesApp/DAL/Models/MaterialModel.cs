using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UploadFilesApp.Models
{
    public class MaterialModel
    {
        [Key]
        public Guid MaterialId { get; set; }
        
        public int MaterialCategory { get; set; }
        
        public ICollection<MaterialVersionModel> MaterialVersions { get; set; }
        public int CurrentVersion { get; set; }
     
        public MaterialModel()
        {
           MaterialVersions = new List<MaterialVersionModel>();
        }
    }
}