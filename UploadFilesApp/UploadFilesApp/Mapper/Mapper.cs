using System.Collections.Generic;
using AutoMapper;
using UploadFilesApp.Models;

namespace UploadFilesApp.Mapper
{
    public class Mapper
    {
        private IMapper _mapper;

        public Mapper()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MaterialVersionModel, ListMaterialVersion>().ForMember(dest => dest.MaterialName, act => act.MapFrom(src => src.MaterialName))
                    .ForMember(dest => dest.MaterialUploadDate, act => act.MapFrom(src => src.MaterialUploadDate))
                    .ForMember(dest => dest.VersionNumber, act => act.MapFrom(src => src.VersionNumber));
            }).CreateMapper();
        }
        
        public List<ListMaterialVersion> MapMaterialVersion(List<MaterialVersionModel> materialVersion)
        {
            var listMaterialVersion = new List<ListMaterialVersion>();
            foreach (var version in materialVersion)
            {
                var result = _mapper.Map<ListMaterialVersion>(version);
                listMaterialVersion.Add(result);
            }

            return listMaterialVersion;
        }
    }
}