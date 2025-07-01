using System.IO.Pipes;
using UnityEngine;

public class Company
{
    public string companyName { get; private set; } = "CompanyName";
    public int stockPrize { get; private set; } = 0;
    public int previousStockPrize { get; private set; } = 0;
    public PriceChangeDirection prizeDirection { get; private set; } = PriceChangeDirection.NoChange;

    public enum PriceChangeDirection
    {
        Up,
        Down,
        NoChange
    }

    public Company(string name, int initialPrize)
    {
        companyName = name;
        stockPrize = initialPrize;
    }

    public void UpdatePrize(int newPrize)
    {
        previousStockPrize = stockPrize;
        stockPrize = newPrize;
    }

    public void UpdatePrizeDirection(PriceChangeDirection newDir)
    {
        prizeDirection = newDir;
    }


}
