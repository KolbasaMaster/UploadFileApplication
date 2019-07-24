using System;
using System.Collections.Generic;

namespace UploadFilesApp.Domain
{
    public class Material
    {
        public Guid MaterialId { get; set; }

        public int MaterialCategory { get; set; }

        public List<MaterialVersion> MaterialVersion { get; set; }
        public int CurrentVersion { get; set; }
    
        public Material()
        {
            MaterialVersion = new List<MaterialVersion>();
        }


    }
}
