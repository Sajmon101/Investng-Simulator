using TMPro;
using UnityEngine;

public class EfectPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text effectsText;

    public void UpdatePanel()
    {
        effectsText.text = RumorManager.Instance.GetAllCompaniesEffectText();
    }
}
