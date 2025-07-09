using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats
{

    public List<RoundStats> statsHistory = new List<RoundStats>();


    public void SaveCurrentRoundStats(Dictionary<Company, int> ownedStocks, Dictionary<Company, int> stocksBoughtThisRound, Dictionary<Company, int> stocksSoldThisRound)
    {
        Dictionary<Company, int> stocksPrices = new Dictionary<Company, int>();

        foreach (Company company in StockMarket.Instance.companies)
        {
            stocksPrices.Add(company, company.stockPrice);
        }

        var roundStats = new RoundStats(
            GameManager.Instance.roundNr, DateTime.Now, ownedStocks, stocksBoughtThisRound, stocksSoldThisRound, stocksPrices
        );
        statsHistory.Add(roundStats);
    }


    public Dictionary<string, int> GetFinalStats(int currentCash, int startingCash, Dictionary<Company, int> ownedStocks)
    {
        Dictionary<string, int> stats = new();
        stats.Add("Koñcowy kapita³", GetFinalCapitalStat(currentCash, ownedStocks));
        stats.Add("Ca³kowity zysk/strata", GetTotalGainOrLoseStat(currentCash, startingCash, ownedStocks));

        var (companyBest, amountOfBest) = GetBestInvestment();
        stats.Add("Najlepsza inwestycja - " + companyBest.companyName, amountOfBest);

        var (companyWorst, amountOfWorst) = GetWorstInvestment();
        stats.Add("Najgorsza inwestycja - " + companyWorst.companyName, amountOfWorst);

        return stats;
    }

    private int GetFinalCapitalStat(int currentCash, Dictionary<Company, int> ownedStocks)
    {
        int totalCapital = currentCash;
        totalCapital += ownedStocks.Sum(x => x.Key.stockPrice * x.Value);
        return totalCapital;
    }

    private int GetTotalGainOrLoseStat(int currentCash, int startingCash, Dictionary<Company, int> ownedStocks)
    {
        int finalCapital = GetFinalCapitalStat(currentCash, ownedStocks);
        int gainOrLose = finalCapital - startingCash;
        return gainOrLose;
    }

    private (Company, int) GetBestInvestment()
    {
        var companyProfits = CalculateCompanyProfits(statsHistory);
        var bestInvestment = companyProfits.OrderByDescending(kvp => kvp.Value).First();
        return (bestInvestment.Key, bestInvestment.Value);
    }

    private (Company, int) GetWorstInvestment()
    {
        var companyProfits = CalculateCompanyProfits(statsHistory);
        var worstInvestment = companyProfits.OrderBy(kvp => kvp.Value).First();
        return (worstInvestment.Key, worstInvestment.Value);
    }

    public static Dictionary<Company, int> CalculateCompanyProfits(List<RoundStats> rounds)
    {
        var allCompanies = new HashSet<Company>();
        foreach (var round in rounds)
        {
            foreach (var c in round.OwnedStocks.Keys) allCompanies.Add(c);
            foreach (var c in round.StocksBought.Keys) allCompanies.Add(c);
            foreach (var c in round.StocksSold.Keys) allCompanies.Add(c);
            foreach (var c in round.StocksPrices.Keys) allCompanies.Add(c);
        }

        var companyProfits = new Dictionary<Company, int>();

        foreach (var company in allCompanies)
        {
            int totalBought = 0, totalBoughtSpent = 0;
            int totalSold = 0, totalSoldEarned = 0;
            int owned = 0;
            int lastPrice = 0;

            foreach (var round in rounds)
            {
                if (round.StocksBought.TryGetValue(company, out int buy))
                {
                    totalBought += buy;
                    if (round.StocksPrices.TryGetValue(company, out int price))
                        totalBoughtSpent += buy * price;
                }

                if (round.StocksSold.TryGetValue(company, out int sell))
                {
                    totalSold += sell;
                    if (round.StocksPrices.TryGetValue(company, out int price))
                        totalSoldEarned += sell * price;
                }

                if (round.OwnedStocks.TryGetValue(company, out int nowOwned))
                    owned = nowOwned;

                if (round.StocksPrices.TryGetValue(company, out int priceNow))
                    lastPrice = priceNow;
            }
            // Wartoœæ posiadanych akcji na koniec gry
            int finalValueOfOwned = owned * lastPrice;
            int profit = finalValueOfOwned + totalSoldEarned - totalBoughtSpent;

            companyProfits[company] = profit;
        }

        return companyProfits;
    }
}
