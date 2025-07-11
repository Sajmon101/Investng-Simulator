using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;

public class CompanyRecord : MonoBehaviour
{
    public TMP_Text companyNameText;
    public TMP_Text stockPrizeText;
    public TMP_Text ownedStocks;
    public Company company;
    public Player player;
    [SerializeField] ArrowChange arrowChange;
    public TMP_Text messageText;
    private Coroutine hideMessageCoroutine;

    private void Start()
    {
        InitializeCompanyData(company.companyName, company.stockPrice, 0);
    }

    public void InitializeCompanyData(string name, int stockPrize, int ownedStocks)
    {
        companyNameText.text = name;
        stockPrizeText.text = stockPrize.ToString();
        this.ownedStocks.text = ownedStocks.ToString();
    }

    public void UpdateDisplay()
    {
        if (company != null)
        {
            stockPrizeText.text = company.stockPrice.ToString();
            arrowChange.ChangeArrow(company.priceDirection);
            ownedStocks.text = player.GetOwnedStocksCount(company).ToString();
        }
    }

    public void OnBuyButtonClicked()
    {
        EventManager.Instance.BuyButtonClickedEvent(company);
        player.TryBuyStock(company);
    }
    public void OnSellButtonClicked()
    {
        EventManager.Instance.SellButtonClickedEvent(company);
        player.TrySellStock(company);
    }

    public void ShowMessage(string message, InfoMessageType type)
    {
        messageText.text = message;
        switch (type)
        {
            case InfoMessageType.Fail:
                messageText.color = Color.red;
                break;
            case InfoMessageType.Success:
                messageText.color = Color.green;
                break;
        }

        if (hideMessageCoroutine != null)
            StopCoroutine(hideMessageCoroutine);

        hideMessageCoroutine = StartCoroutine(HideMessageAfterDelay(2f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
        hideMessageCoroutine = null;
    }
}
