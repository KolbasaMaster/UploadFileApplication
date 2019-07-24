using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

using UploadFilesApp;
using UploadFilesApp.Domain;


namespace UploadFilesApp.Infrastructure
{
    public class LocalFileStorage : IFileStorage

    {
        public Guid Save(byte[] bytes)
        {
            var id = Guid.NewGuid();
            File.Create(@"C:\Users\OUT-Reutov-VA\source\repos\UploadFilesApp\UploadFilesApp\Uploads\" + id);
            return id;
        }

        public byte[] Get(Guid id)
        {
            using (FileStream fstream =
                File.OpenRead(@"C:\Users\OUT-Reutov-VA\source\repos\UploadFilesApp\UploadFilesApp\Uploads\" + id))
            {
                byte[] material =new byte[fstream.Length];
                return material;
            }
        }
    }
}