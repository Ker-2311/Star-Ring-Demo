using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science
{
    //科技信息
    public ScienceInfo ScienceInfo;
    //是否解锁
    public bool isUnLock;
    //技术列表
    public List<Tech> techs;
    //当前点数
    public int curPoint;
    /// <summary>
    /// 根据ID生成Tech
    /// </summary>
    /// <param name="id"></param>
    public Science(string id)
    {
        ScienceInfo = ScienceTable.Instance.GetDictionary()[id];
        isUnLock = false;
        curPoint = 0;
        techs = new List<Tech>();
        foreach (var tech in TechTable.Instance.GetDictionary().Values)
        {
            if (tech.FrontID == id)
            {
                techs.Add(new Tech(tech.ID));
            }
        }
    }
}
