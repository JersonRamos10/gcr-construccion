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
                .ForMember(
                    dest => dest.ProveedorNombre,
                    opt => opt.MapFrom(src =>
                        src.Proveedor != null ? src.Proveedor.Nombre : "Sin proveedor"
                    )
                )
                .ForMember(
                    dest => dest.CategoriaNombre,
                    opt => opt.MapFrom(src =>
                        src.CategoriaMaterial != null ? src.CategoriaMaterial.Nombre : "Sin categoría"
                    )
                )
                .ForMember(
                    dest => dest.Medida,
                    opt => opt.MapFrom(src => src.Medida)
                );

            CreateMap<CompraMaterialCreateDto, CompraMaterial>()
                .ForMember(
                    dest => dest.Medida,
                    opt => opt.MapFrom(src => src.Medida)
                );

            CreateMap<CompraMaterialUpdateDto, CompraMaterial>()
                .ForMember(
                    dest => dest.Medida,
                    opt => opt.MapFrom(src => src.Medida)
                );
        }
    }

}