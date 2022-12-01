using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerAmmoText : MonoBehaviour
{
    TMP_Text TMP_Text;
    Inventory playerInventory;
    private void Start()
    {
        TMP_Text = GetComponent<TMP_Text>();
        playerInventory = ReferenceContainer.PlayerSpawner.GetPlayer().GetComponent<Inventory>();
        playerInventory.onAmmoChanged += UpdateText;
    }
    private void OnDestroy()
    {
        playerInventory.onAmmoChanged -= UpdateText;
    }
    private void UpdateText()
    {
        TMP_Text.text = playerInventory.Ammo.ToString("F0") + "/" + playerInventory.MaxAmmo;
    }
}
