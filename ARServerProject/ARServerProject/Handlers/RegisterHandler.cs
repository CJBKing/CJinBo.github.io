using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCommon;
using Photon.SocketServer;
using ARCommon.Tools;
using ARServerProject.DB.Managers;
using ARCommon.Models;

namespace ARServerProject.Handlers
{
    public class RegisterHandler : HandlerBase
    {
        private UserManager userManager;
        public RegisterHandler():base()
        {
            userManager = new UserManager();
        }
        public override void OnHandlerMessage(OperationRequest request, OperationResponse respons, ARPeer peer, SendParameters sendParameters)
        {
            string  userName = ParameterTool.GetParameter<string>(request.Parameters, ParameterCode.UserName, false);
            string pwd = ParameterTool.GetParameter<string>(request.Parameters, ParameterCode.Pwd, false);
           IList<User> userList=userManager.GetUserName(userName);
            if (userList.Count == 0|| userList==null)
            {
                User user = new User();
                user.UserID = 1;
                user.UserName = userName;
                user.Pwd = pwd;
                userManager.AddUser(user);
                Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                parameter.Add((byte)ReturnCode.Sucess, "注冊成功了！");
                respons.Parameters = parameter;
            }
            if (userList.Count>0)
            {
                Dictionary<byte, object> parameter = new Dictionary<byte, object>();
                parameter.Add((byte)ReturnCode.Sucess, "用户名已存在！");
                respons.Parameters = parameter;
            }
           
        }

      
        public override OperationCode opCode
        {
            get
            {
                return OperationCode.Register;
            }
        }
    }
}
