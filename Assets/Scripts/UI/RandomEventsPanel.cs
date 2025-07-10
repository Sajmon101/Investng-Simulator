using TMPro;
using UnityEngine;

public class RandomEventsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text randomEventText;

    private string data;

    private void OnEnable()
    {
        Clear();
        UpdatePanel();
        RandomEventManager.Instance.OnRandomEventTriggered += HandleRandomEventTrigger;

    }

    private void HandleRandomEventTrigger(IGameLogs obj)
    {
        SetPanelData(obj.GetLog().message);
    }

    public void UpdatePanel()
    {
        randomEventText.text = data;
    }

    public void SetPanelData(string text)
    {
        data = text;
    }

    public void Clear()
    {
        data = "Rynek stabilny – brak wyj¹tkowych zdarzeñ.";
        randomEventText.text = data;
    }
}
