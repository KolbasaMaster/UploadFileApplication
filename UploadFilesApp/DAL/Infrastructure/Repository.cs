using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UploadFilesApp.Models;
using AutoMapper;
using DAL.Infrastructure;
using UploadFilesApp.DAL.Mapper;
using UploadFilesApp.Domain;

namespace UploadFilesApp.Infrastructure
{
    public class Repository :IRepository
    {
        private readonly AppContext _db;
        private readonly IFileStorage _fileStorage;
        private readonly MapperDAL _mapper;
        
        public Repository(AppContext context, IFileStorage fileStorage, MapperDAL mapper)
        {
            _db = context;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public void Create(Material agregate)
        {
           var modelMaterial = _mapper.Map<MaterialModel>(agregate);
           var bytes = agregate.MaterialVersions.Single().MaterialBytes;
           var uploadId = _fileStorage.Save(bytes);
           modelMaterial.MaterialVersions.Single().UploadId = uploadId;
           _db.Materials.Add(modelMaterial);
           _db.SaveChanges();
        }

        public void Update(Material agregate)
        {
            var id = agregate.MaterialId;
            var material = _db.Materials.Where(x => x.MaterialId == id).Include(x=>x.MaterialVersions).FirstOrDefault();
           
            var materialVersionsList = material.MaterialVersions;
            var versionsToAdd = agregate.MaterialVersions.Where(x =>
                    materialVersionsList.All(s => s.VersionNumber != x.VersionNumber)
                );
            foreach (var version in versionsToAdd)
            {
                var versionAdd = _mapper.Map<MaterialVersionModel>(version);
                versionAdd.UploadId = _fileStorage.Save(version.MaterialBytes);
                material.MaterialVersions.ToList().Add(versionAdd);
                material.CurrentVersion = version.VersionNumber;
            }
            _db.SaveChanges();

        }

        public Material Get(Guid id)
        {
            var materialWithVersions = _db.Materials.Where(x => x.MaterialId == id).Include(x=>x.MaterialVersions).FirstOrDefault();
            Material material = _mapper.Map<Material>(materialWithVersions); 
            material.MaterialVersions.ToList().ForEach(x =>
            {
                var uploadId = materialWithVersions.MaterialVersions.First(mv => mv.Id == x.Id).UploadId;
                x.MaterialBytes = _fileStorage.Get(uploadId);
            });
            return material;
        }


        //public void ChangeVersion(MaterialVersion version, Guid id) //
        //{
        //    var material = _db.Materials.Find(id);
        //    MapperDAL mapper = new MapperDAL();
        //    var modelMaterialVersion = mapper.MapMaterialVersion(version);
        //    var bytes = version.MaterialBytes;
        //    var vers = material.CurrentVersion += 1;
        //    modelMaterialVersion.VersionNumber = vers;
        //    material.MaterialVersion.Add(modelMaterialVersion);
        //    var uploadId = _fileStorage.Save(bytes);
        //    modelMaterialVersion.UploadId = uploadId;
        //    _db.MaterialVersions.Add(modelMaterialVersion);
        //    _db.SaveChanges();
        //}





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

        public string GetMaterialName(Guid id, int versionNum)
        {
            var queryAboutMaterial = _db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
            var name = queryAboutMaterial.MaterialName;
            return name;
        }

        public Guid GetMaterialId(Guid id, int versionNum)
        {
            var queryAboutMaterial = _db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
            var fileName = queryAboutMaterial.UploadId;
            return fileName;
        }

        public byte[] GetMaterialByte(Guid id)
        {
            var downloadByte = _fileStorage.Get(id);
            return downloadByte;
        }


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