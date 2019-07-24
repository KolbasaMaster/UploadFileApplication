using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Read.CqrsModels;

namespace DAL.Read
{
    public interface IRead
    {
        MaterialNameWithId GetMaterialNameWithId(Guid id, int versionNum);
      
    }
}
