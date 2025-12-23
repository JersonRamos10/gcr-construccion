using AutoMapper;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Mappings
{
    public class EmpleadoProfile : Profile
    {
        
        public EmpleadoProfile()
        {
            CreateMap<Empleado, EmpleadoDto>();
            CreateMap<EmpleadoCreateDto, Empleado>();
            CreateMap<EmpleadoUpdateDto, Empleado>();

        }
    }
}