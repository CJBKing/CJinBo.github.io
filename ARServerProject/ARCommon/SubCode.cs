using System;
using System.Collections.Generic;
using System.Text;

namespace ARCommon
{
    public enum  SubCode
    {
        Register,
        AddRole,
        LoadRoomList,
        AddRoom,
        RemoveRoom,
        WaitTeam,//等待组队
        SendTeam,//组队的请求
        CancelTeam,//取消组队
        GetTeam,//组队成功
    }
}
