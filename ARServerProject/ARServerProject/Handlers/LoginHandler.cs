using System;
using System.Collections.Generic;
using System.Text;
using ARCommon;
using Photon.SocketServer;
using ARCommon.Tools;
using ARServerProject.DB.Managers;
using ARCommon.Models;

namespace ARServerProject.Handlers
{
    public class LoginHandler : HandlerBase
    {
        private UserManager userManager;
        public LoginHandler():base()
        {
            userManager = new UserManager();
        }
        public override void OnHandlerMessage(OperationRequest request,OperationResponse respons ,ARPeer peer, SendParameters sendParameters)
        {
            string userName = ParameterTool.GetParameter<string>(request.Parameters, ParameterCode.UserName, false);
            string pwd = ParameterTool.GetParameter<string>(request.Parameters, ParameterCode.Pwd, false);
            IList<User> userList = userManager.GetUserName(userName);
            if (userList.Count == 0 || userList == null)
            {
                Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                parameter.Add((byte)ReturnCode.Sucess, "登录失败，用户名或密码错误！");
                respons.Parameters = parameter;
            }
            if (userList.Count > 0)
            {
                Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                parameter.Add((byte)ReturnCode.Sucess, "登录成功！");
                respons.Parameters = parameter;
            }
        }
        public override OperationCode opCode
        {
            get
            {
                return OperationCode.Login;
            }
        }
    }
}
