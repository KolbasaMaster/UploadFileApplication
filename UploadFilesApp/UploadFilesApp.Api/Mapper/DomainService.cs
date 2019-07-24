using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadFilesApp.Domain;
using UploadFilesApp.Dto;

//namespace UploadFilesApp.Api.Mapper
//{
//    public class DomainService
//    {
//        public Material CreateDomainMaterial(CategoryDto materialType, string path, string fileName, string contentType,
//            int size, byte[] bytes)
//        {
//            Material material = new Material()
//            {
//                MaterialId = Guid.NewGuid(),
//                MaterialCategory = (int) materialType
//            };
//            MaterialVersion version = new MaterialVersion()
//            {
//                MaterialName = fileName,
//                MaterialUploadDate = DateTime.Now,
//                Id = Guid.NewGuid(),
//                VersionNumber = 1,
//                MaterialType = contentType,
//                MaterialPath = path,
//                MaterialSize = size,
//                MaterialBytes = bytes,
                
//            };
//            material.CurrentVersion = version.VersionNumber;
//            material.MaterialVersion.Add(version);
//            return material;
//        }

//        public MaterialVersion ChangeVersionDomainMaterial(CategoryDto materialType, string path, string fileName,
//            string contentType,
//            int size, byte[] bytes)
//        {
//            MaterialVersion version = new MaterialVersion()
//            {
//                MaterialName = fileName,
//                MaterialUploadDate = DateTime.Now,
//                Id = Guid.NewGuid(),
//                MaterialType = contentType,
//                MaterialPath = path,
//                MaterialSize = size,
//                MaterialBytes = bytes,
//            };
//            return version;
//        }
//    }
//}