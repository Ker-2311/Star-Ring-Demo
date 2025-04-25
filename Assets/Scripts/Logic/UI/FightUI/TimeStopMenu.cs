using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeStopMenu : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        var quickUseButton = transform.Find("ScrollArea/MainMenu/ButtonGroup/QuickUseButton").GetComponent<Button>();
        var quickUseExitButton = transform.Find("ScrollArea/QuickUseMenu/MenuButton").GetComponent<Button>();
        var shipStatusButton = transform.Find("ScrollArea/MainMenu/ButtonGroup/ShipStatusButton").GetComponent<Button>();
        var mapButton = transform.Find("ScrollArea/MainMenu/ButtonGroup/MapButton").GetComponent<Button>();

        quickUseButton.onClick.AddListener(() => _animator.SetInteger("Action", 1));
        quickUseExitButton.onClick.AddListener(() => _animator.SetInteger("Action", 2));
        //shipStatusButton.onClick.AddListener(() => _animator.SetInteger("Action", 3));
    }
}
