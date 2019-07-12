using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadFilesApp.Models;

namespace UploadFilesApp.Infrastructure
{
    public enum MaterialType { Presentation, Application, Other }

    public class FileService
    {
        private AppContext db;

        public FileService(AppContext context)
        {
            db = context;
        }

        public Guid SaveNewMaterial(MaterialType materialType, string path, string fileName, string contentType, int size)
        {
            MaterialModel material = new MaterialModel() 
            {
                MaterialId = Guid.NewGuid(),
                MaterialCategory = (int)materialType
                };
            MaterialVersionModel version = new MaterialVersionModel()
            {
                MaterialName = fileName,
                MaterialUploadDate = DateTime.Now,
                Id = Guid.NewGuid(), VersionNumber = 1, MaterialModel = material,
                MaterialType = contentType,
                MaterialPath = path,
                MaterialSize = size
            };
            material.CurrentVersion = version.VersionNumber;

            db.Materials.Add(material);
            db.MaterialVersions.Add(version);
            db.SaveChanges();
            return material.MaterialId;
        }

        public int ChangeVersion(Guid id,  string fileName, string contentType, string path, int size)
        {
            var version = db.Materials.Find(id);
           
            MaterialVersionModel materialVersion = new MaterialVersionModel()
            {
                MaterialName = fileName,
                MaterialUploadDate = DateTime.Now,
                Id = Guid.NewGuid(),
                MaterialModel = version,
                MaterialPath = path,
                MaterialSize = size,
                VersionNumber = version.CurrentVersion + 1
            };
            
            version.CurrentVersion = materialVersion.VersionNumber;
            db.MaterialVersions.Add(materialVersion);
            db.SaveChanges();
            return materialVersion.VersionNumber;
        }

        public string GetMaterialName(Guid id, int versionNum)
        {
            var queryAboutMaterial = db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
            var name = queryAboutMaterial.MaterialName;
            return name;
        }
    }
}