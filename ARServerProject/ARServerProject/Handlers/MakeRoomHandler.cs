using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using ARCommon;
using ARServerProject.DB.Managers;
using LitJson;
using ExitGames.Logging;
using ARCommon.Models;
using ARCommon.Tools;

namespace ARServerProject.Handlers
{
    public class MakeRoomHandler : HandlerBase
    {
        private RoomManager roomManager;
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public MakeRoomHandler() : base()
        {
            roomManager = new DB.Managers.RoomManager();
        }
        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ARPeer peer, SendParameters sendParameters)
        {
            SubCode subcode = ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode, false);
            switch (subcode)
            {
                case SubCode.LoadRoomList:
                    {
                        IList<Room> roomList = roomManager.GetAllRoom();
                        string jsonObject = JsonMapper.ToJson(roomList);
                        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                        parameter.Add((byte)ParameterCode.SubCode,SubCode.LoadRoomList);
                        parameter.Add((byte)ParameterCode.ServerList, jsonObject);
                        response.Parameters = parameter;
                    }
                    break;
                case SubCode.AddRoom:
                    {

                        Room roomToAdd = ParameterTool.GetParameter<Room>(request.Parameters, ParameterCode.RoomName);
                        roomManager.AddRoom(roomToAdd);
                        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                        parameter.Add((byte)ParameterCode.SubCode, SubCode.AddRoom);
                        response.Parameters = parameter;
                    }
                    break;
                case SubCode.RemoveRoom:
                    {
                       string roomName= ParameterTool.GetParameter<string>(request.Parameters,ParameterCode.RoomName,false);
                        roomManager.RemoveRoomByName(roomName);
                        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                        parameter.Add((byte)ParameterCode.SubCode, SubCode.RemoveRoom);
                        parameter.Add((byte)ParameterCode.RoomName, "成功移除房间");
                        response.Parameters = parameter;
                    }
                    break;
               
            }

        }
        public override OperationCode opCode
        {
            get
            {
                return OperationCode.MakeRoom;
            }
        }
    }
}
