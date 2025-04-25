using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGridToggle : ItemToggle
{
    public override void Init(Item item, GameObject description, Sprite[] icons)
    {
        base.Init(item, description, icons);
        var name = _text.FindComponent<Text>("Name");
        var icon = gameObject.FindComponent<Image>("Icon");

        gameObject.name = item.itemInfo.Name;
        name.text = item.itemInfo.Name;
        foreach (var sprite in icons)
        {
            if (sprite.name == item.itemInfo.Name)
            {
                icon.sprite = sprite;
                break;
            }
        }
    }
}
