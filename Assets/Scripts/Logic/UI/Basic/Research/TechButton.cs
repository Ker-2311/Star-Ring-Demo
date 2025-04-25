using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tech _tech;
    private TechInfo _techInfo;
    private GameObject _suspensionPanel;
    private GameObject _researchPanel;
    public void Init(Tech tech, GameObject researchPanel)
    {
        _tech = tech;
        _techInfo = tech.techInfo;
        _researchPanel = researchPanel;
        _suspensionPanel = _researchPanel.transform.Find("SuspensionPanel").gameObject;
    }

    /// <summary>
    /// 鼠标进入时执行
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        var text_Name = _suspensionPanel.transform.Find("Name").GetComponent<Text>();
        //var text_Type = _suspensionPanel.transform.Find("Type").GetComponent<Text>();
        var text_Descript = _suspensionPanel.transform.Find("Descript").GetComponent<Text>();
        var text_CoinCost = _suspensionPanel.transform.Find("CoinCost").GetComponent<Text>();
        var text_PointCost = _suspensionPanel.transform.Find("PointCost").GetComponent<Text>();
        var text_CurPoint = _suspensionPanel.transform.Find("CurPoint").GetComponent<Text>();
        var text_PointIncrease = _suspensionPanel.transform.Find("PointIncrease").GetComponent<Text>();
        var slider_Progress = _suspensionPanel.transform.Find("ProgressSlider").GetComponent<Slider>();
        var icon = _suspensionPanel.transform.Find("Icon").GetComponent<Image>();

        text_Name.text = _techInfo.Name;
        //text_Type.text = _techInfo.Type;
        text_Descript.text = _techInfo.Descript;
        text_CoinCost.text = _techInfo.CoinCost.ToString();
        text_PointCost.text = _techInfo.PointCost.ToString();
        text_CurPoint.text = _tech.curPoint.ToString();
        if (_techInfo.CoinCost != 0)
        {
            slider_Progress.value = _tech.curPoint / _techInfo.CoinCost;
        }
        else
        {
            slider_Progress.value = 0;
        }

        _suspensionPanel.SetActive(true);
    }

    /// <summary>
    /// 鼠标退出时执行
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _suspensionPanel.SetActive(false);
    }
}
