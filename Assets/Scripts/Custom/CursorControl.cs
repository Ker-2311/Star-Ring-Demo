using UnityEngine;
public class CursorControl : MonoBehaviour
{
    public Texture2D _normal;
    public Texture2D _click;

    /// <summary>
    /// 鼠标点击播放动画
    /// </summary>
    private Animator clickAnimator;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2(0, -5);//鼠标待命状态的图标，和鼠标点击后的图标 有位移偏差，需要把两个图片 放入 图片编辑里面进行查看。
                                                 //Vector2.zero;

    /// <summary>
    /// 鼠标点击 时长计时
    /// </summary>
    private float timer = 0;

    #region 鼠标坐标

    //Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标

    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标

    //Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标

    #endregion//鼠标坐标

    // Use this for initialization
    void Start()
    {
        setCursor(_normal, 0);
    }

    /// <summary>
    /// 设置当前鼠标图示
    /// </summary>
    /// <param name="cursor">cursor的鼠标图片</param>
    public void setCursor(Texture2D cursor, int idle)
    {
        if (cursor != null && idle == 0)
        {
            Cursor.SetCursor(cursor, Vector2.zero, cursorMode);
        }
        else if (cursor != null && idle == 1)
        {
            Cursor.SetCursor(cursor, Vector2.zero, cursorMode);
        }
    }

    /// <summary>
    /// 鼠标点击播放动画
    /// </summary>
    private void mouseClickAnimation()
    {
        if (clickAnimator != null)
        {
            clickAnimator.SetTrigger("Click");
        }
    }
    /// <summary>
    /// 重置 鼠标点击图标
    /// </summary>
    private void mouseClickAnimation_reset()
    {
        if (clickAnimator != null)
        {
            clickAnimator.ResetTrigger("Click");
        }
    }

    /// <summary>
    /// 跟随鼠标点击 的位置
    /// </summary>
    void MouseFollow()
    {

        //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
        //screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //获取鼠标在场景中坐标
        //mousePositionOnScreen = Input.mousePosition;

        if (clickAnimator)
        {
            mousePositionOnScreen = Input.mousePosition;
            clickAnimator.transform.position = mousePositionOnScreen;
        }

        //让场景中的Z=鼠标坐标的Z
        //mousePositionOnScreen.z = screenPosition.z;

        //将相机中的坐标转化为世界坐标
        //mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //物体跟随鼠标移动
        //transform.position = mousePositionInWorld;

        //物体跟随鼠标X轴移动
        //transform.position = new Vector3(mousePositionInWorld.x, transform.position.y, transform.position.z);

    }

    void Update()
    {
        //mouseClickAnimation_reset();
        if (Input.GetMouseButton(0))
        {
            setCursor(_click, 1); 
            //mouseClickAnimation(); 
            //MouseFollow();
            timer += Time.deltaTime;
            //print("鼠标左键按！");
        }//
        else if (Input.GetMouseButtonUp(0) && timer != 0)
        {
            mouseClickAnimation(); MouseFollow();
            //print("鼠标左键长按" + timer + "秒！");
            timer = 0;
        }//
        if (Input.GetMouseButton(1))
        {
            timer += Time.deltaTime;
            //Debug.Log("   鼠标右键 长按  " + timer + "秒！");
        }//
        if (Input.GetMouseButtonUp(0))
        {
            setCursor(_normal, 0);
            //print("鼠标左键抬起！");
        }//
        if (Input.GetMouseButtonUp(1))
        {
            //print("鼠标右键抬起！");
        }//
        if (Input.GetMouseButtonUp(2))
        {
            //print("鼠标中键抬起！");
        }//
        if (Input.GetMouseButtonUp(3))
        {
            //print("鼠标侧键抬起！");
        }//
    }
}
