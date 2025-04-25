using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponToggle : ConfigToggle
{
    private Weapon _weapon;

    public void Init(Weapon weapon,GameObject infoPanel)
    {
        _weapon = weapon;
        _infoPanel = infoPanel;
        transform.Find("Name").GetComponent<Text>().text = weapon.WeaponInfo.Name;
        transform.Find("Type").GetComponent<Text>().text = weapon.WeaponInfo.Type;
    }

    public Weapon GetWeapon()
    {
        return _weapon;
    }


}
