using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public string id;
    public int starTypeIndex;
    public List<Planet> planets;
    //�����ϵ���ӵ���ϵ
    public List<GameObject> connectStar;
    public Star()
    {
        connectStar = new List<GameObject>();
        planets = new List<Planet>();
        //�˴��߼�ע��
        starTypeIndex = Random.Range(0, 6);
    }
}
