using UnityEngine;

public class PlayerTriggerEvent : IRandomEvent
{
    private Company company;
    public int roundNr { get; set; } = 0;
    public PlayerTriggerEvent(Company company)
    {
        this.company = company;
        roundNr = GameManager.Instance.roundNr;
    }

    float IRandomEvent.probability { get; } = 0.15f;

    public void Apply()
    {
        int newPrize = Mathf.RoundToInt(company.stockPrice * 0.75f);
        company.UpdatePrice(newPrize);
    }

    public string GetAlertDescription()
    {
        return $"W odpowiedzi na du¿y zakup akcji firmy {company.companyName} , czêœæ inwestorów instytucjonalnych zdecydowa³a siê na sprzeda¿ akcji. Cana akcji spad³a o 25%";
    }
}
