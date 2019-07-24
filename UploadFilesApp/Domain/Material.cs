using System;
using System.Collections.Generic;
using System.Linq;


namespace UploadFilesApp.Domain
{
    public class Material 
    {
        public Guid MaterialId { get; }

        public int MaterialCategory { get;  }

        public IEnumerable<MaterialVersion> MaterialVersions { get; } 
        public int CurrentVersion { get; private set; }

        public Material(int materialCategory, byte[] content, string fileName)
        {
            MaterialId = Guid.NewGuid();
            CurrentVersion = 1;
            MaterialCategory = materialCategory;
            var version = new MaterialVersion(content, CurrentVersion,  fileName);
            MaterialVersions = new List<MaterialVersion>();
            ((ICollection<MaterialVersion>) MaterialVersions).Add(version);
        }

        public Material(Guid materialId, int materialCategory, ICollection<MaterialVersion> materialVersions, int currentVersion)
        {
            MaterialId = materialId;
            MaterialCategory = materialCategory;
            MaterialVersions = materialVersions;
            CurrentVersion = currentVersion;
        }
        
        public void AddVersion(byte[] content, string fileName)
        {
            var lastVersion = MaterialVersions.Select(x => x.VersionNumber).Max();
            lastVersion++;
            CurrentVersion = lastVersion;
            var version = new MaterialVersion(content, CurrentVersion, fileName);
            MaterialVersions.ToList().Add(version);
        }
    }
}