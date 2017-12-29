using ARCommon.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.Mappings
{
    public class RoleMapping:ClassMap<Role>
    {
        public RoleMapping()
        {
            Id(x => x.RoleID).Column("Id");
            Map(x => x.RoleName).Column("Name");
            Map(x => x.RoleHP).Column("Hp");
            Map(x => x.HPRecover).Column("HPRecover");
            Map(x => x.RoleMP).Column("Mp");
            Map(x => x.MPRecover).Column("MPRecover");
            Map(x => x.NormalDamage).Column("NormalDamage");
            Map(x => x.MoveSpeed).Column("MoveSpeed");
            References(x => x.Skill).Column("SkillID");
            Table("role");
        }
    }
}
