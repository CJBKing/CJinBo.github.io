using ARCommon;
using ExitGames.Logging;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARServerProject.Handlers
{
    public abstract class HandlerBase
    {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public HandlerBase()
        {
            ARApplication.AR_Instance.handlers.Add((byte)opCode, this); //在Application类中注册各种Handler
            log.Debug("Hanlder:" + this.GetType().Name + "  is register.");
        }
        public abstract void OnHandlerMessage(OperationRequest request,OperationResponse response, ARPeer peer, SendParameters sendParameters);
        public abstract OperationCode opCode
        {
            get;
        }
    }
}
