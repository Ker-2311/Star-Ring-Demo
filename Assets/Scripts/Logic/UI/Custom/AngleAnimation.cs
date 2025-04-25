using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleAnimation : MonoBehaviour
{
    public float speed = 3;
    private GameObject _panel;
    private GameObject _angle;
    private Vector2 _rightdownPos;
    private Vector2 _rightupPos;
    private Vector2 _leftupPos;
    private Vector2 _leftdownPos;
    private RectTransform _rightdown;
    private RectTransform _rightup;
    private RectTransform _leftup;
    private RectTransform _leftdown;
    private bool _startShowAnimation;
    private bool _startCloseAnimation;


    private void Update()
    {
        if (_startShowAnimation)
        {
            StartShowAnimation();
        }
        if (_startCloseAnimation)
        {
            StartCloseAnimation();
        }

    }

    public void StartShow(GameObject panel)
    {
        _panel = panel;
        _startShowAnimation = true;

        Init();
    }

    public void StartClose(GameObject panel)
    {
        _panel = panel;
        _startCloseAnimation = true;

        Init();
    }

    private void Init()
    {
        _angle = ResMgr.Instance.GetInstance("Prefabs/UI/Angle", "Angle", _panel.transform.parent);
        _rightdown = _angle.transform.Find("Right-Down").GetComponent<RectTransform>();
        _rightup = _angle.transform.Find("Right-Up").GetComponent<RectTransform>();
        _leftup = _angle.transform.Find("Left-Up").GetComponent<RectTransform>();
        _leftdown = _angle.transform.Find("Left-Down").GetComponent<RectTransform>();

        var rect = _panel.GetComponent<RectTransform>();
        _leftupPos = new Vector2(rect.rect.xMin, rect.rect.yMax);
        _leftdownPos = new Vector2(rect.rect.xMin, rect.rect.yMin);
        _rightdownPos = new Vector2(rect.rect.xMax, rect.rect.yMin);
        _rightupPos = new Vector2(rect.rect.xMax, rect.rect.yMax);
    }

    private void StartShowAnimation()
    {
        if (Mathf.Abs(_rightdown.anchoredPosition.x)> Mathf.Abs(_rightdownPos.x))
        {
            Destroy(_angle);
            _startShowAnimation = false;
            _panel.SetActive(true);
        }
        _rightdown.anchoredPosition += Vector2.Lerp(Vector2.zero, _rightdownPos, 0.1f * speed);
        _rightup.anchoredPosition += Vector2.Lerp(Vector2.zero, _rightupPos, 0.1f * speed);
        _leftup.anchoredPosition += Vector2.Lerp(Vector2.zero, _leftupPos, 0.1f * speed);
        _leftdown.anchoredPosition += Vector2.Lerp(Vector2.zero, _leftdownPos, 0.1f * speed);
    }

    private void StartCloseAnimation()
    {
        _panel.SetActive(false);
        if (Mathf.Abs(_rightdown.anchoredPosition.x) == 0)
        {
            Destroy(_angle);
            _startCloseAnimation = false;
        }
        _rightdown.anchoredPosition += Vector2.Lerp(_rightdownPos, Vector2.zero, 0.1f * speed);
        _rightup.anchoredPosition += Vector2.Lerp(_rightupPos, Vector2.zero, 0.1f * speed);
        _leftup.anchoredPosition += Vector2.Lerp(_leftupPos, Vector2.zero, 0.1f * speed);
        _leftdown.anchoredPosition += Vector2.Lerp(_leftdownPos, Vector2.zero, 0.1f * speed);
    }
}
