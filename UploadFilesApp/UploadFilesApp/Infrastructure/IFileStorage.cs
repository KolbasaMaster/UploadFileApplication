using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFilesApp.Infrastructure
{
    public interface IFileStorage
    {
        Guid Save(string path);
       
        byte[] Get(Guid id);
    }
}
