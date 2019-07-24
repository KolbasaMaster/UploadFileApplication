using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Read.CqrsModels
{
    public class MaterialNameWithId
    {
        public string MaterialName { get; set; }
        public Guid UploadId { get; set; }
    }
}
