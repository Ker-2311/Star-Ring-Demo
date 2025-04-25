using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;
using UnityEditor;

//[ExecuteInEditMode]
public class ResearchPanel : BasePanel
{
    private GameObject _buttonPrefab;
    [HideInInspector]
    public GameObject TargetButton;
    //Content对象
    private GameObject _techContent;
    private Transform _materialContent;
    private Transform _energyContent;
    private Transform _infomaticsContent;
    private GameObject _toggleGroup;
    private GameObject _unactivateLinePrefab;
    private GameObject _techButtonPrefab;
    [Header("激活的ScienceButton图标")]
    public Sprite activateScienceButton;
    [Header("激活的ScienceButton图标")]
    public Sprite activateTechButton;
    private List<ScienceButton> _createdButtons = new List<ScienceButton>();
    private Sprite[] _icons;
    [Header("技术按钮的半径")]
    public float TechButtonRadius = 100;

    [System.Obsolete]
    private void Update()
    {
        //if (Input.GetKey(KeyCode.S))
        //{
        //    Debug.Log("正在保存预制体");
        //    PrefabUtility.ReplacePrefab(gameObject, ResMgr.instance.GetResource<GameObject>("Prefabs/UI/Basic/Research/ResearchPanel"));
        //    Debug.Log("保存成功");
        //}
        //if (Input.GetKey(KeyCode.R))
        //{
        //    UpdateAllButton();
        //}
        //if (Input.GetKey(KeyCode.T))
        //{ 

        //}
    }
    public override void Awake()
    {
        base.OnEnter();
        _buttonPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Research/ReserachButton");
        _techContent = transform.Find("Scroll View/TechContent").gameObject;
        _materialContent = _techContent.transform.Find("Material");
        _energyContent = _techContent.transform.Find("Energy");
        _infomaticsContent = _techContent.transform.Find("Infomatics");
        _toggleGroup = transform.Find("ToggleGroup").gameObject;
        _unactivateLinePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Research/UnactivateLine");
        _techButtonPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Research/TechButton");
        _icons = Resources.LoadAll<Sprite>("Image/Icon/Research");

        var materialToggle = _toggleGroup.transform.Find("MaterialToggle").GetComponent<Toggle>();
        var energyToggle = _toggleGroup.transform.Find("EnergyToggle").GetComponent<Toggle>();
        var infomaticsToggle = _toggleGroup.transform.Find("InfomaticsToggle").GetComponent<Toggle>();

        materialToggle.onValueChanged.AddListener((bool IsOn) => ChangeContent(IsOn, _materialContent));
        energyToggle.onValueChanged.AddListener((bool IsOn) => ChangeContent(IsOn, _energyContent));
        infomaticsToggle.onValueChanged.AddListener((bool IsOn) => ChangeContent(IsOn, _infomaticsContent));

        foreach (var content in _techContent.GetAllChilds())
        {
            foreach (var button in content.GetAllChilds())
            {
                var scienceButtonComponent = button.GetComponent<ScienceButton>();
                if (scienceButtonComponent != null)
                {
                    _createdButtons.Add(scienceButtonComponent);
                }
            }
        }

        InitAllButton();
    }


    /// <summary>
    /// 更新所有按钮对象(用于按钮预制体修改后的更新)
    /// </summary>
    private void UpdateAllButton()
    {
        _createdButtons.Clear();
        foreach (var content in _techContent.GetAllChilds())
        {
            foreach (var button in content.GetAllChilds())
            {
                var scienceButtonComponent = button.GetComponent<ScienceButton>();
                if (scienceButtonComponent != null)
                {
                    UpdateButton(scienceButtonComponent.name, scienceButtonComponent.transform);
                }
            }
        }
        foreach (var content in _techContent.GetAllChilds())
        {
            foreach (var button in content.GetAllChilds())
            {
                var scienceButtonComponent = button.GetComponent<ScienceButton>();
                if (scienceButtonComponent != null)
                {
                    _createdButtons.Add(scienceButtonComponent);
                }
            }
        }
    }
    /// <summary>
    /// 初始化所有按钮
    /// </summary>
    private void InitAllButton()
    {
        foreach (var scienceButtonComponent in _createdButtons)
        {
            var techButtons = scienceButtonComponent.transform.Find("Tech").gameObject.GetAllChilds();
            var scienceData = ScienceAndTechMgr.Instance.GetScienceData()[scienceButtonComponent.name];
            scienceButtonComponent.Init(scienceData, gameObject,_unactivateLinePrefab, _icons);
            foreach (var techButton in techButtons)
            {
                techButton.GetComponent<TechButton>().Init(scienceData.techs.Find(x => x.techInfo.ID == techButton.name), gameObject);
            }
        }
    }

    private void UpdateAllButtonInfo()
    {
        //遍历按钮并显示
        foreach (var button in _createdButtons)
        {
            InitScienceButton(button.gameObject);
            //检查按钮前置科技是否解锁
            if (!ScienceAndTechMgr.Instance.IsUnlock(button.science.ScienceInfo.FrontID))
            {
                button.line.SetActive(false);
                button.gameObject.SetActive(false);
            }
            else
            {
                //检查按钮科技是否解锁
                if (ScienceAndTechMgr.Instance.IsUnlock(button.science.ScienceInfo.ID))
                {
                    button.GetComponent<Image>().sprite = activateScienceButton;
                }
                button.line.SetActive(true);
                button.gameObject.SetActive(true);
            }
        }
    }
    private void ChangeContent(bool isOn, Transform content)
    {
        if (isOn)
        {
            content.gameObject.SetActive(true);
        }
        else
        {
            content.gameObject.SetActive(false);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        MouseEventBlinding.Instance.ChangeStatus(MouseEventBlinding.MouseEventStatus.ResearchPanel);
        MouseEventBlinding.Instance.BlindMouseEvent(MouseEventBlinding.MouseEventStatus.ResearchPanel, 1, DisTargetCurButton);
        UpdateAllButtonInfo();
        _toggleGroup.transform.Find("MaterialToggle").GetComponent<Toggle>().isOn = true;
    }

    public override void OnExit()
    {
        base.OnExit();
        MouseEventBlinding.Instance.ChangeStatus(MouseEventBlinding.MouseEventStatus.MainPanel);
    }

    /// <summary>
    /// 更新按钮
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedButton"></param>
    private void UpdateButton(string id, Transform updatedButton)
    {
        var scienceButtonObject = ResMgr.Instance.GetInstance(_buttonPrefab, updatedButton.parent);
        var scienceButton = scienceButtonObject.GetComponent<ScienceButton>();
        var science = ScienceAndTechMgr.Instance.GetScienceData()[id];
        var icon = scienceButtonObject.transform.Find("Icon").GetComponent<Image>();
        //生成角度
        var angle = 45;
        List<GameObject> techButtons = new List<GameObject>();

        foreach (var tech in science.techs)
        {
            var techButton = ResMgr.Instance.GetInstance(_techButtonPrefab, scienceButtonObject.transform.Find("Tech").transform);
            techButton.GetComponent<RectTransform>().anchoredPosition = TechButtonRadius * new Vector2(Util.RadiansCos(angle),
                Util.RadiansSin(angle));
            switch (science.techs.Count)
            {
                case 1: break;
                case 2: angle += 180;break;
                case 3: angle += 120;break;
                case 4: angle += 90; break;
                case 5: angle += 72; break;
                case 6: angle += 60; break;
            }
            techButton.GetComponent<TechButton>().Init(tech, gameObject);
            techButton.name = tech.techInfo.ID;
            techButtons.Add(techButton);
            techButton.SetActive(false);
        }

        scienceButtonObject.GetComponent<Toggle>().group = _techContent.GetComponent<ToggleGroup>();
        foreach (var sprite in _icons)
        {
            if (sprite.name == science.ScienceInfo.Name)
            {
                icon.sprite = sprite;
                break;
            }
        }
        scienceButtonObject.name = id;
        scienceButtonObject.transform.position = updatedButton.position;
        InitScienceButton(scienceButton.gameObject);
        GameObject.DestroyImmediate(updatedButton.gameObject);
    }

    private void DisTargetCurButton()
    {
        TargetButton.GetComponent<Toggle>().isOn = false;
        //TargetButton.GetComponent<ResearchButton>().DisTarget();
    }

    private void InitScienceButton(GameObject button)
    {
        var scienceButton = button.GetComponent<ScienceButton>();
        scienceButton.Init(ScienceAndTechMgr.Instance.GetScienceData()[button.name], gameObject, _unactivateLinePrefab, _icons);
    }
}

