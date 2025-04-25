using UnityEngine;
using UnityEngine.UI;
using Exterior;
using UnityEngine.EventSystems;

public class ItemToggle : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Item item;
    //库存面板
    protected GameObject _description;
    protected GameObject _text;
    protected Sprite[] _iconSprites;

    /// <summary>
    /// 初始化参数
    /// </summary>
    /// <param name="descript"></param>
    /// <param name="name"></param>
    /// <param name="count"></param>
    /// <param name="rank"></param>
    public virtual void Init(Item item,GameObject description,Sprite[] icons)
    {
        _description = description;
        this.item = item;
        _text = transform.Find("Text").gameObject;
        _iconSprites = icons;
        //函数赋予
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
    }

    public virtual void Update()
    {
        //显示在按钮上的文本
        var count = _text.FindComponent<Text>("Count");

        gameObject.name = item.itemInfo.Name;
        count.text = item.count.ToString();
    }

    public virtual void OnValueChanged(bool IsOn)
    {
        if (IsOn)
        {
            _description.SetActive(true);
            var icon = _description.FindComponent<Image>("Icon");
            var name = _description.FindComponent<Text>("Name");
            var description = _description.FindComponent<Text>("Description");
            var count = _description.FindComponent<Text>("Count");
            var price = _description.FindComponent<Text>("Price");
            var rank = _description.FindComponent<Text>("Rank");
            var unitLoad = _description.FindComponent<Text>("UnitLoad");

            name.text = item.itemInfo.Name;
            description.text = item.itemInfo.Description;
            count.text = item.count.ToString();
            price.text = item.itemInfo.Price.ToString();
            rank.text = item.itemInfo.Rank.ToString();
            unitLoad.text = item.itemInfo.UnitLoad.ToString();
            foreach (var sprite in _iconSprites)
            {
                if (sprite.name == item.itemInfo.Name)
                {
                    icon.sprite = sprite;
                    break;
                }
            }
        }
        else
        {
            _description.SetActive(false);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Toggle>().isOn = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Toggle>().isOn = false;
    }
}
