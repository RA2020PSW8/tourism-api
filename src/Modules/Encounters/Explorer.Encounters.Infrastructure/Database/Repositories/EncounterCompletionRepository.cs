using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterCompletionRepository : CrudDatabaseRepository<EncounterCompletion, EncountersContext>, IEncounterCompletionRepository
    {
        private readonly DbSet<EncounterCompletion> _dbSet;

        public EncounterCompletionRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbSet = DbContext.Set<EncounterCompletion>();
        }

        public PagedResult<EncounterCompletion> GetPagedByUser(int page, int pageSize, long userId)
        {
            var task = _dbSet.Where(ec => ec.UserId == userId).Include(ec => ec.Encounter).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public EncounterCompletion GetByUserAndEncounter(long userId, long encounterId)
        {
            var encounterCompletion = _dbSet.FirstOrDefault(ec => ec.UserId == userId && ec.EncounterId == encounterId);
            if (encounterCompletion == null) throw new KeyNotFoundException("Not found: " + userId + " + " + encounterId); 
            return encounterCompletion;
        }
    }
}
