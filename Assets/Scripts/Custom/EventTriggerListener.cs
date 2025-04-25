using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
    public delegate void VoidDelegate(GameObject obj);

    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onUp;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;//关联更新

    public static EventTriggerListener Get(GameObject obj)
    {
        //这样就不需要为每个按钮添加脚本了
        EventTriggerListener listener = obj.GetComponent<EventTriggerListener>();
        if (listener == null)
        {
            listener = obj.AddComponent<EventTriggerListener>();
        }
        return listener;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }
}
