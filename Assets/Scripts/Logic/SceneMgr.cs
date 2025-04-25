using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    public void LoadScene(string name)
    {
        ClearSceneUI();
        SceneManager.LoadScene(name);
    }

    public void ClearSceneUI()
    {
        PanelMgr.Instance.RemoveAllPanel();
        UIManager.Instance.RemoveAllLayer();
    }

    /// <summary>
    /// 异步加载场景的迭代器,需要协程
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator AsynchronousLoadSceneEnumerator(string sceneName)
    {
        if (sceneName != null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {

                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
