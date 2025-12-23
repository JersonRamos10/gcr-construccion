using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Mappings
{
    public class PagoManoDeObraProfile : Profile
    {
        public PagoManoDeObraProfile()
        {
            CreateMap<PagoManoDeObra, PagoManoDeObraDto>()
                .ForMember(d => d.EmpleadoNombre,
                    o => o.MapFrom(s => s.Empleado.NombreCompleto))
                .ForMember(d => d.PagoPorDia,
                    o => o.MapFrom(s => s.Empleado.PagoPorDia));

            CreateMap<PagoManoDeObraCreateDto, PagoManoDeObra>();
        }
    }
}
