using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SectorEvent : IRandomEvent, IGameLogs
{
    private Sector sector;

    public float probability { get;} = 0.15f;

    public Log log { get; }

    public Log GetLog()
    {
        return log;
    }

    public SectorEvent()
    {
        Sector[] sectors = (Sector[])System.Enum.GetValues(typeof(Sector));
        int randomNr = Random.Range(0, sectors.Length);
        sector = sectors[randomNr];

        log = new Log
        {
            roundNr = GameManager.Instance.roundNr,
            time = System.DateTime.Now,
            message = $"Nowe regulacje w sektorze {sector.ToString()}. Akcje wszystkich firm w tym sektorze spad³y o 35%."
        };
        
    }

    public bool Apply()
    {
        List<Company> companiesInSector = StockMarket.Instance.companies.FindAll(c => c.sectors.Contains(sector));

        foreach (Company company in companiesInSector)
        {
            int newPrice = Mathf.RoundToInt(company.stockPrice * 0.65f); 
            company.UpdatePrice(newPrice);
        }

        return true;
    }
}
