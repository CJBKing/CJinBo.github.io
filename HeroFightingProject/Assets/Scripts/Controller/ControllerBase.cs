using UnityEngine;
using System.Collections;
using ARCommon;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour
{
    public abstract OperationCode opCode
    {
        get;
    }
    public virtual void Start()
    {
        PhotonEngine.photonInstance.RegisterController(opCode, this);
    }
    public virtual void OnDestroy()
    {
        PhotonEngine.photonInstance.UnRegisterController(opCode);
    }
    public abstract void OnOperationResponse(OperationResponse response);

}
