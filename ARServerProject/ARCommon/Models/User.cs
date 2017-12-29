using System;
using System.Collections.Generic;
using System.Text;

namespace ARCommon.Models
{
    public class User
    {
        public virtual int UserID { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Pwd { get; set; }
    }
}
