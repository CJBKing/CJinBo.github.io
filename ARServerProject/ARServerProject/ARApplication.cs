using ARCommon;
using ARServerProject.Handlers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ARServerProject
{
    public class ARApplication : ApplicationBase
    {
        public List<ARPeer> peerListForTeam = new List<ARPeer>();
        public Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();
        private static ARApplication _instance;
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public ARApplication()
        {
            _instance = this;
            RegisterHandler();
        }
        public static ARApplication AR_Instance
        {
            get {
                return _instance;
            }
        }
        void RegisterHandler()
        {
            //handlers.Add((byte)OperationCode.Login,new LoginHandler());
            //handlers.Add((byte)OperationCode.MakeRoom,new MakeRoomHandler());
            Type[] types = Assembly.GetAssembly(typeof(HandlerBase)).GetTypes();   //得到所有继承自HandlerBase的类
            foreach (var type in types)
            {
                if (type.FullName.EndsWith("Handler"))
                {
                    Activator.CreateInstance(type);    //创建类的实例，为了能够使各个Handler的构造函数实现在ARApplication中注册
                }
            }
        }
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ARPeer(initRequest.Protocol,initRequest.PhotonPeer);
        }

        protected override void Setup()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "MS" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
            log.Debug("ARServer is Setup!!!");
        }

        protected override void TearDown()
        {
            log.Debug("ARServer is TearDown!!!");
        }
    }
}
