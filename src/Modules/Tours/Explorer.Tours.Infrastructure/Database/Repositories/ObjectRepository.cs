using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ObjectRepository : CrudDatabaseRepository<Core.Domain.Object, ToursContext>, IObjectRepository
    {
        private readonly DbSet<Core.Domain.Object> _dbSet;

        public ObjectRepository(ToursContext dbContext) : base(dbContext) 
        {
            _dbSet = dbContext.Set<Core.Domain.Object>();
        }

        public PagedResult<Core.Domain.Object> GetPublicPaged(int page, int pageSize)
        {
            var task = _dbSet.Where(o => o.Status == Core.Domain.Enum.ObjectStatus.PUBLIC).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Core.Domain.Object> GetPublicPagedInRange(int page, int pageSize, double longitude, double latitude, double radius)
        {
            double earthRadius = 6371;
            double degreeRad = (Math.PI / 180.0);

            double longDegree = longitude * degreeRad;
            double latDegree = latitude * degreeRad;


            // hello darkness my old friend
            // I've come to talk with you againnnnn
            var task = _dbSet
                .Where(o => 
                    2 * earthRadius *
                    Math.Atan2 (
                        Math.Sqrt(
                            Math.Sin((o.Latitude * degreeRad - latDegree) / 2) * Math.Sin((o.Latitude * degreeRad - latDegree) / 2)
                            +
                            Math.Cos(o.Latitude * degreeRad) * Math.Cos(latDegree)
                            *
                            Math.Sin((o.Longitude * degreeRad - longDegree) / 2) * Math.Sin((o.Longitude * degreeRad - longDegree) / 2)
                            )
                        ,
                        Math.Sqrt(
                            1
                            -
                                (
                                Math.Sin((o.Latitude * degreeRad - latDegree) / 2) * Math.Sin((o.Latitude * degreeRad - latDegree) / 2)
                                +
                                Math.Cos(o.Latitude * degreeRad) * Math.Cos(latDegree)
                                *
                                Math.Sin((o.Longitude * degreeRad - longDegree) / 2) * Math.Sin((o.Longitude * degreeRad - longDegree) / 2)
                                )
                            )
                    )
                    <= radius)
                .GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
