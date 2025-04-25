using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartPanel : MonoBehaviour
{
    [Header("最大旋转"), Range(0, 1)]
    public float maxRotateAngle = 0.001f;

    private Button _startButton;
    private Button _exitButton;
    private GameObject _loadingText;
    private GameObject _loadingTextContent;
    private GameObject _camera;
    //第一次进入游戏的加载是否完成
    private bool _firstLoadingFinish = false;

    private void Start()
    {
        _camera = ResMgr.Instance.GetInstance("Prefabs/UI/Start/Camera", "JumpCamera", transform);
        _camera.SetActive(false);
        _loadingText = transform.Find("LoadingText").gameObject;
        _loadingTextContent = _loadingText.transform.Find("Scroll View/Viewport/Content").gameObject;

        _startButton = transform.Find("StartButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();

        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(Exit);

    }

    private void Update()
    {
        //第一次进入游戏的加载
        if (_firstLoadingFinish && Input.anyKey)
        {
            GameObject.Destroy(_camera);
            UIManager.Instance.RemoveLayer(UIManager.UILayer.Top);
            UIManager.Instance.AddUI("Prefabs/UI/Basic/Main/MainPanel", UIManager.UILayer.Top);
            UIManager.Instance.AddUI("Prefabs/UI/Basic/Main/UnLockPanel", UIManager.UILayer.Top).SetActive(false);
            StarSystemMgr.Instance.ExitStarSystem();
            //进入最远的星系作为初始星系
            //StarSystemMgr.Instance.EnterStarSystem(StarMgr.Instance.GetTheFarthest().name);
        }
    }

    private void StartGame()
    {
        var content = _loadingTextContent.GetComponent<LoadingTextContent>();
        GetComponent<Animator>().SetInteger("Status", 1);

        _camera.SetActive(true);
        _camera.GetComponent<Animator>().SetBool("Start", true);

        content.AddText("配置读取中");
        content.AddText("配置读取完成");
        content.AddText("系统载入中");
        StartCoroutine(GameMgr.Instance.LoadingInit());
        content.AddText("正在生成星系(" + StarMgr.Instance.generatedCount.ToString() + "/" +
            StarMgr.Instance.maxGenerateCount.ToString() + ")");
        _camera.transform.SetParent(transform);
        StartCoroutine(SceneMgr.Instance.AsynchronousLoadSceneEnumerator("Basic"));
        StartCoroutine(FirstLoadingScene(content));
    }

    /// <summary>
    /// 在第一次加载场景中,通过携程判断场景是否加载完毕
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private IEnumerator FirstLoadingScene(LoadingTextContent content)
    {
        while (!StarMgr.Instance.generateSpaceFinished)
        {
            content.ChangeText("正在生成星系(" + StarMgr.Instance.generatedCount.ToString() + "/" +
        StarMgr.Instance.maxGenerateCount.ToString() + ")");
            yield return null;
        }
        content.AddText("加载完成，按任意键进入");
        _firstLoadingFinish = true;
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
