using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentCash { get; private set; } = 0;
    public Dictionary<string, int> ownedStocks = new Dictionary<string, int>();

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
}
