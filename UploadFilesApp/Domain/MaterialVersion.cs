using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFilesApp.Domain
{
    public class MaterialVersion
    {
        public Guid Id { get; internal set; }
        public int VersionNumber { get; internal set; }
        public string MaterialName { get; internal set; }
        public DateTime? MaterialUploadDate { get; internal set; }
        public int MaterialSize { get; internal set; }
        public byte[] MaterialBytes { get; set; }

        public MaterialVersion(byte[] content,int currentVersion,  string fileName)
        {
            Id = Guid.NewGuid();
            VersionNumber = currentVersion;
            MaterialBytes = content;
            MaterialUploadDate = DateTime.Now;
            MaterialSize = content.Length;
            MaterialName = fileName;
        }

        public MaterialVersion(Guid id, int versionNumber, string materialName, DateTime? materialUploadDate, int materialSize)
        {
            Id = id;
            VersionNumber = versionNumber;
            MaterialName = materialName;
            MaterialUploadDate = materialUploadDate;
            MaterialSize = materialSize;
            

        }
    

    }

    
}
