using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ʒ����
/// </summary>
public class Item
{
    //��Ʒ��Ϣ
    public MaterialInfo itemInfo;
    //��Ʒ����
    public int count;

    /// <summary>
    /// ����ItemType����Item
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
    /// ����ID����Item
    /// </summary>
    /// <param name="id"></param>
    public Item(string id)
    {
        itemInfo = MaterialTable.Instance[id];
        count = 0;
    }
}
