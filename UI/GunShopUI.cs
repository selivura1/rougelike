using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunShopUI : MonoBehaviour
{
    [SerializeField] Button _rightArrow, _leftArrow;
    [SerializeField] Button _equipButton;
    [SerializeField] TMP_Text _weaponName;
    [SerializeField] TMP_Text _weaponDesc;
    [SerializeField] TMP_Text _equipText;
    [SerializeField] Image _weaponImage;
    ArmsDealer currentDealer;
    private int index;
    public void SetDisplay(ArmsDealer dealer)
    {
       currentDealer = dealer;
       Refresh();
    }
    public void Equip()
    {
        currentDealer.Equip(index);
        Refresh();
    }
    public void Right()
    {
        if (index < currentDealer.Weapons.Length - 1)
            index++;
        Refresh();
    }
    public void Left()
    {
        if (index > 0)
            index--;
        Refresh();
    }
    public void Refresh()
    {
        var weapons = currentDealer.Weapons;
        Debug.Log("Current weapon: " + weapons[index].name);
        _weaponImage.sprite = weapons[index].Icon;
        _weaponName.text = weapons[index].Name;
        _weaponDesc.text = weapons[index].Desc;

        _leftArrow.interactable = index > 0 ? true : false;
        _rightArrow.interactable = index < weapons.Length - 1 ? true : false;

        _equipButton.interactable = !currentDealer.AlreadyEquipped(index);
    }
}
