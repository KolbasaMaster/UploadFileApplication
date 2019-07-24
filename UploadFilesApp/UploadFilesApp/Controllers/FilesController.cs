using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UploadFilesApp.Dto;
using UploadFilesApp.Infrastructure;
using AppContext = UploadFilesApp.Infrastructure.AppContext;

namespace UploadFilesApp.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly FileService _fileService;

        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("presentation")]
        public  Guid UploadPresentation()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var id = _fileService.SaveNewMaterial(CategoryDto.Presentation, path, file.FileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);

        }

        [HttpPost]
        [Route("application")]
        public Guid UploadApplication()
        {
           
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var id = _fileService.SaveNewMaterial(CategoryDto.Application, path, file.FileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("other")]
        public Guid UploadAnother()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                
                var id = _fileService.SaveNewMaterial(CategoryDto.Other, path, file.FileName, file.ContentType, file.ContentLength);
                return id;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("version")]
        public async Task<IHttpActionResult> UploadVersion(Guid guidId)
        {
           
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
                file.SaveAs(path);
                var version = _fileService.ChangeVersion(guidId, fileName, file.ContentType,  path,  file.ContentLength);
                return Ok();
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("download")]
        public HttpResponseMessage GetFile(Guid id, int version) 
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            var fileName = _fileService.GetMaterialId(id, version);
            var bytes = _fileService.GetMaterialByte(fileName);
            var name = _fileService.GetMaterialName(id, version);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(name));
            return response;
        }


        [HttpGet]
        [Route("")] 
        public async Task <IHttpActionResult> GetMaterialByCategory(int pageNum, int? category, int pageSize)
        {
            var materials = _fileService.GetParticularMaterialWIthVersion(category, pageSize, pageNum);
            return Ok(materials);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task <IHttpActionResult> GetMaterialById(Guid id)
        {
            var material = _fileService.GetMaterialById(id);
            return Ok(material);
        }
    }
}
    




