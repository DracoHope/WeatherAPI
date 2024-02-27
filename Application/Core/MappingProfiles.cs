/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/


using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            /*
                Basically it means get and track a <Activity> object and then replace this the other <Activity> object
                This is applicable to updating an object in the <Edit> class
                The Auto Mapper will help to Map one <Activity> to the other <Activity>
                The Auto Mapper will try to map the properties name of the <Activity> object from one <Activity> object to the other <Activity> object
            */
            CreateMap<Activity, Activity>();
        }
    }
}