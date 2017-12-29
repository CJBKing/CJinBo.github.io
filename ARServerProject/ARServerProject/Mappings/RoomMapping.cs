using ARCommon.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.Mappings
{
    class RoomMapping:ClassMap<Room>
    {
        public RoomMapping()
        {
            Id(x => x.ID).Column("Id");
            Map(x => x.RoomName).Column("roomName");
            Table("roomtable");
        }
    }
}
