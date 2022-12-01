using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UpgradeSlotUI : MonoBehaviour
{
    [SerializeField] Image Image;
    private int slot;
    private UpgradeSelect upgrade;
    LevelUpUI levelUpUI;
    [SerializeField] TooltipTrigger tooltipTrigger;
    public void Setup(int slot)
    {
        upgrade = FindObjectOfType<UpgradeSelect>();
        levelUpUI = FindObjectOfType<LevelUpUI>();
        this.slot = slot;
        Item item = upgrade.Slots[slot];
        Image.sprite = item.Picture;
        tooltipTrigger.header = item.name;
        tooltipTrigger.content = item.GetDescription();
    }
    public void OnClick()
    {
        upgrade.SelectSlotByIndex(slot);
        levelUpUI.UsePoint();
    }
}
