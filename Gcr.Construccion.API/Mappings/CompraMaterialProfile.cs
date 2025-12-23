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
            .ForMember(d => d.ProveedorNombre,
                opt => opt.MapFrom(s => s.Proveedor.Nombre))
            .ForMember(d => d.CategoriaNombre,
                opt => opt.MapFrom(s => s.CategoriaMaterial.Nombre));
            CreateMap<CompraMaterialCreateDto, CompraMaterial>();
        }
    }
}