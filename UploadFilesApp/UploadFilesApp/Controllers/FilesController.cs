using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Results;
using UploadFilesApp.Infrastructure;
using UploadFilesApp.Models;
using AppContext = UploadFilesApp.Infrastructure.AppContext;

namespace UploadFilesApp.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        
        [HttpPost]
        [Route("presentation")]
        public Guid UploadPresentation()
        {
            AppContext db = new AppContext();
            FileService fileService = new FileService(db);
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var id = fileService.SaveNewMaterial(MaterialType.Presentation, path, fileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);

        }

        [HttpPost]
        [Route("application")]
        public Guid UploadApplication()
        {
            AppContext db = new AppContext();
            FileService fileService = new FileService(db);
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var id = fileService.SaveNewMaterial(MaterialType.Application, path, fileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("other")]
        public Guid UploadAnother()
        {
            AppContext db = new AppContext();
            FileService fileService = new FileService(db);
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                
                var id = fileService.SaveNewMaterial(MaterialType.Other, path, fileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("version")]
        public int UploadVersion(Guid guidId)
        {
            AppContext db = new AppContext();
            FileService fileService = new FileService(db);
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var version = fileService.ChangeVersion(guidId, fileName, file.ContentType,  path,  file.ContentLength);
                return version;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("Download")]
        public HttpResponseMessage GetFile(Guid guidId, int version)
        {
            AppContext db = new AppContext();
            FileService fileService = new FileService(db);
            var name = fileService.GetMaterialName(guidId, version);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string path = HttpContext.Current.Server.MapPath("~/App_data/") + name;
            if (!File.Exists(path))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", name);
                throw new HttpResponseException(response);
            }

            byte[] bytes = File.ReadAllBytes(path);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(name));
            return response;

        }
    }
}
    




