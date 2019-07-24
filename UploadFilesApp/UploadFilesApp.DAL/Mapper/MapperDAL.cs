using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UploadFilesApp.Domain;
using UploadFilesApp.Models;

namespace UploadFilesApp.DAL.Mapper
{
    public class MapperDAL
    {
        private IMapper _mapper;

        public MapperDAL()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Material, MaterialModel>();
                    //.ForMember(dest => dest.MaterialId, act => act.MapFrom(src => src.MaterialId))
                    //.ForMember(dest => dest.CurrentVersion, act => act.MapFrom(src => src.CurrentVersion))
                    //.ForMember(dest => dest.MaterialCategory, act => act.MapFrom(src => src.MaterialCategory));
                cfg.CreateMap<MaterialVersion, MaterialVersionModel>();
                //.ForMember(dest => dest.MaterialId, act => act.MapFrom(src => src.MaterialId))
                //.ForMember(dest => dest.MaterialName, act => act.MapFrom(src => src.MaterialName))
                //.ForMember(dest => dest.MaterialType, act => act.MapFrom(src => src.MaterialType))
                //.ForMember(dest => dest.MaterialUploadDate, act => act.MapFrom(src => src.MaterialUploadDate))
                //.ForMember(dest => dest.UploadId, act => act.MapFrom(src => src.UploadId))
                //.ForMember(dest => dest.VersionNumber, act => act.MapFrom(src => src.VersionNumber))
                //.ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                //.ForMember(dest => dest.MaterialSize, act => act.MapFrom(src => src.MaterialSize))
                //.ForMember(dest => dest.MaterialPath, act => act.MapFrom(src => src.MaterialPath));
            }).CreateMapper();
        }

        public MaterialModel MapMaterialModel(Material material)
        {
            var materialmodel =_mapper.Map<MaterialModel>(material);
            return materialmodel;
        }

    }
}
