using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SourcesInfoShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool IsEnter = false;

    private void Update()
    {
        if (IsEnter)
        {
            string info = "<size=30>"+SourcesTable.Instance[name].Name + "</size>\n" + "\u3000\u3000" + SourcesTable.Instance[name].Description;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition,
                UIManager.Instance.GetCamera(), out pos);
            //Debug.Log(pos);
            InfoPanelMgr.Instance.ShowInfoPanel(info, transform, pos);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsEnter = false;
        InfoPanelMgr.Instance.CloseInfoPanel();
    }
}
