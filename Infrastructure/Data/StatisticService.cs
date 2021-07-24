using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Statistic>> GetStatisticAsync(AdvertisementOrderBySpecParams adParams)
        {
            var count = adParams.Count;
            var spec = new AdvertisementWithCategoriesOnlyOrderBySpecification();
            switch (adParams.OrderByCriteria)
            {
                case OrderByCriteria.CategoriesAsc:
                    var views = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);
                    var sortedCategories = views.GroupBy(x => x.Category.Name, y => y.ViewsCount)
                        .Select(g => new Statistic
                        {
                            Name = g.Key,
                            ViewCount = g.Sum()
                        }).OrderByDescending(x => x.ViewCount).Take(count);
                    return sortedCategories;

                case OrderByCriteria.PostsAsc:
                    var posts = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);
                    var sortedPosts = posts.Select(x => new Statistic
                    {
                        Name = x.Name,
                        ViewCount = x.ViewsCount
                    }).Take(count);
                    return sortedPosts;

                case OrderByCriteria.TypesAsc:
                    var types = await _unitOfWork.Repository<Advertisement>().ListAsync(spec);
                    var sortedTypes = types.GroupBy(x => x.Type, y => y.ViewsCount)
                        .Select(g => new Statistic
                        {
                            Name = g.Key.ToString(),
                            ViewCount = g.Sum()
                        }).OrderByDescending(x => x.ViewCount).Take(count);
                    return sortedTypes;

                default:
                    return new Statistic[] {new() { Name = "Not found", ViewCount = 0}};
            }
        }
    }
}
