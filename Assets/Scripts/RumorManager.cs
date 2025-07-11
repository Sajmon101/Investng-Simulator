using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RumorManager : MonoBehaviour
{

    public static RumorManager Instance { get; private set; }
    private List<Rumor> rumors;
    private Rumor currentRumor;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        LoadRumors();
        DrawNewRumor();
    }

    public void LoadRumors()
    {
        Debug.Log("Wczytywanie plotek...");
        TextAsset jsonText = Resources.Load<TextAsset>("rumors");
        if (jsonText == null)
        {
            Debug.LogError("Nie znaleziono pliku rumors.json w Resources!");
            return;
        }

        rumors = new List<Rumor>(JsonHelper.FromJson<Rumor>(jsonText.text));
    }

    public void DrawNewRumor()
    {
        int i = Random.Range(0, rumors.Count);
        currentRumor = rumors[i];
        rumors.RemoveAt(i);
        Debug.Log(" wylosowana plotka nr"+ i);
    }
    
    public string GetRumorText()
    {
        if (currentRumor != null)
        {
            return currentRumor.rumor;
        }

        return "Nie uda³o siê pobraæ tekstu plotki.";
    }

    public string GetAllCompaniesEffectText()
    {
        return string.Join("\n\n", currentRumor.effects.Select(e => $"{e.companyName}: {e.text}"));
    }

    public float GetPriceChangeForCompany(Company company)
    {
        float percentChange = currentRumor.effects
            .Where(e => e.companyId == company.companyID)
            .Select(e => e.change)
            .FirstOrDefault();
        
        return percentChange;
    }
}
