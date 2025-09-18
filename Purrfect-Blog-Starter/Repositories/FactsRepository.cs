using Purrfect_Blog_Starter.Data;
using Purrfect_Blog_Starter.Dtos;
using System.Linq;
using System;

namespace Purrfect_Blog_Starter.Repositories
{
    public class FactsRepository : IFactsRepository
    {
        private readonly ApplicationDbContext _context;
        public FactsRepository(ApplicationDbContext context)
        {   
            _context = context;
        }

        public void Delete(string userId, int id)
        {
            var fact = _context.SavedFacts.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if(fact != null)
            {
                _context.SavedFacts.Remove(fact);
                _context.SaveChanges();
            }
        }

        public IQueryable<SavedFactDto> GetSavedFactDtos(string userId) 
        { 
            return _context.SavedFacts.Where(x => x.UserId == userId);
        }

        public void Save(string userId, string description)
        {
            var fact = new SavedFactDto
            {
                Text = description,
                UserId = userId,
                CreatedAtUtc = DateTime.Now
            };

            _context.SavedFacts.Add(fact);
            _context.SaveChanges();
        }
    }
}