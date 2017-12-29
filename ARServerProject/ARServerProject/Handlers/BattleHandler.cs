using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCommon;
using Photon.SocketServer;
using ARCommon.Tools;

namespace ARServerProject.Handlers
{
    public class BattleHandler : HandlerBase
    {
        public override OperationCode opCode
        {
            get
            {
                return OperationCode.Battle;
            }
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ARPeer peer, SendParameters sendParameters)
        {
            SubCode subcode = ParameterTool.GetParameter<SubCode>(request.Parameters,ParameterCode.SubCode,false);
            switch (subcode)
            {
               
                case SubCode.AddRole:
                    {
                        int heroIndex = ParameterTool.GetParameter<int>(request.Parameters, ParameterCode.RoleID, false);
                        Dictionary<byte, object> responseParameter = new Dictionary<byte, object>();
                        responseParameter.Add((byte)ParameterCode.RoleID, heroIndex);
                        //response.Parameters = responseParameter;
                        for(int i=0;i<ARApplication.AR_Instance.peerListForTeam.Count;i++)
                        {
                            if(peer!= ARApplication.AR_Instance.peerListForTeam[i])
                            {
                                EventData data = new EventData();
                                data.Parameters = responseParameter;
                                ARApplication.AR_Instance.peerListForTeam[i].SendEvent(data,sendParameters);
                            }
                        }
                    }break;
                case SubCode.SendTeam:
                    {
                        if (ARApplication.AR_Instance.peerListForTeam.Count == 2)
                        {
                            ARPeer peer1 = ARApplication.AR_Instance.peerListForTeam[0];
                            Team t = new Team(peer1, peer);
                            Dictionary<byte, object> parameterDic = new Dictionary<byte, object>();
                            parameterDic.Add((byte)ParameterCode.SubCode, SubCode.GetTeam);
                            response.Parameters = parameterDic;
                        }
                        else
                        {
                            ARApplication.AR_Instance.peerListForTeam.Add(peer);
                            Dictionary<byte, object> parameterDic = new Dictionary<byte, object>();
                            parameterDic.Add((byte)ParameterCode.SubCode, SubCode.WaitTeam);
                            response.Parameters = parameterDic;
                        }

                    }
                    break;
            }
        }
    }
}
