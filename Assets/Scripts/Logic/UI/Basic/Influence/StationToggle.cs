using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationToggle : MonoBehaviour
{
    public Station station;
    public void Init(Station curStation)
    {
        station = curStation;
    }
}
