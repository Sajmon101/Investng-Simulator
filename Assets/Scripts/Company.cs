using UnityEngine;

public class Company
{
    public string companyName { get; private set; } = "CompanyName";
    public int stockPrize { get; private set; } = -1;

    public Company(string name, int initialPrize)
    {
        companyName = name;
        stockPrize = initialPrize;
    }

    public void UpdatePrize(int amount)
    {
        stockPrize += amount;
    }


}
