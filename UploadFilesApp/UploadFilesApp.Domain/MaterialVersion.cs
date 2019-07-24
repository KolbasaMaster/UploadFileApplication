using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFilesApp.Domain
{
    public class MaterialVersion
    {
        public Guid MaterialId { get; set; }
        public Guid Id { get; set; }
        public int VersionNumber { get; set; }
        public string MaterialName { get; set; }
        public DateTime? MaterialUploadDate { get; set; }
        public string MaterialType { get; set; }
        public string MaterialPath { get; set; }
        public int MaterialSize { get; set; }
        public Material Material { get; set; }
        public Guid UploadId { get; set; }
        public byte[] MaterialBytes { get; set; }
    }
}
