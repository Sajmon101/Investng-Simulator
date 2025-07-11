using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class StockMarket : MonoBehaviour
{
    public List<Company> companies { get; private set; }
    [SerializeField] private Player player;
    public static StockMarket Instance { get; private set; }
    [SerializeField]  EventLogPanel eventLogPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CreateCompanies();
    }

    private void CreateCompanies()
    {
        companies = new List<Company>
        {
            new Company(1, "AutoDrive", 1000, new List<Sector> { Sector.Automotive, Sector.Tech }),
            new Company(2, "TechNova", 1500, new List < Sector > { Sector.Tech, Sector.Energy }),
            new Company(3, "GreenFuel", 2000, new List < Sector > { Sector.Automotive, Sector.Energy })
        };
    }


    public void UpdateCompaniesPrices()
    {
        foreach (Company company in companies)
        {
            int newPrize = CountNewPrize(company);
            company.UpdatePrice(newPrize);
        }
    }

    private int CountNewPrize(Company company)
    {
        int newPrice = company.stockPrice;
        //Debug.Log("Current price: " + newPrice);
        newPrice += CalculatePriceModifierFromRumors(company);
        //Debug.Log("Price after rumors: " + newPrice);
        newPrice += CalculatePriceModifierDueToSupply(company);
        //Debug.Log("Price after supply and demand: " + newPrice);

        return newPrice;
    }

    private int CalculatePriceModifierFromRumors(Company company)
    {
        int currentCompanyPrice = company.stockPrice;
        float percentChange = RumorManager.Instance.GetPriceChangeForCompany(company);
        int priceModifier = Mathf.RoundToInt(currentCompanyPrice * percentChange);
        //Debug.Log(percentChange);

        return priceModifier;
    }
    private int CalculatePriceModifierDueToSupply(Company company)
    {
        float baseRandomness = UnityEngine.Random.Range(-1.5f, 1.5f);
        float demandFactor = 0.25f; 
        float supplyFactor = 0.25f; 

        if (player.stocksBoughtThisRound.TryGetValue(company, out int boughtThisRound) && player.stocksSoldThisRound.TryGetValue(company, out int soldThisRound))
        {
            // suma procentowa zmiany
            float percentChange = (boughtThisRound * demandFactor) - (soldThisRound * supplyFactor) + baseRandomness;

            // nowa cena = stara cena * (1 + zmiana%)
            float priceChange = company.stockPrice * (percentChange / 100f);

            return Mathf.RoundToInt(priceChange);
        }

        Debug.LogError("Can't update company price due to supply, demand");
        return 0;
    }

}
