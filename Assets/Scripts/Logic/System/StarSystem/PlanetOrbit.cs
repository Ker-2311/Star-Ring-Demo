using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlanetOrbit : MonoBehaviour
{
    private List<Vector3> _points = new List<Vector3>();
    private LineRenderer _line;
    public float Radius;
    private int _pointsNumber = 100;

    void Awake()
    {
        _line = gameObject.GetComponent<LineRenderer>();
    }

    public void GenerateOrbit(Transform Center)
    {
        transform.position = Center.position;
        _points.Clear();
        for (int i = 0; i <= _pointsNumber; i++)
        {
            var angle = i * 360f / _pointsNumber;
            var x = Radius * Util.RadiansCos(angle);
            var y = Radius * Util.RadiansSin(angle);
            Vector3 pt = new Vector3(x, y, 0);
            _points.Add(pt);
        }
        _line.positionCount = _points.Count;
        _line.SetPositions(_points.ToArray());
    }



}
