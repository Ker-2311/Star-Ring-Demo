using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarInfoPanel : BasePanel
{
    private Star _star;
    private GameObject _buildToggle;
    private GameObject _claimToggle;
    private GameObject _jumpToggle;
    private GameObject _claimInfoPanel;

    public override void Awake()
    {
        base.OnEnter();
        _claimToggle = transform.Find("ToggleGroup/ClaimToggle").gameObject;
        _jumpToggle = transform.Find("ToggleGroup/JumpToggle").gameObject;
        _claimInfoPanel = transform.Find("ClaimInfoPanel").gameObject;

        _claimToggle.GetComponent<Toggle>().onValueChanged.AddListener(ClaimToggleOnValueChange);
        _jumpToggle.GetComponent<Toggle>().onValueChanged.AddListener(JumpToggleOnValueChange);
    }

    public void Init(Star star)
    {
        _star = star;
    }

    private void JumpToggleOnValueChange(bool isOn)
    {
        if (isOn)
        {
            StarSystemMgr.Instance.EnterStarSystem(_star.id);
            PanelMgr.Instance.PopAllPanel();
        }
    }

    private void ClaimToggleOnValueChange(bool isOn)
    {
        if (isOn)
        {
            _claimInfoPanel.SetActive(true);
        }
        else
        {
            _claimInfoPanel.SetActive(false);
        }
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        PanelMgr.instance.PopAllPanel();
        //        return;
        //    }
        //    StarSystemMgr.instance.EnterStarSystem(_star.ID);
        //    PanelMgr.instance.PopAllPanel();
        //}
    }
}
