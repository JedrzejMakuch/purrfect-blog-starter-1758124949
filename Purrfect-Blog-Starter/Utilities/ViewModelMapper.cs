using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.ViewModels;
using System.Linq;

namespace Purrfect_Blog_Starter.Utilities
{
    public class ViewModelMapper
    {
        public static CatFactsViewModel ToCatFactsViewModel(CatFactsDto dto)
        {
            var items = dto?.Data ?? Enumerable.Empty<CatFactDto>();

            var facts = items
                .Select(x => new CatFactViewModel
                {
                    Description = x.Fact
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Description))
                .ToList();

            return new CatFactsViewModel { Facts = facts };
        }
    }
}