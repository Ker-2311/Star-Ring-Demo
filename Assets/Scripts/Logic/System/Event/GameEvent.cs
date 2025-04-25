using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public EventInfo eventInfo;
    /// <summary>
    /// �¼�ѡ��Ч��
    /// </summary>
    public List<IGameEventEffect> gameEventEffects;
    /// <summary>
    /// �¼�Ч������
    /// </summary>
    public List<string> gameEventEffectParameter;
    public int optionCount = 0;//ѡ����

    /// <summary>
    /// �¼�����ʱ��
    /// </summary>
    private int time = 0;
    public int Time 
    {
        get 
        {
            if (time < 0)
            {
                time = 0;
            }
            return time; 
        }
        set
        {
            time = value;
        }
    }

    public GameEvent(EventInfo info)
    {
        eventInfo = info;
        gameEventEffects = new List<IGameEventEffect>();
        gameEventEffectParameter = new List<string>();
        for (int i = 0;i<6; i++)
        {
            var option = info.GetType().GetField("Option" + (i + 1).ToString()).GetValue(info);
            if (option != null)
            {
                var optionSplit = option.ToString().Split(' ');
                if (optionSplit.Length == 1)
                {
                    gameEventEffectParameter.Add(null);
                }
                else if (optionSplit.Length == 2)
                {
                    gameEventEffectParameter.Add(optionSplit[1]);
                }
                gameEventEffects.Add(GameEventEffectFactory.CreateGameEventEffect(optionSplit[0]));
                optionCount++;
            }
            else
            {
                break;
            }
        }
    }

}
