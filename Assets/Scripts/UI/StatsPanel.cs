using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text statsText;
    [SerializeField] Player player;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        string stats = "";
        Dictionary<string,int> finalStats = player.GetFinalStats();

        foreach (var stat in finalStats)
        {
            stats += $"{stat.Key}: {stat.Value} $\n";
        }

        statsText.text = stats;
    }
}
