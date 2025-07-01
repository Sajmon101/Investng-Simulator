using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentCash { get; private set; } = 0;
    public Dictionary<string, int> ownedStocks = new Dictionary<string, int>();
    public Dictionary<Company, int> stocksBoughtThisRound = new Dictionary<Company, int>();
    public Dictionary<Company, int> stocksSoldThisRound = new Dictionary<Company, int>();

    private void UpdateCash(int amount)
    {
        currentCash += amount;
    }

    public void AddStocks(string companyName)
    {
        if (ownedStocks.ContainsKey(companyName))
            ownedStocks[companyName] += 1;
        else
            ownedStocks[companyName] = 1;
    }

    public void SetupPlayerStocks(Company company)
    {
        ownedStocks[company.companyName] = 0;
        stocksBoughtThisRound[company] = 0;
        stocksSoldThisRound[company] = 0;
    }
}
