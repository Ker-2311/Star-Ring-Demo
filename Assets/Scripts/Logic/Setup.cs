using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    private void Awake()
    {
        GameMgr.Instance.SetupInit();
        SceneMgr.Instance.LoadScene("Start");
        UIManager.Instance.AddUI("Prefabs/UI/Start/StartPanel",UIManager.UILayer.Top);
    }
}
