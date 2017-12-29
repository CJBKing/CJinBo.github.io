using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject
{
    public class Team
    {
        public List<ARPeer> clientPeers = new List<ARPeer>();
        public int masterRoleId = 0;
        public Team()
        {

        }
        public Team(ARPeer peer1,ARPeer peer2)
        {
            clientPeers.Add(peer1);
            clientPeers.Add(peer2);
            peer1.Team = this;
            peer2.Team = this;
            masterRoleId = peer2.LoginRole.RoleID;
        }
    }
}
