using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Exterior;

public class SourcesPanel : MonoBehaviour
{
    private GameObject _normalSources;
    private GameObject _specialSourcesPanel;
    private GameObject _sourceInfoPrefab;
    private Toggle _sourcesButton;
    private Sprite[] _sprites;
    private Animation _animation;
    private void Awake()
    {
        _normalSources = transform.Find("NormalSources").gameObject;
        _specialSourcesPanel = transform.Find("SpecialSourcesPanel").gameObject;
        _sourcesButton = transform.Find("SourcesButton").GetComponent<Toggle>();
        _sourceInfoPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/Building/SourceInfo");
        _sprites = Resources.LoadAll<Sprite>("Image/Icon/Sources");
        _animation = GetComponent<Animation>();

        _sourcesButton.onValueChanged.AddListener(OnValueChanged);
        UpdateSourcesContent();
    }

    //private void Update()
    //{
    //    UpdateSourcesContent();
    //}

    private void UpdateSourcesContent()
    {
        var sourcesDic = SourcesMgr.Instance.GetSourcesDic();
        
        _specialSourcesPanel.DestroyChilds();

        foreach (var source in _normalSources.GetAllChilds())
        {
            source.transform.Find("Count").GetComponent<Text>().text = sourcesDic[source.name].count.ToString();
            sourcesDic.Remove(source.name);
        }
        foreach (var source in sourcesDic)
        {
            var sourceInfo = ResMgr.Instance.GetInstance(_sourceInfoPrefab, _specialSourcesPanel.transform);
            sourceInfo.name = source.Key;
            foreach (var sprite in _sprites)
            {
                if (sprite.name == source.Value.sourceInfo.Name)
                {
                    sourceInfo.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
                    break;
                }
            }
            sourceInfo.transform.Find("Count").GetComponent<Text>().text = source.Value.count.ToString();
        }
    }

    public void OnValueChanged(bool IsOn)
    {
        if (IsOn)
        {
            _animation.Play("SourcesPanelShow");
        }
        else
        {
            _animation.Play("SourcesPanelClose");
        }
    }
}
