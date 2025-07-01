using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StockMarket : MonoBehaviour
{
    public List<Company> companies { get; private set; }
    [SerializeField] private Player player;

    private void OnEnable()
    {
        EventManager.Instance.OnNextRound += HandleOnNextRoundEvent;
    }
    private void OnDisable()
    {
        EventManager.Instance.OnNextRound -= HandleOnNextRoundEvent;
    }

    void Awake()
    {
        CreateCompanies();
        InitializePlayersStocks();
    }


    private void CreateCompanies()
    {
        companies = new List<Company>
        {
            new Company("TechCorp", 1000),
            new Company("HealthInc", 1500),
            new Company("FinanceGroup", 2000)
        };
    }

    public void InitializePlayersStocks()
    {
        foreach (Company company in companies)
        {
            player.SetupPlayerStocks(company);
        }
    }

    private void HandleOnNextRoundEvent()
    {
        UpdateCompaniesPrizes();
        UpdateCompaniesDirections();
    }

    private void UpdateCompaniesPrizes()
    {
        foreach (Company company in companies)
        {
            int newPrize = CountNewPrize(company);
            company.UpdatePrize(newPrize);
            EventManager.Instance.PrizeChangeEvent();
        }
    }

    private void UpdateCompaniesDirections()
    {
        foreach (Company company in companies)
        {
            int stockPrize = company.stockPrize;
            int previousStockPrize = company.previousStockPrize;

            if (stockPrize > previousStockPrize)
            {

                company.UpdatePrizeDirection(Company.PriceChangeDirection.Up);
            }
            else if (stockPrize < previousStockPrize)
            {
                company.UpdatePrizeDirection(Company.PriceChangeDirection.Down);
            }
            else
            {
                company.UpdatePrizeDirection(Company.PriceChangeDirection.NoChange);
            }
        }
    }

    private int CountNewPrize(Company company)
    {
        float baseRandomness = UnityEngine.Random.Range(-200f, 200f);
        float demandFactor = 50f;
        float supplyFactor = 50f;

        if (player.stocksBoughtThisRound.TryGetValue(company, out int boughtThisRound) && player.stocksSoldThisRound.TryGetValue(company, out int soldThisRound))
        {
            float delta = (boughtThisRound * demandFactor) - (soldThisRound * supplyFactor) + baseRandomness;

            int newPrize = company.stockPrize + Mathf.RoundToInt(delta);

            return Math.Max(1, newPrize);
        }

        UnityEngine.Debug.LogError($"Can't read player informations anout company {company.companyName} about bought or sold stocks this round");
        return 0;
    }
}
