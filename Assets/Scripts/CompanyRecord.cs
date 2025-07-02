using NUnit.Framework;
using TMPro;
using UnityEngine;

public class CompanyRecord : MonoBehaviour
{
    public TMP_Text companyNameText;
    public TMP_Text stockPrizeText;
    public TMP_Text ownedStocks;
    public Company company;
    [SerializeField] ArrowChange arrowChange;

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
            // this.ownedStocks.text = company.ownedStocks.ToString(); // Uncomment if ownedStocks is a property of Company
        }
    }
}
