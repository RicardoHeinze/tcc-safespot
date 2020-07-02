using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCCSafeSpot.AutoMapper;

namespace TCCSafeSpot.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configurar()
        {
            Mapper.AddProfile<DomainToViewModelProfile>();
        }


    }
}