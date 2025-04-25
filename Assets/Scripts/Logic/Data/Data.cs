using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存内的物品
/// </summary>
public class Data:Singleton<Data>
{
    //势力数据
    public Dictionary<string, Force> ForceData { get; set; } = new Dictionary<string, Force>();

    /// <summary>
    /// 保存所有游戏事件，Key为事件类型，Value为ID，GameEvent字典
    /// </summary>
    public Dictionary<string, Dictionary<string, GameEvent>> GameEventData { get; set; } = new Dictionary<string, Dictionary<string, GameEvent>>();
    //正在进行中的事件
    public List<GameEvent> TriggerEventData { get; set; } = new List<GameEvent>();

} 
