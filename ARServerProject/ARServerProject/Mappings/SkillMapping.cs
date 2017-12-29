using ARCommon.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.Mappings
{
    public class SkillMapping:ClassMap<Skill>
    {
        public SkillMapping()
        {
            Id(x => x.SkillID).Column("Id");
            Map(x => x.SkillName).Column("SkillName");
            Map(x => x.SkillDamage).Column("SkillDamage");
            Map(x => x.SkillCost).Column("SkillCost");
            Table("skill");
        }
    }
}
