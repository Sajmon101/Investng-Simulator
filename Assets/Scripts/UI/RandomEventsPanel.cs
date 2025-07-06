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

    public void Clear()
    {
        data = "Rynek stabilny – brak wyj¹tkowych zdarzeñ.";
        randomEventText.text = data;
    }
}
