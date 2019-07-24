using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UploadFilesApp;


namespace UploadFilesApp.Infrastructure
{
    public class LocalFileStorage : IFileStorage

    {
        public Guid Save(string path)
        {
            var id = Guid.NewGuid();
            var newPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), id.ToString());
            File.Move(path, newPath);
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