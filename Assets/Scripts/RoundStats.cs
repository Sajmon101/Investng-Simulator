using System.Collections.Generic;
using System;

public class RoundStats
{
    public int RoundNumber;
    public DateTime Date;
    public Dictionary<Company, int> OwnedStocks;
    public Dictionary<Company, int> StocksBought;
    public Dictionary<Company, int> StocksSold;
    public Dictionary<Company, int> StocksPrices;

    public RoundStats(int roundNumber, DateTime date,
                      Dictionary<Company, int> ownedStocks,
                      Dictionary<Company, int> stocksBought,
                      Dictionary<Company, int> stocksSold,
                      Dictionary<Company, int> stocksPrices)
    {
        RoundNumber = roundNumber;
        Date = date;
        OwnedStocks = new Dictionary<Company, int>(ownedStocks);
        StocksBought = new Dictionary<Company, int>(stocksBought);
        StocksSold = new Dictionary<Company, int>(stocksSold);
        StocksPrices = new Dictionary<Company, int>(stocksPrices);
    }


}