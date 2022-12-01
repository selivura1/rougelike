using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Image _weapon;
    [SerializeField] Image _legendary;
    [SerializeField] ProgressBar _chargeBar;
    [SerializeField] Transform _inventoryObject;
    [SerializeField] TooltipTrigger _weaponTrigger;
    [SerializeField] TooltipTrigger _activeTrigger;
    Inventory inventory;
    EntityBase _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerControl>().GetComponent<EntityBase>();
        inventory = _player.GetComponent<Inventory>();
        inventory.onItemsReferesh += Refresh;
    }
    public void Refresh()
    {
        _weapon.sprite = inventory.GetWeapon().Icon;
        _legendary.sprite = inventory.GetActiveItem().Picture;
        for (int i = 0; i < _inventoryObject.childCount; i++)
        {
            Destroy(_inventoryObject.GetChild(i).gameObject);
        }
        foreach (var item in inventory.GetInventory())
        {
            var go = new GameObject(item.name);
            go.AddComponent<Image>().sprite = item.Picture;
            var tooltip = go.AddComponent<TooltipTrigger>();
            tooltip.header = item.name;
            tooltip.content = item.GetDescription();
            go.transform.SetParent(_inventoryObject);
            go.transform.localScale = Vector3.one;
        }
        _weaponTrigger.header = inventory.GetWeapon().Name;
        _weaponTrigger.content = inventory.GetWeapon().Desc;
        _activeTrigger.header = inventory.GetActiveItem().name;
        _activeTrigger.content = inventory.GetActiveItem().GetDescription();
    }
    private void OnDestroy()
    {
        inventory.onItemsReferesh -= Refresh;
    }
    private void Update()
    {
        _chargeBar.CurrentValue = inventory.Charge;
        _chargeBar.Max = inventory.MaxCharge;
    }
}
