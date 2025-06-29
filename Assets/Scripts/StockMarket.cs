using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    public List<Company> companies { get; private set; }

    private void OnEnable()
    {
        EventManager.Instance.OnNextRound += HandleOnNextRoundEvent;
    }

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

    private void HandleOnNextRoundEvent()
    {
        foreach (Company company in companies)
        {
            int newPrize = CountNewPrize();
            company.UpdatePrize(newPrize);
            EventManager.Instance.PrizeChangeEvent();
        }
    }

    private int CountNewPrize()
    {
        int newPrize = 2;

        return newPrize;
    }
}
