using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UploadFilesApp.Models;
using AutoMapper;
using UploadFilesApp.DAL.Mapper;
using UploadFilesApp.Domain;





namespace UploadFilesApp.Infrastructure
{
    public class FileService
    {
        private readonly AppContext _db;
        private readonly IFileStorage _fileStorage;

        
        public FileService(AppContext context, IFileStorage fileStorage)
        {
            _db = context;
            _fileStorage = fileStorage;
        }

        public void Create(Material agregate)
        {
           MapperDAL mapper = new MapperDAL();
           var modelMaterial = mapper.MapMaterialModel(agregate);
           var bytes = agregate.MaterialVersion.FirstOrDefault().MaterialBytes;
           var uploadId = _fileStorage.Save(bytes);
           modelMaterial.MaterialVersion.FirstOrDefault().UploadId = uploadId;
           _db.Materials.Add(modelMaterial);
           _db.SaveChanges();
        }

 

        //public int ChangeVersion(Guid id,  string fileName, string contentType, string path, int size)
        //{
        //    var version = _db.Materials.Find(id);
           
        //    MaterialVersionModel materialVersion = new MaterialVersionModel()
        //    {
        //        MaterialName = fileName,
        //        MaterialUploadDate = DateTime.Now,
        //        Id = Guid.NewGuid(),
        //        MaterialModel = version,
        //        MaterialPath = path,
        //        MaterialSize = size,
        //        VersionNumber = version.CurrentVersion + 1,
                
        //    };
            
        //    version.CurrentVersion = materialVersion.VersionNumber;
        //    version.MaterialVersion.Add(materialVersion);
        //    _db.MaterialVersions.Add(materialVersion);
        //    var uploadId = _fileStorage.Save(path);
        //    materialVersion.UploadId = uploadId;
        //    _db.SaveChanges();
        //    return materialVersion.VersionNumber;
        //}

        //public string GetMaterialName(Guid id, int versionNum)
        //{
        //    var queryAboutMaterial = _db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
        //    var name = queryAboutMaterial.MaterialName;
        //    return name;
        //}

        //public Guid GetMaterialId(Guid id, int versionNum)
        //{
        //    var queryAboutMaterial = _db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
        //    var fileName = queryAboutMaterial.UploadId;
        //    return fileName;
        //}

        //public byte[] GetMaterialByte(Guid id)
        //{
        //    var downloadByte = _fileStorage.Get(id);
        //    return downloadByte;
        //}


        //public List<string> GetParticularMaterialWIthVersion(int? category, int pageSize, int pageNum) 
        //{
        //    var result = new List<string>();
        //    var query = _db.Materials.AsQueryable();
        //    if (category.HasValue)
        //    {
        //        query = query.Where(x => x.MaterialCategory == category);
        //    }

        //    foreach (var material in query.Skip(pageSize*(pageNum-1)).Take(pageSize))
        //    {
        //        var queryAboutMaterialWithVersion = material.MaterialId + " " + material.CurrentVersion;
        //        result.Add(queryAboutMaterialWithVersion);
        //    }
        //    return result;
        //}

        //public ParticularMaterialModel GetMaterialById(Guid id)
        //{
        //    Mapper.Mapper mapper = new Mapper.Mapper();
        //    var versions = _db.MaterialVersions.Where(x => x.MaterialId == id).ToList();
        //    ParticularMaterialModel material = new ParticularMaterialModel()
        //    {
        //        MaterialId = id,
        //        MaterialVersionModels = mapper.MapMaterialVersion(versions)
        //    };

        //    return material;
        //}
    }
}