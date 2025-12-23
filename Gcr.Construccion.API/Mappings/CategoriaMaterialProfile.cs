using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Mappings
{
    
    public class CategoriaMaterialProfile : Profile
    {
        
        public CategoriaMaterialProfile()
        {
            CreateMap<CategoriaMaterial, CategoriaMaterialDto>();
            CreateMap<CategoriaMaterialCreateDto, CategoriaMaterial>();
        }

    }

}