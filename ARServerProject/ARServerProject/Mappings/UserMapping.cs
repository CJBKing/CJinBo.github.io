using ARCommon.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.Mappings
{
    class UserMapping:ClassMap<User>
    {
        public UserMapping()
        {
            Id(x => x.UserID).Column("Id");
            Map(x => x.UserName).Column("userName");
            Map(x => x.Pwd).Column("Pwd");
            Table("usertable");
        }
    }
}
