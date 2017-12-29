using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using ARCommon;

public class RegisterPanel : BasePanel {

    public Text WaringText;
    public float ShowWarningTime=2;
    private InputField inputName;
    private InputField inputPwd;
    private InputField inputRePwd;
    private Button btnRegister;
    private Button btnClose;
    bool isShowWaring = false;
    private float timer = 0;
    private CanvasGroup canvasGroup;
    void Update()
    {
        timer += Time.deltaTime;
        if(isShowWaring)
        {
            canvasGroup.alpha = ShowWarningTime - timer;
            if(timer>ShowWarningTime)
            {
                timer = 0;
                isShowWaring = false;
            }
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
            base.uiPanelType = UiPanelType.Register;
        }
    }
    public override void OnEnter()
    {
        this.gameObject.SetActive(true);
        inputName = transform.Find("InputName").GetComponent<InputField>();
        inputPwd = transform.Find("InputPwd").GetComponent<InputField>();
        inputRePwd = transform.Find("InputRePwd").GetComponent<InputField>();
        btnRegister = transform.Find("BtnRegister").GetComponent<Button>();
        btnClose = transform.Find("BtnClose").GetComponent<Button>();
        canvasGroup = WaringText.GetComponent<CanvasGroup>();
        btnRegister.onClick.AddListener(OnBtnRegisterClicked);
        btnClose.onClick.AddListener(OnBtnCloseClicked);

    }
    public override void OnExit()
    {
        this.gameObject.SetActive(false);
    }
    void OnBtnCloseClicked()
    {
        UIManager._Instnace.PopPanel();
    }
    void OnBtnRegisterClicked()
    {
        if(inputName.text.Length>0&& inputPwd.text.Length>0&&inputPwd.text.Equals(inputRePwd.text))
        {
            Dictionary<byte, object> paramter = new Dictionary<byte, object>();
            paramter.Add((byte)ParameterCode.UserName, inputName.text);
            paramter.Add((byte)ParameterCode.Pwd, inputPwd.text);
            RegisterController._instance.SendRequest(paramter);
        }
        else
        {
            isShowWaring = true;
        }
    }
}
