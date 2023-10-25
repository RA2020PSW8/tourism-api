using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ToursContext _dbContext;
        public EquipmentRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        IEnumerable<Equipment> IEquipmentRepository.GetAll()
        {
            return _dbContext.Equipment.ToList();
        }
    }
}
