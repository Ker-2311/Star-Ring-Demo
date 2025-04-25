using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cmd : MonoBehaviour
{
    private GameEvent _gameEvent;
    private GameObject _bgMask;
    private GameObject _eventTime;
    private GameObject _completeDescription;
    public void Init(GameEvent gameEvent)
    {
        _bgMask = transform.Find("bgMask").gameObject;
        _eventTime = transform.Find("EventTime").gameObject;
        _completeDescription = transform.Find("CompleteDescription").gameObject;
        _gameEvent = gameEvent;
        //var sprites = Resources.LoadAll<>
        //switch (gameEvent.eventInfo.EventType)
        //{
        //    case "¾çÇéÊÂ¼þ":transform.Find("Icon").GetComponent<Image>().sprite = 
        //}
        transform.Find("Name").GetComponent<Text>().text = gameEvent.eventInfo.Name;
        transform.Find("Icon").GetComponent<Image>().sprite =
            ResMgr.Instance.GetResource<Sprite>("Image/Icon/Cmd/" + gameEvent.eventInfo.EventType);
    }

    private void Update()
    {
        if (_gameEvent.Time == 0)
        {
            _eventTime.SetActive(false);
            _bgMask.SetActive(true);
            _completeDescription.SetActive(true);
        }
    }
}
