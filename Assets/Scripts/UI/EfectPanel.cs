using TMPro;
using UnityEngine;

public class EfectPanel : MonoBehaviour
{
    [SerializeField] TMP_Text effectsText;

    private void OnEnable()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        effectsText.text = RumorManager.Instance.GetAllCompaniesEffectText();
    }
}
