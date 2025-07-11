using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IGameLogs
{
    public int currentCash { get; private set; } = 20000;
    public int startingCash { get; private set; } = 20000;

    public Dictionary<Company, int> ownedStocks = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksBoughtThisRound = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksSoldThisRound = new Dictionary<Company, int>();
    private PlayerStats playerStats;
    public Log log { get; set; }


    //Stats funcions
    public void SaveCurrentRoundStats() => playerStats.SaveCurrentRoundStats(ownedStocks, stocksBoughtThisRound, stocksSoldThisRound);
    public Dictionary<string, int> GetFinalStats() => playerStats.GetFinalStats(currentCash, startingCash, ownedStocks);
    //

    private void Awake()
    {
        playerStats = new PlayerStats();
    }

    public Log GetLog()
    {
        return new Log
        {
            roundNr = GameManager.Instance.roundNr,
            time = System.DateTime.Now,
            message = GetPlayerLog()
        };
    }

    public string GetPlayerLog()
    {
        int roundNr = GameManager.Instance.roundNr;
        DateTime date = DateTime.Now;

        var sb = new System.Text.StringBuilder();
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
            EventManager.Instance.PlayerCashChangedEvent();
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
            EventManager.Instance.PlayerCashChangedEvent();
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

    public void SetupPlayerStocks(List<Company> companies)
    {
        foreach (Company company in companies)
        {
            ownedStocks[company] = 0;
            stocksBoughtThisRound[company] = 0;
            stocksSoldThisRound[company] = 0;
        }
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
}
