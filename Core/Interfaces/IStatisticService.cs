using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IStatisticService
    {
        Task<IEnumerable<Statistic>> GetStatisticAsync(AdvertisementOrderBySpecParams adParams);
    }
}
