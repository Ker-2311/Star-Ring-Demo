using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    //�����������
    public int PlanetTypeIndex;
    //��������Ŵ�0-6
    public int RailIndex;
    //�������нǶ�
    public float Angle;
    //�ϴν���������
    public int LastEnterDays;
    //������ת�ٶ�

    public Planet()
    {
        LastEnterDays = 0;
        //ע��ô��߼�Ӧ��
        PlanetTypeIndex = Random.Range(0, 10);
    }
}
