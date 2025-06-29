using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    public List<Company> companies { get; private set; }

    void Awake()
    {
        CreateCompanies();
    }



    private void CreateCompanies()
    {
        companies = new List<Company>
        {
            new Company("TechCorp", 100),
            new Company("HealthInc", 150),
            new Company("FinanceGroup", 200)
        };
    }

    public void UpdateAllCompaniesPrize(int amount)
    {
        foreach (Company company in companies)
        {
            company.UpdatePrize(amount);
        }
    }
}
