using System.Runtime.InteropServices.JavaScript;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class Sale:Entity
{
    public Sale(){}

    public Sale(DateOnly startDate, DateOnly endDate, double percentage, int userId)
    {
        StartDate = startDate;
        EndDate = endDate;
        Percentage = percentage;
        UserId = userId;
    }

    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public double Percentage { get; init; }
    public int UserId { get; init; }
    public ICollection<TourSale>? TourSales { get; set; }

    private static void Validate(double percentage, DateOnly startDate, DateOnly endDate)
    {
        if (percentage is > 100 or < 0) throw new ArgumentException("Invalid percentage value");
        if (startDate > endDate) throw new ArgumentException("Start date must be earlier than end date");
    }

}