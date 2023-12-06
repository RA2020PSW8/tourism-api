using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TourSale: Entity
{
    public TourSale(){}

    public TourSale(int saleId, int tourId)
    {
        SaleId = saleId;
        TourId = tourId;
    }

    public long SaleId { get; init; }
    public int TourId { get; init; }
}