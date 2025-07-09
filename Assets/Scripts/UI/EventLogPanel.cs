using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventLogPanel : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform contentParent;
    public static List<Log> AllLogs { get; } = new List<Log>();

    private void OnEnable()
    {
        RandomEventManager.Instance.OnRandomEventTriggered += HandleRandomEventTriggered;
    }

    private void HandleRandomEventTriggered(IGameLogs randomEvent)
    {
        AddLog(randomEvent.GetLog());
    }

    public void AddLog(Log log)
    {
        AllLogs.Add(log);
        AddLogToPanel(log);
    }

    public void ShowAllLogs()
    {
        foreach (var log in AllLogs)
        {
            AddLogToPanel(log);
        }
    }

    private void AddLogToPanel(Log log)
    {
        string logText = $"\nRunda {log.roundNr} - {log.time.ToShortTimeString()} - {log.message}";
        GameObject row = Instantiate(rowPrefab, contentParent);
        row.GetComponentInChildren<TMPro.TMP_Text>().text = logText;
    }
}
