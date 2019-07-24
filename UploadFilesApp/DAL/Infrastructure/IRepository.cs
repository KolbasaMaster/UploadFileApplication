using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFilesApp.Domain;

namespace DAL.Infrastructure
{
    public interface IRepository
    {
        void Create(Material agregate);
        void Update(Material agregate);
        Material Get(Guid id);
        string GetMaterialName(Guid id, int versionNum);
        Guid GetMaterialId(Guid id, int versionNum);
        byte[] GetMaterialByte(Guid id);
    }
}
