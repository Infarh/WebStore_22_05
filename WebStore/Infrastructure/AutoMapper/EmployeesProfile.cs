﻿using AutoMapper;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.AutoMapper;

public class EmployeesProfile : Profile
{
    public EmployeesProfile()
    {
        CreateMap<Employee, EmployeeViewModel>()
           .ForMember(m => m.Name, o => o.MapFrom(e => e.FirstName))
           .ReverseMap();
    }
}