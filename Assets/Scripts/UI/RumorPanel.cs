using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RumorPanel : MonoBehaviour
{
    public TMP_Text rumor_text;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        string rumorText = RumorManager.Instance.GetRumorText();
        rumor_text.text = rumorText;
    }
}
