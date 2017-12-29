using UnityEngine;
using UnityEngine.EventSystems;

public class SpinWithMouse : MonoBehaviour, IDragHandler
{
    public static SpinWithMouse _instance;
    public Transform target;  //要旋轉的三維物體的Transform組件
    public float speed = 1f;
    public SpinWithMouse _Instance
    {
        get {
            if (_instance == null)
            {
                _instance = this;
            }
            return _instance;
        }
        
    }
    void Start()
    {
        if (target == null) target = transform;
    }

    public void GetTargetTrans(Transform target)
    {
        this.target = target;
    }
    public void OnDrag(PointerEventData eventData)
    {
        target.localRotation = Quaternion.Euler(0f, -0.5f * eventData.delta.x * speed, 0f) * target.localRotation;
    }
}