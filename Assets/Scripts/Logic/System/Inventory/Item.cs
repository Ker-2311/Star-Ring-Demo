using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 物品类型
/// </summary>
public class Item
{
    //物品信息
    public MaterialInfo itemInfo;
    //物品数量
    public int count;

    /// <summary>
    /// 根据ItemType生存Item
    /// </summary>
    /// <param name="iteminfo"></param>
    public Item(BaseInfo iteminfo)
    {
        if (iteminfo.GetType() == typeof(MaterialInfo))
        {
            itemInfo = iteminfo as MaterialInfo;
        }
        count = 0;
    }

    /// <summary>
    /// 根据ID生成Item
    /// </summary>
    /// <param name="id"></param>
    public Item(string id)
    {
        itemInfo = MaterialTable.Instance[id];
        count = 0;
    }
}
