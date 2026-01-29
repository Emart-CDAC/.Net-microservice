using AnalyticsEngineApi.Data;
using AnalyticsEngineApi.DTOs;
using System.Globalization;

namespace AnalyticsEngineApi.Services;

public class AnalyticsService
{
    private readonly AnalyticsDbContext _db;

    public AnalyticsService(AnalyticsDbContext db)
    {
        _db = db;
    }

    public List<ProductOfferInventoryDTO> GetProductOffersInventory()
    {
        return _db.Products
            .Select(p => new ProductOfferInventoryDTO
            {
                Product_Name = p.Product_Name,

                Discount_Offer =
                    p.Ecard_Price != null && p.Ecard_Price < p.Normal_Price
                        ? Math.Round(
                            ((p.Normal_Price - p.Ecard_Price.Value) / p.Normal_Price) * 100, 2
                          ) + "% OFF"
                        : "No discount available",

                Quantity = p.Available_Quantity ?? 0
            })
            .ToList();
    }

    //public List<DailySalesDTO> GetDailySales()
    //{
    //    return _db.Orders
    //        .GroupBy(o => o.OrderDate.Date)
    //        .Select(g => new DailySalesDTO
    //        {
    //            Date = g.Key.ToString("yyyy-MM-dd"),
    //            TotalRevenue = g.Sum(x => x.TotalAmount),
    //            TotalOrders = g.Count()
    //        })
    //        .ToList();
    //}

    //public List<DailySalesDTO> GetDailySales()
    //{
    //    return new()
    //    {
    //        new() { Date = "2026-01-25", TotalRevenue = 42000, TotalOrders = 20 },
    //        new() { Date = "2026-01-26", TotalRevenue = 61000, TotalOrders = 32 }
    //    };
    //}

    //public List<WeeklyRevenueDTO> GetWeeklyRevenue()
    //{
    //    return _db.Orders
    //        .AsEnumerable()
    //        .GroupBy(o =>
    //            CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
    //                o.OrderDate,
    //                CalendarWeekRule.FirstDay,
    //                DayOfWeek.Monday))
    //        .Select(g => new WeeklyRevenueDTO
    //        {
    //            Week = "Week " + g.Key,
    //            TotalRevenue = g.Sum(x => x.TotalAmount)
    //        })
    //        .ToList();
    //}

    //public List<WeeklyRevenueDTO> GetWeeklyRevenue()
    //{
    //    return new()
    //    {
    //        new() { Week = "2026-W04", TotalRevenue = 245000 },
    //        new() { Week = "2026-W05", TotalRevenue = 310000 }
    //    };
    //}

    //public List<LoyaltyProductDTO> GetLoyaltyProducts()
    //{
    //    return _db.OrderItems
    //        .Where(i => i.IsRedeemed)
    //        .GroupBy(i => i.ProductId)
    //        .Select(g => new LoyaltyProductDTO
    //        {
    //            ProductId = g.Key,
    //            ProductName = _db.Products
    //                               .Where(p => p.Id == g.Key)
    //                               .Select(p => p.Name)
    //                               .FirstOrDefault(),
    //            RedemptionCount = g.Sum(x => x.Quantity)
    //        })
    //        .OrderByDescending(x => x.RedemptionCount)
    //        .Take(10)
    //        .ToList();
    //}

    //public List<LoyaltyProductDTO> GetLoyaltyProducts()
    //{
    //    return new()
    //    {
    //        new() { ProductId = 11, ProductName = "Sony Headphones", RedemptionCount = 85 },
    //        new() { ProductId = 15, ProductName = "Smart Watch", RedemptionCount = 60 }
    //    };
    //}
}
