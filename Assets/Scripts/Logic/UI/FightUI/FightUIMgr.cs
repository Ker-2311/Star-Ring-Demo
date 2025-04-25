using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUIMgr : Singleton<FightUIMgr>
{
    public void AddFightUI()
    {
        var fightPanel = UIManager.Instance.AddUI("Prefabs/UI/FightUI/FightPanel", UIManager.UILayer.FightUI);
    }
    
    public void RemoveFightUI()
    {
        UIManager.Instance.RemoveLayer(UIManager.UILayer.FightUI);
    }

}
