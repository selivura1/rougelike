using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class PlayerAmmoBar : MonoBehaviour
{
    ProgressBar bar;
    Inventory playerInventory;
    private void Start()
    {
        bar = GetComponent<ProgressBar>();
        playerInventory = ReferenceContainer.PlayerSpawner.GetPlayer().GetComponent<Inventory>();
        playerInventory.onAmmoChanged += UpdateBar;
    }
    private void OnDestroy()
    {
        playerInventory.onAmmoChanged -= UpdateBar;
    }
    private void UpdateBar()
    {
        bar.CurrentValue = playerInventory.Ammo;
        bar.Max = playerInventory.MaxAmmo;
    }
}
