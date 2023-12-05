using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Tours.Core.Domain;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterRepository : CrudDatabaseRepository<Encounter, EncountersContext>, IEncounterRepository
    {
        private readonly DbSet <Encounter> _dbSet;

        public EncounterRepository(EncountersContext dbContext) : base (dbContext)
        {
            _dbSet = dbContext.Set<Encounter>();
        }
        public PagedResult<Encounter> GetAllByStatus(EncounterStatus status)
        {
            var encounters = _dbSet.AsNoTracking().Where(e => e.Status == status).ToList();
            return new PagedResult<Encounter>(encounters, encounters.Count);
        }

        public IEnumerable<Encounter> GetAllByStatusAndType(EncounterStatus status, EncounterType type)
        {
            return _dbSet.AsNoTracking().Where(e => e.Status == status && e.Type == type).ToList();
        }

        public PagedResult<Encounter> GetNearbyByType(int page, int pageSize, double longitude, double latitude, EncounterType type)
        {
            double earthRadius = 6371;
            var degreeRad = Math.PI / 180.0;

            var longDegree = longitude * degreeRad;
            var latDegree = latitude * degreeRad;

            var task = _dbSet
                .Where(e =>
                    2 * earthRadius *
                    Math.Atan2(
                        Math.Sqrt(
                            Math.Sin((e.Latitude * degreeRad - latDegree) / 2) *
                            Math.Sin((e.Latitude * degreeRad - latDegree) / 2)
                            +
                            Math.Cos(e.Latitude * degreeRad) * Math.Cos(latDegree)
                                                              *
                                                              Math.Sin((e.Longitude * degreeRad - longDegree) / 2) *
                                                              Math.Sin((e.Longitude * degreeRad - longDegree) / 2)
                        )
                        ,
                        Math.Sqrt(
                            1
                            -
                            (
                                Math.Sin((e.Latitude * degreeRad - latDegree) / 2) *
                                Math.Sin((e.Latitude * degreeRad - latDegree) / 2)
                                +
                                Math.Cos(e.Latitude * degreeRad) * Math.Cos(latDegree)
                                                                  *
                                                                  Math.Sin((e.Longitude * degreeRad - longDegree) / 2) *
                                                                  Math.Sin((e.Longitude * degreeRad - longDegree) / 2)
                            )
                        )
                    )
                    <= e.Range
                    && e.Type == type)
                .GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
