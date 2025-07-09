using System;
using TMPro;
using UnityEngine;

public class PlayerCashPanel : MonoBehaviour, IUpdatablePanel
{
    [SerializeField] TMP_Text cashText;
    [SerializeField] Player player;

    private void OnEnable()
    {
        EventManager.Instance.OnPlayerCashChanged += OnPlayerCashChanged;
    }

    private void OnPlayerCashChanged()
    {
        UpdatePanel();
    }


    public void UpdatePanel()
    {
        cashText.text = player.currentCash.ToString();
    }
}
