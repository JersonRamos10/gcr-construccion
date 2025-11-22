using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace grc.Construccion.API.Mappings
{
    public class ProveedorProfile : Profile
    {
        public ProveedorProfile()
        {
            CreateMap<Proveedor, ProveedorDto>();
            CreateMap<ProveedorCreateDto, Proveedor>();
        }
    }
}