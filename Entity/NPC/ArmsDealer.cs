using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsDealer : Trigger
{
    public WeaponBase[] Weapons { get; private set; }
    public Inventory PlayerInventory { get; private set; }
    HUDActivator HUDActivator;
    private GunShopUI gunShopUI;

    private void Setup()
    {
        PlayerInventory = FindObjectOfType<PlayerSpawner>().GetPlayer().GetComponent<Inventory>();
        HUDActivator = FindObjectOfType<HUDActivator>();
        gunShopUI = HUDActivator.GetGunSelect().GetComponent<GunShopUI>();
        Weapons = FindObjectOfType<Database>().GetUnlockedWeapons().ToArray();
    }
    public void Equip(int index)
    {
        PlayerInventory.SetWeapon(Weapons[index]);
    }
    public bool AlreadyEquipped(int index)
    {
        return PlayerInventory.GetWeapon() == Weapons[index];
    }
    public override void OnTouch()
    {
        Setup();
        HUDActivator.SetActiveGunSelect(true);
        gunShopUI.SetDisplay(this);
    }
}
