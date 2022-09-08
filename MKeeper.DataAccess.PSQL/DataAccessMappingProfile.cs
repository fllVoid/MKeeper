using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MKeeper.Domain.Models;
using MKeeper.DataAccess.PSQL.Entities;

namespace MKeeper.DataAccess.PSQL;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<Domain.Models.Account, Entities.Account>();
        CreateMap<Domain.Models.Category, Entities.Category>();
        CreateMap<Domain.Models.Currency, Entities.Currency>();
        CreateMap<Domain.Models.Debt, Entities.Debt>();
        CreateMap<Domain.Models.Intervals, Entities.Intervals>();
        CreateMap<Domain.Models.ScheduledTransaction, Entities.ScheduledTransaction>();
        CreateMap<Domain.Models.Transaction, Entities.Transaction>();
        CreateMap<Domain.Models.Transfer, Entities.Transfer>();
        CreateMap<Domain.Models.User, Entities.User>();
    }
}
