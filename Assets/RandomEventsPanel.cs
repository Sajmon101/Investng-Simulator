using TMPro;
using UnityEngine;

public class RandomEventsPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text randomEventText;
    private string data;

    public void UpdatePanel()
    {
        randomEventText.text = data;
    }

    public void SetPanelData(string text)
    {
        data = text;
    }
}
