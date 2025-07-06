using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IGameLogs
{
    public int currentCash { get; private set; } = 20000;
    public int startingCash { get; private set; } = 20000;
public Log log{ get; set; }

    public Dictionary<Company, int> ownedStocks = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksBoughtThisRound = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksSoldThisRound = new Dictionary<Company, int>();
    public List<RoundStats> statsHistory = new List<RoundStats>();

    private void UpdateCash(int amount)
    {
        currentCash += amount;
    }

    public bool TryBuyStock(Company company)
    {
        
        if (company.stockPrice <= currentCash)
        {
            UpdateCash(-company.stockPrice);
            AddStock(company);
            
            CheckForMassiveBuy(company);

            EventManager.Instance.RecordMessageEvent(company, "Akcja kupiona! ^^", InfoMessageType.Success);
            return true;
        }
        else
        {
            EventManager.Instance.RecordMessageEvent(company, "Nie masz tyle pieniêdzy", InfoMessageType.Fail);
            return false;
        }

    }

    private void CheckForMassiveBuy(Company company)
    {
        if (stocksBoughtThisRound.ContainsKey(company))
        {
            if (stocksBoughtThisRound[company] >= 10)
            {
                PlayerTriggerEvent randomEvent = new PlayerTriggerEvent(company);
                RandomEventManager.Instance.TryTriggerEvent(randomEvent);
            }
        }
    }

    public bool TrySellStock(Company company)
    {
        if (ownedStocks.ContainsKey(company) && ownedStocks[company] > 0)
        {
            UpdateCash(company.stockPrice);
            SubstractStock(company);

            EventManager.Instance.RecordMessageEvent(company, "Akcja sprzedana! \\(^o^)/", InfoMessageType.Success);
            return true;
        }

        EventManager.Instance.RecordMessageEvent(company, "Nie masz ¿adnej... ", InfoMessageType.Fail);
        return false;
    }

    public void AddStock(Company company)
    {
        if (ownedStocks.ContainsKey(company))
                ownedStocks[company] += 1;
        else
            ownedStocks[company] = 0;

        if (stocksBoughtThisRound.ContainsKey(company))
            stocksBoughtThisRound[company] += 1;
        else
            stocksBoughtThisRound[company] = 1;
    }

    public void SubstractStock(Company company)
    {
        if (ownedStocks.ContainsKey(company))
            if (ownedStocks[company] > 0)
                ownedStocks[company] -= 1;
        else
            ownedStocks[company] = 0;

        if (stocksSoldThisRound.ContainsKey(company))
            stocksSoldThisRound[company] += 1;
        else
            stocksSoldThisRound[company] = 1;
    }

    public void SetupPlayerStocks(Company company)
    {
        ownedStocks[company] = 0;
        stocksBoughtThisRound[company] = 0;
        stocksSoldThisRound[company] = 0;
    }

    public int GetOwnedStocksCount(Company company)
    {
        if (ownedStocks.TryGetValue(company, out int count))
            return count;
        return 0;
    }

    public void ResetPreviosRoundStocks()
    {
        foreach (Company company in stocksBoughtThisRound.Keys.ToList())
        {
            stocksBoughtThisRound[company] = 0;
        }

        foreach (Company company in stocksSoldThisRound.Keys.ToList())
        {
            stocksSoldThisRound[company] = 0;
        }
    }

    public Log GetLog()
    {
        return new Log
        {
            roundNr = GameManager.Instance.roundNr,
            time = System.DateTime.Now,
            message = GetPlayerStatsLog()
        };
    }

    public string GetPlayerStatsLog()
    {
        int roundNr = GameManager.Instance.roundNr;
        DateTime date = DateTime.Now;

        var sb = new System.Text.StringBuilder();
        //sb.AppendLine($"Runda {roundNr} - {date:yyyy-MM-dd HH:mm}");
        sb.AppendLine($"\nFirma\t\tSprzedano\t    Kupiono\tPosiadane\t\tCena");
        foreach (var company in StockMarket.Instance.companies)
        {
            int sold = stocksSoldThisRound.TryGetValue(company, out int s) ? s : 0;
            int bought = stocksBoughtThisRound.TryGetValue(company, out int b) ? b : 0;
            int owned = ownedStocks.TryGetValue(company, out int o) ? o : 0;
            float price = company.stockPrice;
            sb.AppendLine($"{company.companyName}\t\t{sold}\t\t{bought}\t\t{owned}\t\t{price}");
        }

        return sb.ToString();
    }

    public void SaveCurrentRoundStats()
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

    
    public Dictionary<string, int> GetFinalStats()
    {
        Dictionary<string, int> stats = new();
        stats.Add("Koñcowy kapita³", GetFinalCapitalStat());
        stats.Add("Ca³kowity zysk/strata", GetTotalGainOrLoseStat());

        var(companyBest, amountOfBest) = GetBestInvestment();
        stats.Add("Najlepsza inwestycja - " + companyBest.companyName, amountOfBest);

        var (companyWorst, amountOfWorst) = GetWorstInvestment();
        stats.Add("Najgorsza inwestycja - " + companyWorst.companyName, amountOfWorst);

        return stats;
    }

    private int GetFinalCapitalStat()
    {
        int totalCapital = currentCash;
        totalCapital += ownedStocks.Sum(x => x.Key.stockPrice * x.Value);
        return totalCapital;
    }

    private int GetTotalGainOrLoseStat()
    {
        int finalCapital = GetFinalCapitalStat();
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
