using AutoMapper;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType));
            CreateMap<Employee, EmployeeDetailsDTO>()
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)));
            CreateMap<CreatedEmployeeDTO, Employee>()
            .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdatedEmployeeDTO, Employee>()
            .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
