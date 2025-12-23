using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Mappings
{
    public class CompraMaterialProfile : Profile
    {
        
        public CompraMaterialProfile()
        {
                CreateMap<CompraMaterial, CompraMaterialDto>()
            .ForMember(dest => dest.ProveedorNombre,
                opt => opt.MapFrom(src => src.Proveedor.Nombre))
            .ForMember(dest => dest.CategoriaNombre,
                opt => opt.MapFrom(src => src.Categoria.Nombre));
            CreateMap<CompraMaterialCreateDto, CompraMaterial>();
        }
    }
}