using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceToggle : MonoBehaviour
{
    public Force force;
    public void Init(Force curforce)
    {
        force = curforce;

        var text_name = transform.Find("Name").GetComponent<Text>();

        text_name.text = force.forceInfo.Name;
    }
}
