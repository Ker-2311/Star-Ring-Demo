using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tech
{
    public TechInfo techInfo;
    public bool isUnLock;
    public int curPoint;

    public Tech(string id)
    {
        techInfo = TechTable.Instance[id];
        isUnLock = false;
        curPoint = 0;
    }
}

