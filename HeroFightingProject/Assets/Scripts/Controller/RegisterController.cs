using UnityEngine;
using System.Collections;
using ARCommon;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

public class RegisterController : ControllerBase
{
    public static RegisterController _instance;
    public override void Start()
    {
        base.Start();
        _instance = this;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.Register;
        }
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        BasePanel messagePanel = UIManager._Instnace.PushPanel(UiPanelType.Message, true);
        object returnMsg = response.Parameters.TryGet((byte)ReturnCode.Sucess);
        messagePanel.ShowMessage(returnMsg as string);
    }
    public void SendRequest(Dictionary<byte, object> parameter)
    {
        GameController._instance.photoEngine.SendRequest(opCode, parameter);
    }

}
