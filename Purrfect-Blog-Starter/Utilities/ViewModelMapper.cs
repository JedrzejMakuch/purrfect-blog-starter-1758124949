using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Purrfect_Blog_Starter.Utilities
{
    public class ViewModelMapper
    {
        public static CatFactsViewModel ToCatFactsViewModel(CatFactsDto dto, IQueryable<SavedFactDto> dtos)
        {
            var items = dto?.Data ?? Enumerable.Empty<CatFactDto>();

            var savedTexts = new HashSet<string>(
                dtos.Select(d => d.Text).ToList(),
                StringComparer.OrdinalIgnoreCase 
            );

            var facts = items
                .Select(x => new CatFactViewModel
                {
                    Description = x.Fact,
                    IsAlreadySaved = savedTexts.Contains(x.Fact) 
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Description))
                .ToList();

            return new CatFactsViewModel { Facts = facts };
        }

        public static IEnumerable<SavedFactsViewModel> ToSavedFactsViewModel(IQueryable<SavedFactDto> dtos)
        {
            return dtos.Select(x => new SavedFactsViewModel
            {
                Description = x.Text,
                Id = x.Id,
            }).ToList();
        }

        public static CatFactViewModel ToFactViewModel(CatFactDto dto)
        {
            var vm = new CatFactViewModel
            {
                Description = dto.Fact
            };

            return vm;
        }
    }
}