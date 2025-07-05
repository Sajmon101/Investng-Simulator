using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SectorEvent : IRandomEvent
{
    private Sector sector;

    public int roundNr { get; set; } = 0;

    public float probability { get;} = 0.15f;

    public SectorEvent()
    {
        Sector[] sectors = (Sector[])System.Enum.GetValues(typeof(Sector));
        int randomNr = Random.Range(0, sectors.Length);
        sector = sectors[randomNr];

        roundNr = GameManager.Instance.roundNr;
    }

    public void Apply()
    {
        List<Company> companiesInSector = StockMarket.Instance.companies.FindAll(c => c.sectors.Contains(sector));

        foreach (Company company in companiesInSector)
        {
            int newPrice = Mathf.RoundToInt(company.stockPrice * 0.65f); 
            company.UpdatePrice(newPrice);
        }
    }

    public string GetAlertDescription()
    {
        return $"Nowe regulacje w sektorze {sector.ToString()}. Akcje wszystkich firm w tym sektorze spad³y o 35%.";
    }

}
