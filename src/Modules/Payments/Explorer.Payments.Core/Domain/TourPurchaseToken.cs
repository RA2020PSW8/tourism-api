using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TourPurchaseToken : Entity
{
    public int TourId { get; init; }
    public int TouristId { get; init; }

    public TourPurchaseToken(int tourId, int touristId)
    {
        TourId = tourId;
        TouristId = touristId;
    }
}