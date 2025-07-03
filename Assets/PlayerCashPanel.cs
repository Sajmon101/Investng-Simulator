using TMPro;
using UnityEngine;

public class PlayerCashPanel : MonoBehaviour
{
    [SerializeField] TMP_Text cashText;
    [SerializeField] Player player;

    public void UpdateDisplay()
    {
        cashText.text = player.currentCash.ToString();
    }
}
