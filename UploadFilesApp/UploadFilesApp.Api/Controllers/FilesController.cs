using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using DAL.Infrastructure;
using DAL.Read;
using UploadFilesApp.Api.Mapper;
using UploadFilesApp.DAL.Mapper;
using UploadFilesApp.Domain;
using UploadFilesApp.Dto;
using UploadFilesApp.Infrastructure;
using AppContext = UploadFilesApp.Infrastructure.AppContext;


namespace UploadFilesApp.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly IRepository _repository;
        private readonly AppContext _db;
        private readonly IFileStorage _fileStorage;
        private readonly MapperDAL _mapper;
        private readonly IRead _read;

        public FilesController(IRepository repository, AppContext context, IFileStorage storage, MapperDAL mapper, IRead read)
        {
            _repository =  repository;
            _db = context;
            _fileStorage = storage;
            _mapper = mapper;
            _read = read;
        }

        [HttpPost]
        [Route("presentation")]
        public  Guid UploadPresentation()
        {
            
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file == null || file.ContentLength <= 0) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var fileName = Guid.NewGuid().ToString();
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
            file.SaveAs(path);
            var bytes = File.ReadAllBytes(path);
            var material = new Material((int)CategoryDto.Presentation,bytes,file.FileName);
            _repository.Create(material);
            return material.MaterialId;

        }

        [HttpPost]
        [Route("application")]
        public Guid UploadApplication()
        {

            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file == null || file.ContentLength <= 0) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var fileName = Guid.NewGuid().ToString();
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
            file.SaveAs(path);
            var bytes = File.ReadAllBytes(path);
            var material = new Material((int)CategoryDto.Application, bytes, file.FileName);
            _repository.Create(material);
            return material.MaterialId;
        }

        [HttpPost]
        [Route("other")]
        public Guid UploadAnother()
        {
           
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file == null || file.ContentLength <= 0) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var fileName = Guid.NewGuid().ToString();
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
            file.SaveAs(path);
            var bytes = File.ReadAllBytes(path);
            var material = new Material((int)CategoryDto.Other, bytes, file.FileName);
            _repository.Create(material);
            return material.MaterialId;

        }

        [HttpPost]
        [Route("version")]
        public void UploadVersion(Guid guidId)
        {
           
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file == null || file.ContentLength <= 0) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var fileName = Guid.NewGuid().ToString();
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_data"), fileName);
            file.SaveAs(path);
            var bytes = File.ReadAllBytes(path);
            var material = _repository.Get(guidId);
            material.AddVersion(bytes,file.FileName);
            _repository.Update(material);
        }

        [HttpGet]
        [Route("download")]
        public HttpResponseMessage GetFile(Guid id, int version)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            var material = _read.GetMaterialNameWithId(id, version);
            var fileName = material.UploadId;
            var bytes = _repository.GetMaterialByte(fileName);
            var name = material.MaterialName;
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(name));
            return response;
        }


        //[HttpGet]
        //[Route("")] 
        //public async Task <IHttpActionResult> GetMaterialByCategory(int pageNum, int? category, int pageSize)
        //{

        //    var materials = _fileService.GetParticularMaterialWIthVersion(category, pageSize, pageNum);
        //    return Ok(materials);
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public async Task <IHttpActionResult> GetMaterialById(Guid id)
        //{

        //    var material = _fileService.GetMaterialById(id);
        //    return Ok(material);
        //}
    }
}
    




