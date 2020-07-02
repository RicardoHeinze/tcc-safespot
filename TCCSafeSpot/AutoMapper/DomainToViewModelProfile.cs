using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCCSafeSpot.Models;
using TCCSafeSpot.Models.ViewModels.Crimes;

namespace TCCSafeSpot.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CrimeSSP, RetornoCrimeViewModel>();
        }
    }
}