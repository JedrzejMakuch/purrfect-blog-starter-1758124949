using Purrfect_Blog_Starter.Dtos;
using System.Linq;

namespace Purrfect_Blog_Starter.Services
{
    public interface IFactsService
    {
        void Delete(string userId, int id);
        IQueryable<SavedFactDto> GetSavedFactDtos(string userId);
        void Save(string userId, string description);
    }
}
