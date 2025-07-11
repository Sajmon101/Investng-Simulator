using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Company
{
    public string companyName { get; private set; } = "CompanyName";
    public int stockPrice { get; private set; } = 0;
    public int previousStockPrice { get; private set; } = 1;
    public PriceChangeDirection priceDirection { get; private set; } = PriceChangeDirection.NoChange;
    public int companyID { get; private set; } = -1;
    public List<Sector> sectors = new List<Sector>();
    public int lastRoundWithPlayerTriggerEvent = -1;

    public enum PriceChangeDirection
    {
        Up,
        Down,
        NoChange
    }

    public Company(int id, string name, int initialPrice, List<Sector> sectors)
    {
        companyID = id;
        companyName = name;
        stockPrice = initialPrice;
        this.sectors = sectors;
    }

    public void UpdatePrice(int newPrice)
    {
        previousStockPrice = stockPrice;
        stockPrice = newPrice;
        UpdatePriceDirection();
    }

    private void UpdatePriceDirection()
    {
        if (stockPrice > previousStockPrice)
            priceDirection = PriceChangeDirection.Up;
        else if (stockPrice < previousStockPrice)
            priceDirection = PriceChangeDirection.Down;
        else
            priceDirection = PriceChangeDirection.NoChange;
    }
}
