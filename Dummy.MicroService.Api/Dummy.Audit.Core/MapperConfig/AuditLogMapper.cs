using AutoMapper;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.MapperConfig
{
    public class AuditLogMapper : Profile
    {
        public AuditLogMapper()
        {
            CreateMap<Auditlog, AuditLogGetViewModel>();
            CreateMap<OrderType, ValuableViewModel>()
                .ForMember(dest => dest.Value, orig => orig.MapFrom(ent => ent.Name));
            CreateMap<OrderColorCube, ValuableViewModel>()
                        .ForMember(dest => dest.Value, orig => orig.MapFrom(ent => ent.Name));
        }
    }
}
