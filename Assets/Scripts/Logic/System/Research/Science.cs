using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science
{
    //�Ƽ���Ϣ
    public ScienceInfo ScienceInfo;
    //�Ƿ����
    public bool isUnLock;
    //�����б�
    public List<Tech> techs;
    //��ǰ����
    public int curPoint;
    /// <summary>
    /// ����ID����Tech
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
