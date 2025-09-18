using Purrfect_Blog_Starter.Dtos;
using System.Linq;

namespace Purrfect_Blog_Starter.Repositories
{
    public interface IFactsRepository
    {
        void Delete(string userId, int id);
        IQueryable<SavedFactDto> GetSavedFactDtos(string userId);
        void Save(string userId, string descriptions);
    }
}
