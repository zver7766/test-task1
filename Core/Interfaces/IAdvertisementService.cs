using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefraces
{
    public interface IAdvertisementService
    {
        Task<Advertisement> CreateAdAsync(AdvertisementToCreate addCreateParams);
    }
}
