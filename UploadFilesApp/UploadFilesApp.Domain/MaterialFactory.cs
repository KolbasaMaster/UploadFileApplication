using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFilesApp.Domain
{
    abstract class MaterialFactory
    {
        private Material _material;
        public MaterialFactory(Material material)
        {
            _material = material;
        }
        
    }
}
