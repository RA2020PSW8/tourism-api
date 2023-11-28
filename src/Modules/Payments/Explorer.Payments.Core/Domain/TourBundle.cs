using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TourBundle : Entity
    {
        public string Name { get; init; }
        public double TotalPrice { get; init; }
        public string Status { get; init; }

        public TourBundle() { }

        public TourBundle(long id, string name,  double totalPrice, string status)
        {
            Id = id;
            Name = name;
            TotalPrice = totalPrice;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Invalid Name");
            if (TotalPrice < 0) throw new Exception("Invalid total price");
            if (string.IsNullOrEmpty(Status)) throw new ArgumentException("Invalid status");

        }
    }
}
