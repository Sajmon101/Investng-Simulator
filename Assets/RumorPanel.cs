using TMPro;
using UnityEngine;

public class RumorPanel : MonoBehaviour, IUpdatablePanel
{
    public TMP_Text rumor_text;
    public void UpdatePanel()
    {
        rumor_text.text = RumorManager.Instance.GetRumorText();
    }
}
