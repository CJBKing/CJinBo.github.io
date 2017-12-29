using UnityEngine;
using System.Collections;
using ARCommon;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

public class LoginController : ControllerBase {
    public static LoginController _instance;
    public override void Start()
    {
        base.Start();
        _instance = this;
    }
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.Login;
        }
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        BasePanel messagePanel = UIManager._Instnace.PushPanel(UiPanelType.Message, true);
        object returnMsg = response.Parameters.TryGet((byte)ReturnCode.Sucess);
        string msgStr = returnMsg as string;
        messagePanel.ShowMessage(msgStr);
        if(msgStr.Equals( "登录成功！"))
        {
            UIManager._Instnace.PushPanel(UiPanelType.StartMenu);
        }
    }
    public void SendRequest(Dictionary<byte, object> parameter)
    {
        GameController._instance.photoEngine.SendRequest(opCode, parameter);
    }


}
