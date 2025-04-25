using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Exterior;
using System.Linq;

/// <summary>
/// 包括科技按钮按下事件和该按钮包含的信息
/// </summary>
public class ScienceButton : MonoBehaviour
{
    public Science science;
    private ScienceInfo _scienceInfo;
    private GameObject _suspensionPanel;
    private GameObject _researchPanel;
    private GameObject _frontTech;
    private GameObject _unActivateLine;
    private GameObject _activateLine;
    private GameObject _bg;
    public GameObject line;
    private GameObject _slider;
    private Sprite[] _icons;
    private Sprite _targetSprite;
    private List<GameObject> _techButtons;
    private bool _isPress;
    private bool _isTarget;
    private float _pressTime = 0;
    [Header("长按速度")]
    public float PressSpeed = 1;

    private void Update()
    {
        if (_scienceInfo.FrontID != null)
        {
            _frontTech = transform.parent.gameObject.FindChildObject(_scienceInfo.FrontID);
            line.GetComponent<LinePositionControll>().Init(gameObject, _frontTech);
        }
        //长按滑动条
        if (_isPress && !_isTarget)
        {
            if (_pressTime >= 1)
            {
                _pressTime = 0;
                GetComponent<Toggle>().isOn = true;
            }
            else
            {
                _pressTime += Time.deltaTime * PressSpeed;
            }
            _slider.GetComponent<Slider>().value = _pressTime;
        }
        else if (!_isPress)
        {
            _pressTime = 0;
            _slider.GetComponent<Slider>().value = _pressTime;
        }
    }


    public void Init(Science science,GameObject researchPanel,GameObject unactivateLinePrefab, Sprite[] icons)
    {
        _icons = icons;
        _techButtons = transform.Find("Tech").gameObject.GetAllChilds();
        this.science = science;
        _scienceInfo = science.ScienceInfo;
        _researchPanel = researchPanel;
        _suspensionPanel = _researchPanel.transform.Find("SuspensionPanel").gameObject;
        _bg = transform.Find("bg").gameObject;
        _unActivateLine = unactivateLinePrefab;
        _slider = transform.Find("Slider").gameObject;

        //判断是否已有线
        var line = transform.parent.Find("Lines").Find(_scienceInfo.ID + "-" + _scienceInfo.FrontID);
        if (line != null)
        {
            this.line = line.gameObject;
        }
        else
        {
            //生成线
            this.line = ResMgr.Instance.GetInstance(unactivateLinePrefab, transform.parent.Find("Lines"));

            this.line.name = _scienceInfo.ID + "-" + _scienceInfo.FrontID;
        }

        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChange);

        var eventTrigger = EventTriggerListener.Get(transform.Find("TriggerArea").gameObject);
        eventTrigger.onDown = OnPointerDown;
        eventTrigger.onUp = OnPointerUp;
        eventTrigger.onEnter = OnPointerEnter;
        eventTrigger.onExit = OnPointerExit;
    }

    public void OnPointerEnter(GameObject obj)
    {
        var text_Name = _suspensionPanel.transform.Find("Name").GetComponent<Text>();
        var text_Type = _suspensionPanel.transform.Find("Type").GetComponent<Text>();
        var text_Descript = _suspensionPanel.transform.Find("Descript").GetComponent<Text>();
        var text_CoinCost = _suspensionPanel.transform.Find("CoinCost").GetComponent<Text>();
        var text_PointCost = _suspensionPanel.transform.Find("PointCost").GetComponent<Text>();
        var text_CurPoint = _suspensionPanel.transform.Find("CurPoint").GetComponent<Text>();
        var text_PointIncrease = _suspensionPanel.transform.Find("PointIncrease").GetComponent<Text>();
        var slider_Progress = _suspensionPanel.transform.Find("ProgressSlider").GetComponent<Slider>();
        var icon = _suspensionPanel.transform.Find("Icon").GetComponent<Image>();

        text_Name.text = _scienceInfo.Name;
        text_Type.text = _scienceInfo.Type;
        text_Descript.text = _scienceInfo.Descript;
        text_CoinCost.text = _scienceInfo.CoinCost.ToString();
        text_PointCost.text = _scienceInfo.PointCost.ToString();
        text_CurPoint.text = science.curPoint.ToString();
        if (_scienceInfo.CoinCost != 0)
        {
            slider_Progress.value = science.curPoint / _scienceInfo.CoinCost;
        }
        else
        {
            slider_Progress.value = 0;
        }
        //Icon图标查找
        foreach (var sprite in _icons)
        {
            if (sprite.name == _scienceInfo.Name)
            {
                icon.sprite = sprite;
                break;
            }
        }
        _suspensionPanel.SetActive(true);
    }

    public void OnPointerExit(GameObject obj)
    {
        _suspensionPanel.SetActive(false);
    }

    public void OnPointerDown(GameObject obj)
    {
        _isPress = true;
    }

    public void OnPointerUp(GameObject obj)
    {
        _isPress = false;
    }

    /// <summary>
    /// 按钮长按锁定
    /// </summary>
    public void OnTarget()
    {
        Debug.Log(_scienceInfo.ID);
        _isTarget = true;
        _researchPanel.GetComponent<ResearchPanel>().TargetButton = gameObject;
        _bg.SetActive(true);
        foreach (var techButton in _techButtons)
        {
            techButton.SetActive(true);
        }
        //if (_science.isUnLock)
        //{
        //    _bg.SetActive(true);
        //}
        //else
        //{

        //}

    }

    /// <summary>
    /// 按钮解除锁定
    /// </summary>
    /// <param name="eventData"></param>
    public void DisTarget()
    {
        _isTarget = false;
        _researchPanel.GetComponent<ResearchPanel>().TargetButton = null;
        _bg.SetActive(false);
        foreach (var techButton in _techButtons)
        {
            techButton.SetActive(false);
        }
    }
    
    private void OnValueChange(bool IsOn)
    {
        if (IsOn)
        {
            OnTarget();
        }
        else
        {
            DisTarget();
        }
        //ScienceMgr.instance.UnlockTech(_scienceInfo.ID);
    }

}
