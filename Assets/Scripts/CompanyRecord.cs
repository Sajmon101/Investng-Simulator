using NUnit.Framework;
using TMPro;
using UnityEngine;

public class CompanyRecord : MonoBehaviour
{
    public TMP_Text companyNameText;
    public TMP_Text stockPrizeText;
    public TMP_Text ownedStocks;
    public Company company;

    private void Start()
    {
        SetCompanyData(company.companyName, company.stockPrize, 0);
    }

    public void SetCompanyData(string name, int stockPrize, int ownedStocks)
    {
        companyNameText.text = name;
        stockPrizeText.text = stockPrize.ToString();
        this.ownedStocks.text = ownedStocks.ToString();
    }
}
