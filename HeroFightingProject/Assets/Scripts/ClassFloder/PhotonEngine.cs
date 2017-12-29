using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;
using System.Net;
using System.Collections.Generic;
using ARCommon;

public class PhotonEngine : IPhotonPeerListener
{
    IPHostEntry hostEntry;
    public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
    public string IPaddress = string.Empty;
    //public string IPAddress = IP+":4530";  
    public string applicationName = "ARApplication";
    public Dictionary<byte, ControllerBase> controllerDic = new Dictionary<byte, ControllerBase>();

    public delegate void OnConnectedToServerEvent();
    public event OnConnectedToServerEvent OnConnectedToServer;
    public delegate void OnDisConnectedEvent();
    public event OnDisConnectedEvent OnDisConnect;


    public static PhotonEngine photonInstance
    {
        get
        {
            return _instance;
        }
    }
    private static PhotonEngine _instance;
    public PhotonPeer peer;
    bool isConnected = false;
    public PhotonEngine()
    {
        _instance = this;
        hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in hostEntry.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPaddress = ip+ ":4530";
            }
        }
        peer = new PhotonPeer(this, protocol);
        peer.Connect(IPaddress, applicationName);
    }
  
    public void RegisterController(OperationCode opCode, ControllerBase controller)  //在ControllerBase中调用此方法  
    {
        controllerDic.Add((byte)opCode, controller);
    }
    public void UnRegisterController(OperationCode opCode)
    {
        controllerDic.Remove((byte)opCode);
    }
    public void SendRequest(OperationCode opCode, Dictionary<byte, object> parameter)
    {
        peer.OpCustom((byte)opCode, parameter, true);
    }
    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnEvent(EventData eventData)
    {

    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase controllerObject;
        controllerDic.TryGetValue(operationResponse.OperationCode, out controllerObject);  //根据operationResponse.Operationcode得到字典中已经注册好的对应的Controller  
        controllerObject.OnOperationResponse(operationResponse);//调用Controller中的方法；  
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                {
                    isConnected = true;
                   BasePanel messagePanel= UIManager._Instnace.PushPanel(UiPanelType.Message,true);
                    messagePanel.ShowMessage("网络已连接！");
                }
                break;
            case StatusCode.Disconnect:
                {
                    BasePanel messagePanel = UIManager._Instnace.PushPanel(UiPanelType.Message,true);
                    messagePanel.ShowMessage("网络已断开！");
                }
                break;
        }
    }
}


