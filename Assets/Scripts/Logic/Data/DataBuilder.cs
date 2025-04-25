using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataBuilder<T> 
    where T:Data
{
    public abstract T GetData();
}
