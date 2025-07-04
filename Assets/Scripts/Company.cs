using System.IO.Pipes;
using UnityEngine;

public class Company
{
    public string companyName { get; private set; } = "CompanyName";
    public int stockPrice { get; private set; } = 0;
    public int previousStockPrice { get; private set; } = 1;
    public PriceChangeDirection priceDirection { get; private set; } = PriceChangeDirection.NoChange;
    public int companyID { get; private set; } = -1;

    public enum PriceChangeDirection
    {
        Up,
        Down,
        NoChange
    }

    public Company(int id, string name, int initialPrice)
    {
        companyID = id;
        companyName = name;
        stockPrice = initialPrice;
    }

    public void UpdatePrice(int newPrice)
    {
        previousStockPrice = stockPrice;
        stockPrice = newPrice;
    }

    public void UpdatePriceDirection(PriceChangeDirection newDir)
    {
        priceDirection = newDir;
    }

}
