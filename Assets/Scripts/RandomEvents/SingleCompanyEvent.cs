
using System.Collections.Generic;
using UnityEngine;

public class SingleCompanyEvent : IRandomEvent, IGameLogs
{
    private Company company;

    public SingleCompanyEvent()
    {
        List<Company> companiesList = StockMarket.Instance.companies;
        int randomNr = Random.Range(0, companiesList.Count);
        company = companiesList[randomNr];
        log = new Log
        {
            roundNr = GameManager.Instance.roundNr,
            time = System.DateTime.Now,
            message = $"Skandal korporacyjny firmy {company.companyName}. Akcje spad³y o 30%."
        };
    }

    float IRandomEvent.probability { get; } = 0.15f;

    public Log log { get;  set; }

    public bool Apply()
    {
        int newPrize = Mathf.RoundToInt(company.stockPrice * 0.7f);
        company.UpdatePrice(newPrize);
        return true;
    }

    public Log GetLog()
    {
        return log;
    }
}
