using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Read.CqrsModels;
using UploadFilesApp.DAL.Mapper;
using AppContext = UploadFilesApp.Infrastructure.AppContext;

namespace DAL.Read
{
    public class Read : IRead
    {
        private readonly AppContext _db;
        private readonly MapperDAL _mapper;

        public Read(AppContext context, MapperDAL mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        public MaterialNameWithId GetMaterialNameWithId(Guid id, int versionNum)
        {
            var queryAboutMaterial = _db.MaterialVersions.FirstOrDefault(s => s.MaterialId == id && s.VersionNumber == versionNum);
            MaterialNameWithId material = new MaterialNameWithId()
            {
                MaterialName = queryAboutMaterial.MaterialName,
                UploadId = queryAboutMaterial.UploadId
            };
            return material;
        }
    }
}
