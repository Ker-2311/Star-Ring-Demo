using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IrregularImage : MonoBehaviour
{
    public float alphaThreshold = 0.1f;

    void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
