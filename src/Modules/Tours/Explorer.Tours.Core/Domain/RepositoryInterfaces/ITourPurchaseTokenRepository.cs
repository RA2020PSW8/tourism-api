using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourPurchaseTokenRepository : ICrudRepository<TourPurchaseToken>
    {
        void AddRange(List<TourPurchaseToken> tokens);
        TourPurchaseToken GetByTourAndTourist(int tourId, int touristId);

    }
}
