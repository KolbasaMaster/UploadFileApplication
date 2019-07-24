using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadFilesApp.Models
{
    public class ParticularMaterialModel
    {
        public Guid MaterialId { get; set; }
        public List<ListMaterialVersion> MaterialVersionModels { get; set; }
    }
}