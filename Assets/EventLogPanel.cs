using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class EventLogPanel : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform contentParent;

    private void HandleRandomEventTriggered(IRandomEvent randomEvent)
    {
        AddLog(randomEvent.GetAlertDescription());
        Debug.Log("Log added: ");
    }

    public void AddLog(string logText)
    {
        GameObject row = Instantiate(rowPrefab, contentParent);
        row.GetComponentInChildren<TMP_Text>().text = logText;

    }
}
