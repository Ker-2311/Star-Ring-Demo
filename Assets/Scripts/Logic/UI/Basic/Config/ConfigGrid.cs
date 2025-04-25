using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigGrid : MonoBehaviour
{
    protected GameObject _icon;

    protected virtual void Awake()
    {
        _icon = transform.Find("Icon").gameObject;
    }

    public virtual void UnEquipWeapon()
    {
        _icon.SetActive(false);
    }
}
