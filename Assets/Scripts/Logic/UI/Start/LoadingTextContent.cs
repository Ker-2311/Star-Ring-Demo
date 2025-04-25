using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextContent : MonoBehaviour
{
    private GameObject _curText;
    private GameObject _textPrefab;

    public GameObject AddText(string text)
    {
        _textPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Start/Text");
        var nextText = ResMgr.Instance.GetInstance(_textPrefab, transform);
        var contentRect = GetComponent<RectTransform>();
        nextText.GetComponent<Text>().text = text;
        contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, 0);
        _curText = nextText;
        return nextText;
    }

    public void ChangeText(string text)
    {
        _curText.GetComponent<Text>().text = text;
    }
}
