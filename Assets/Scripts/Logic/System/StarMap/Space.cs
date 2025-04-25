using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Space : MonoBehaviour
{
    private Camera _camera;
    private GameObject _starInfoPanelPrefab;


    /// <summary>
    /// Basic������ʼ��,�ڼ��س���ʱ����
    /// </summary>
    public void Awake()
    {
        var viewPoint = transform.Find("ViewPoint");
        _camera = transform.Find("StarMapCamera").GetComponent<Camera>();
        _starInfoPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Main/StarInfoPanel");

        StarMgr.Instance.Init(54620, gameObject);
        StarSystemMgr.Instance.Init(gameObject);
        StartCoroutine(StarMgr.Instance.GenerateAllStar());
        _camera.GetComponent<StarMapCameraControl>().SetTarget(viewPoint);
        MouseEventBlinding.Instance.BlindMouseEvent(MouseEventBlinding.MouseEventStatus.MainPanel, 0, RaycastEvent);

    }


    private void RaycastEvent()
    {
        //����������Ҳ�����ϵ�ں�UI��ִ��
        if (!StarSystemMgr.Instance.OnSystem && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var jumpPanel = PanelMgr.Instance.Push(_starInfoPanelPrefab);
                jumpPanel.GetComponent<StarInfoPanel>().Init(StarMgr.Instance.GetStarData()[hit.transform.name]);
            }
        }
    }

}
