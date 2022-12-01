using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] Item activeItem;
    [SerializeField] WeaponBase weaponEquipped;
    EntityStats entityStats;
    EntityBase entity;
    public float Charge { get; private set; } = 100;
    public float MaxCharge { get; private set; } = 100;
    public float Ammo { get; private set; }
    public float MaxAmmo { get; private set; }
    public float AmmoRegen { get; private set; }
    float lastAmmoChangeTimer = 0;
     float ammoRegenDelay = 1;
    public Action onItemsReferesh;
    public Action onAmmoChanged;

    private void Start()
    {
        entity = GetComponent<EntityBase>();
        entityStats = entity.EntityStats;
        foreach (var item in items)
        {
            item.OnPickup(entity);
        }
        if (activeItem)
            SetActiveItem(activeItem);
        Charge = MaxCharge;
        AmmoRefresh();
    }
    private void FixedUpdate()
    {
        foreach (var item in items)
        {
            item.OnUpdate(entity);
        }
        if (Charge < MaxCharge)
        {
            Charge += Time.deltaTime;
        }
        else if(Charge > MaxCharge)
        {
            Charge = MaxCharge;
        }
        if(Ammo < MaxAmmo)
        {
            if (lastAmmoChangeTimer >= ammoRegenDelay || weaponEquipped.ammoRegenWhenFire)
            {
                Ammo += Time.fixedDeltaTime * AmmoRegen;
                if (Ammo > MaxAmmo)
                {
                    Ammo = MaxAmmo;
                }
                onAmmoChanged?.Invoke();
            }
            else
            {
                lastAmmoChangeTimer += Time.fixedDeltaTime;
            }
        }
    }
    public void ChangeAmmo(float value)
    {
        Ammo = value;
        lastAmmoChangeTimer = 0;
        onAmmoChanged?.Invoke();
    }
    public void Give(Item item)
    {
        item.OnPickup(entity);
        //Log.WriteInLog("You got an item: " + item.name);
        items.Add(item);
        Refresh();
    }
    public WeaponBase GetWeapon()
    {
        return weaponEquipped;
    }
    public List<Item> GetInventory()
    {
        return items;
    }
    public Item GetActiveItem()
    {
        return activeItem;
    }
    public void SetActiveItem(Item item)
    {
        activeItem = item;
        MaxCharge = item.chargeNeeded;
        Charge = 0;
        Refresh();
    }
    public void SetWeapon(WeaponBase weapon)
    {
        weaponEquipped = weapon;
        AmmoRefresh();
        Refresh();
    }
    public void AmmoRefresh()
    {
        Ammo = weaponEquipped.ammo;
        MaxAmmo = weaponEquipped.ammo;
        AmmoRegen = weaponEquipped.ammoRegenPerSecond;
        ammoRegenDelay = weaponEquipped.ammoRegenDelay;
        onAmmoChanged?.Invoke();
    }
    public void ActivateArtifact()
    {
        if (Charge < activeItem.chargeNeeded) return;
        activeItem.OnActivate(entity);
        Charge = 0;
    }
    public void Refresh()
    {
        entityStats.Attack.RemoveAllModifiers();
        entityStats.Health.RemoveAllModifiers();
        entityStats.Speed.RemoveAllModifiers();
        entityStats.CollectibleChance.RemoveAllModifiers();
        foreach (var item in items)
        {
            entityStats.Attack.AddModifier(item.Attack);
            entityStats.Health.AddModifier(item.Health);
            entityStats.Speed.AddModifier(item.Speed);
            entityStats.CollectibleChance.AddModifier(item.CollectibleChance);
        }
        onItemsReferesh?.Invoke();
    }
}

