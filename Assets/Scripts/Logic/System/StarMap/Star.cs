using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public string id;
    public int starTypeIndex;
    public List<Planet> planets;
    //与该星系连接的星系
    public List<GameObject> connectStar;
    public Star()
    {
        connectStar = new List<GameObject>();
        planets = new List<Planet>();
        //此处逻辑注意
        starTypeIndex = Random.Range(0, 6);
    }
}
