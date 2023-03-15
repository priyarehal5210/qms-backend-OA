using AutoMapper;
using DomainLayer.Models;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
 public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<Tasks, TasksDto>().ReverseMap();
            CreateMap<Register, RegisterUserDto>().ReverseMap();
            CreateMap<AssignTasks, AssignTaskDto>().ReverseMap();
            CreateMap<UserSuccess, UserSuccessDto>().ReverseMap();
        }
    }
}
