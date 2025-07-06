using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text statsText;
    [SerializeField] Player player;
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
