using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;
using System;

public class EventPanel : BasePanel
{
    private GameEvent _curGameEvent;
    public GameEvent CurEvent
    { 
        get { return _curGameEvent; }
        set 
        {
            _curGameEvent = value;
            ShowEventInfo(value);
        } 
    }
    private GameObject _options;
    private GameObject _framework;
    private GameObject _circle;

    public override void Awake()
    {
        base.OnEnter();
        _options = transform.Find("Options").gameObject;
        _framework = transform.Find("Framework").gameObject;
        _circle = transform.Find("Framework/Circle").gameObject;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        GameEventMgr.Instance.panelIsOpen = true;

        _options.GetAllChilds().ForEach(x => x.SetActive(false));
    }

    public override void OnExit()
    {
        base.OnExit();
        GameEventMgr.Instance.panelIsOpen = false;
    }

    private void Update()
    {
        var activateToggle = _options.GetComponent<ToggleGroup>();
        var angle = _framework.transform.Find(activateToggle.GetFirstActiveToggle().name);
        var circleRect = _circle.GetComponent<RectTransform>();
        Vector2 mousPos;
        angle.GetComponent<Toggle>().isOn = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition,
            UIManager.Instance.GetCamera(), out mousPos);
        circleRect.localEulerAngles = new Vector3(0,0,Vector2.SignedAngle(Vector2.right, mousPos- circleRect.anchoredPosition));
    }

    private void ShowEventInfo(GameEvent gameEvent)
    {
        var text_name = transform.Find("Name").GetComponent<Text>();
        var text_type = transform.Find("Type").GetComponent<Text>();
        var text_Description = transform.Find("Description").GetComponent<Text>();
        var options = _options.GetAllChilds();

        text_name.text = gameEvent.eventInfo.Name;
        text_type.text = gameEvent.eventInfo.EventType;
        text_Description.text = gameEvent.eventInfo.Description;

        for (int i =0;i<gameEvent.optionCount;i++)
        {
            options[i].SetActive(true);
            options[i].GetComponent<Option>().optionOnClick = () =>
                 {
                     gameEvent.gameEventEffects[i].EffectStart(gameEvent.gameEventEffectParameter[i]);
                 };
            switch (i)
            {
                case 0:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option1Description;break;
                case 1:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option2Description; break;
                case 2:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option3Description; break;
                case 3:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option4Description; break;
                case 4:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option5Description; break;
                case 5:
                    options[i].transform.Find("Text").GetComponent<Text>().text = gameEvent.eventInfo.Option6Description; break;
            }

        }
    }
}
