using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessagePanel : BasePanel {

    public float showTime = 2;
    private Text msgText;
    private CanvasGroup canvasGroup;
    private float timer = 0;
    public bool isShow = false;
    void Awake()
    {
        msgText = transform.Find("MsgText").GetComponent<Text>();
        canvasGroup = this.GetComponent<CanvasGroup>();
    }
    public override UiPanelType uiPanelType
    {
        get
        {
            return base.uiPanelType;
        }

        set
        {
            base.uiPanelType = UiPanelType.Message;
        }
    }
    public override void OnEnter()
    {
        this.gameObject.SetActive(true);
       
    }
    void Update()
    {
       
        if(isShow)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = showTime - timer;
            if (timer >= showTime)
            {
                UIManager._Instnace.PopPanel();
                timer = 0;
                isShow = false;
            }
        }
    }
    public override void ShowMessage(string msg)
    {
        isShow = true;
        msgText.text = msg;
    }
}
