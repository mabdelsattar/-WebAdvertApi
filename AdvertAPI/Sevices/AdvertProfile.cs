using AdvertAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Sevices
{
    public class AdvertProfile:Profile
    {
        public AdvertProfile()
        {
            //from , to 
            CreateMap<AdvertModel, AdvertDBModel>();
        }

    }
}
