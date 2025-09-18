using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.Repositories;
using System.Linq;

namespace Purrfect_Blog_Starter.Services
{
    public class FactsService : IFactsService
    {
        private readonly IFactsRepository _factsRepository;

        public FactsService(IFactsRepository factsRepository)
        {
            _factsRepository = factsRepository;
        }

        public void Delete(string userId, int id)
        {
            _factsRepository.Delete(userId, id);
        }

        public IQueryable<SavedFactDto> GetSavedFactDtos(string userId)
        {
            return _factsRepository.GetSavedFactDtos(userId);
        }

        public void Save(string userId, string description)
        {
            _factsRepository.Save(userId, description);
        }
    }
}