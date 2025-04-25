using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ڵ���Ʒ
/// </summary>
public class Data:Singleton<Data>
{
    //��������
    public Dictionary<string, Force> ForceData { get; set; } = new Dictionary<string, Force>();

    /// <summary>
    /// ����������Ϸ�¼���KeyΪ�¼����ͣ�ValueΪID��GameEvent�ֵ�
    /// </summary>
    public Dictionary<string, Dictionary<string, GameEvent>> GameEventData { get; set; } = new Dictionary<string, Dictionary<string, GameEvent>>();
    //���ڽ����е��¼�
    public List<GameEvent> TriggerEventData { get; set; } = new List<GameEvent>();

} 
