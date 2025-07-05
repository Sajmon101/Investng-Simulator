using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentCash { get; private set; } = 100000;
    public Dictionary<Company, int> ownedStocks = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksBoughtThisRound = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksSoldThisRound = new Dictionary<Company, int>();

    private void UpdateCash(int amount)
    {
        currentCash += amount;
    }

    public bool TryBuyStock(Company company)
    {
        CheckForMassiveBuy(company);

        if (company.stockPrice <= currentCash)
        {
            UpdateCash(-company.stockPrice);
            AddStock(company);

            EventManager.Instance.RecordMessageEvent(company, "Stock purchased! ^^", InfoMessageType.Success);
            return true;
        }
        else
        {
            EventManager.Instance.RecordMessageEvent(company, "Not enough money", InfoMessageType.Fail);
            return false;
        }

    }

    private void CheckForMassiveBuy(Company company)
    {
        if (stocksBoughtThisRound.ContainsKey(company))
        {
            if (stocksBoughtThisRound[company] >= 10)
            {
                IRandomEvent randomEvent = new PlayerTriggerEvent(company);
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

            EventManager.Instance.RecordMessageEvent(company, "Stock sold! \\(^o^)/", InfoMessageType.Success);
            return true;
        }

        EventManager.Instance.RecordMessageEvent(company, "You don't own any... ", InfoMessageType.Fail);
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
}
