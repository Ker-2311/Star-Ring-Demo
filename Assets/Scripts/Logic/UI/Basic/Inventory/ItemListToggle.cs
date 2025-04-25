using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemListToggle : ItemToggle
{
    public override void Init(Item item, GameObject description, Sprite[] icons)
    {
        base.Init(item, description, icons);
        _text = transform.Find("Text").gameObject;
        var name = _text.FindComponent<Text>("Name");
        var descript = _text.FindComponent<Text>("Description");

        gameObject.name = item.itemInfo.Name;
        name.text = item.itemInfo.Name;
        descript.text = item.itemInfo.Description;
    }
}
