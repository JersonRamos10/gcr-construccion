using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Mappings
{
    public class IngresoProfile : Profile
    {
        public IngresoProfile()
        {
            CreateMap<Ingreso, IngresoDto>();
            CreateMap<IngresoCreateDto, Ingreso>();
        }
    }
}
