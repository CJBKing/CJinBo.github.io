using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Text;
using PhotonHostRuntimeInterfaces;
using ARServerProject.Handlers;
using ExitGames.Logging;
using ARCommon.Models;
using ARServerProject.DB;
using ARServerProject.DB.Managers;

namespace ARServerProject
{
    public class ARPeer : PeerBase
    {
        public Team Team { get; set; }
        public Role LoginRole { get; set; }
        public Room roomField { get; set; }
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public ARPeer(IRpcProtocol protocol, IPhotonPeer photonPeer) : base(protocol, photonPeer)
        {

        }
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if(ARApplication.AR_Instance.peerListForTeam.Contains(this))
            {
                ARApplication.AR_Instance.peerListForTeam.Remove(this);
                if (ARApplication.AR_Instance.peerListForTeam.Count<1)
                {
                    RoomManager room = new RoomManager();
                    foreach (Room r in room.GetAllRoom())
                    {
                        room.RemoveRoomByName(r.RoomName);
                    }
                }
                
            }

        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            ARApplication.AR_Instance.handlers.TryGetValue(operationRequest.OperationCode, out handler);
            if (handler != null)
            {
                OperationResponse response = new OperationResponse();
                response.OperationCode = operationRequest.OperationCode;
                response.Parameters = new Dictionary<byte, object>();
                handler.OnHandlerMessage(operationRequest, response, this,sendParameters);
                SendOperationResponse(response, sendParameters);
            }
            else
            {
                //ARApplication的字典Handlers中没有operationRequest.OperationCode对应的Handler；
            }
        }
    }
}
