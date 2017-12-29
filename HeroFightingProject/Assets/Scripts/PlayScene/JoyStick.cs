using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler//需要注意继承的接口，接口内的方法需要实现
{

   
    public float JoyStickRadius = 50;

    public float JoyStickResetSpeed = 5.0f;

    private RectTransform selfTransform;

    private bool isTouched = false;

    private Vector2 originPosition;

    private Vector2 touchedAxis;
    public Vector2 TouchedAxis
    {
        get
        {
            if (touchedAxis.magnitude < JoyStickRadius)
                return touchedAxis.normalized / JoyStickRadius;
            return touchedAxis.normalized;
        }
    }

  
    public delegate void JoyStickTouchBegin(Vector2 vec);
    
    public delegate void JoyStickTouchMove(Vector2 vec);

 
    public delegate void JoyStickTouchEnd();

    public event JoyStickTouchBegin OnJoyStickTouchBegin;


    public event JoyStickTouchMove OnJoyStickTouchMove;

    
    public event JoyStickTouchEnd OnJoyStickTouchEnd;

    void Start()
    {
        selfTransform = this.GetComponent<RectTransform>();
        originPosition = selfTransform.anchoredPosition;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
        touchedAxis = GetJoyStickAxis(eventData);
        if (this.OnJoyStickTouchBegin != null)
            this.OnJoyStickTouchBegin(TouchedAxis);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        selfTransform.anchoredPosition = originPosition;
        touchedAxis = Vector2.zero;
        if (this.OnJoyStickTouchEnd != null)
            this.OnJoyStickTouchEnd();
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchedAxis = GetJoyStickAxis(eventData);
        if (this.OnJoyStickTouchMove != null)
            this.OnJoyStickTouchMove(TouchedAxis);
    }


    void Update()
    {
        //当虚拟摇杆移动到最大半径时摇杆无法拖动
        //为了确保被控制物体可以继续移动
        //在这里手动触发OnJoyStickTouchMove事件
        if (isTouched && touchedAxis.magnitude >= JoyStickRadius)
        {
            if (this.OnJoyStickTouchMove != null)
                this.OnJoyStickTouchMove(TouchedAxis);
        }

        //松开虚拟摇杆后让虚拟摇杆回到默认位置
        if (selfTransform.anchoredPosition.magnitude > originPosition.magnitude)
            selfTransform.anchoredPosition -= TouchedAxis * Time.deltaTime * JoyStickResetSpeed;
    }

    /// <summary>
        /// 返回虚拟摇杆的偏移量
        /// </summary>
        /// <returns>The joy stick axis.</returns>
        /// <param name="eventData">Event data.</param>
    private Vector2 GetJoyStickAxis(PointerEventData eventData)
    {
        //获取手指位置的世界坐标
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(selfTransform,
        eventData.position, eventData.pressEventCamera, out worldPosition))
            selfTransform.position = worldPosition;
        //获取摇杆的偏移量
        Vector2 touchAxis = selfTransform.anchoredPosition - originPosition;
        //摇杆偏移量限制
        if (touchAxis.magnitude >= JoyStickRadius)
        {
            touchAxis = touchAxis.normalized * JoyStickRadius;
            selfTransform.anchoredPosition = touchAxis;
        }
        return touchAxis;
    }


}