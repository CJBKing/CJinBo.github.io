using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using ARCommon;

public class StartPanel : BasePanel {

    public float ShowWarnTime = 3;
    public Text warningText;
    private InputField inputName;
    private InputField inputPwd;
    private Button btnLogin;
    private Button btnRegister;
    private CanvasGroup canvasGroup;
    private CanvasGroup warnCanvasGroup;
    private float timer = 0;
    bool isShowWarn = false;
    void Update()
    {
        if(isShowWarn)
        {
            timer += Time.deltaTime;
            warnCanvasGroup.alpha = ShowWarnTime - timer;
            if (timer>ShowWarnTime)
            {
                timer = 0;
                isShowWarn = false;
            }
        }

    }
    public override void OnEnter()
    {
        inputName = transform.Find("InputName").GetComponent<InputField>();
        inputPwd = transform.Find("InputPwd").GetComponent<InputField>();
        btnLogin = transform.Find("BtnLogin").GetComponent<Button>();
        btnRegister = transform.Find("BtnRegister").GetComponent<Button>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        warnCanvasGroup = warningText.GetComponent<CanvasGroup>();
        btnLogin.onClick.AddListener(OnBtnLoginClicked);
        btnRegister.onClick.AddListener(OnBtnRegisterClicked);
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    void OnBtnLoginClicked()
    {
        if(inputName.text.Length>0&&inputPwd.text.Length>0)
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>();
            parameter.Add((byte)ParameterCode.UserName, inputName.text);
            parameter.Add((byte)ParameterCode.Pwd, inputPwd.text);
            LoginController._instance.SendRequest(parameter);
        }
       else
        {
            isShowWarn = true;
        }
    }
    public override UiPanelType uiPanelType
    {
        get
        {
            return base.uiPanelType;
        }

        set
        {
            base.uiPanelType = UiPanelType.Start;
        }
    }
    void OnBtnRegisterClicked()
    {
        UIManager._Instnace.PushPanel(UiPanelType.Register);
    }
}
