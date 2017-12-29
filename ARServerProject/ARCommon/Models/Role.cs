using System;
using System.Collections.Generic;
using System.Text;

namespace ARCommon.Models
{
    public class Role
    {
        public virtual int RoleID { get; set; }
        public virtual string RoleName { get; set; }
        public virtual int RoleHP { get; set; }
        public virtual int HPRecover { get; set; }
        public virtual int RoleMP { get; set; }
        public virtual int MPRecover { get; set; }
        public virtual int NormalDamage { get; set; }
        public virtual int MoveSpeed { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
