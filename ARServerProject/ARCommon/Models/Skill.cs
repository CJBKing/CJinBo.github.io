using System;
using System.Collections.Generic;
using System.Text;

namespace ARCommon.Models
{
    public class Skill
    {
        public virtual int SkillID { get; set; }
        public virtual string SkillName { get; set; }
        public virtual int SkillDamage { get; set; }
        public virtual int SkillCost { get; set; }
    }
}
