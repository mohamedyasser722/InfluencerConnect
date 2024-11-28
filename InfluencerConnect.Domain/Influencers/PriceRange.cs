using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Influencers;
public sealed class PriceRange
{
    public decimal MinPrice { get; }
    public decimal MaxPrice { get; }

    public PriceRange(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Min price cannot be negative.", nameof(minPrice));

        if (maxPrice < 0)
            throw new ArgumentException("Max price cannot be negative.", nameof(maxPrice));

        if (minPrice > maxPrice)
            throw new ArgumentException("Min price cannot be greater than max price.", nameof(minPrice));

        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }
}
