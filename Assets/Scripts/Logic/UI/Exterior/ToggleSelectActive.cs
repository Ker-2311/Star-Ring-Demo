using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Toggleѡ��ʱ����GameObject��ѡ��ʱȡ��
/// </summary>
public class ToggleSelectActive : MonoBehaviour
{
    public GameObject[] gameObjects;
    private void Awake()
    {
        var button = transform.GetComponent<Toggle>();

        button.onValueChanged.AddListener(OnButtonClick);
    }

    private void OnButtonClick(bool isOn)
    {
        if (isOn)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
