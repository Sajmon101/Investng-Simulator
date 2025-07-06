using UnityEngine;

public class PlayerTriggerEvent : IRandomEvent, IGameLogs
{
    private Company company;

    public Log log { get; set; }
    private int currentRound;
    public PlayerTriggerEvent(Company company)
    {
        this.company = company;
        currentRound = GameManager.Instance.roundNr;
        log = new Log
        {
            roundNr = currentRound,
            time = System.DateTime.Now,
            message = $"W odpowiedzi na du¿y zakup akcji firmy {company.companyName} , czêœæ inwestorów instytucjonalnych zdecydowa³a siê na sprzeda¿ akcji. Cana akcji spad³a o 25%"
        };
    }


    float IRandomEvent.probability { get; } = 1f;

    public bool Apply()
    {
        if (company.lastRoundWithPlayerTriggerEvent < currentRound)
        {
            int newPrize = Mathf.RoundToInt(company.stockPrice * 0.75f);
            company.UpdatePrice(newPrize);
            company.lastRoundWithPlayerTriggerEvent = currentRound;
            return true;
        }
        return false;
    }

    public Log GetLog()
    {
        return log;
    }
}
