using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person;

namespace UmfrageWebApi.Helper.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonModelForCreation, Person>();
            CreateMap<PersonModel, Person>();

        }
    }
}
