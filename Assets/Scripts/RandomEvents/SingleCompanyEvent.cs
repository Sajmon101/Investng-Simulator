
using System.Collections.Generic;
using UnityEngine;

public class SingleCompanyEvent : IRandomEvent
{
    private Company company;
    public int roundNr { get; set; } = 0;

    public SingleCompanyEvent()
    {
        List<Company> companiesList = StockMarket.Instance.companies;
        int randomNr = Random.Range(0, companiesList.Count);
        company = companiesList[randomNr];
        roundNr = GameManager.Instance.roundNr;
    }

    float IRandomEvent.probability { get; } = 0.15f;

    public void Apply()
    {
        int newPrize = Mathf.RoundToInt(company.stockPrice * 0.7f);
        company.UpdatePrice(newPrize);
    }

    public string GetAlertDescription()
    {
        return $"Skandal korporacyjny firmy {company.companyName}. Akcje spad³y o 30%.";
    }
}
