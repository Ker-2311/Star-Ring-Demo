using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGrid : ConfigGrid
{
    private Weapon _weapon;

    public void EquipWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _icon.SetActive(true);
        //_icon.GetComponent<Image>().sprite = weapon.icon;
    }


}
