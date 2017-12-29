using ARCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.DB.Managers
{
    public class UserManager
    {
        public void AddUser(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }
        public IList<User> GetUserName(string userName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<User>().Where(x => x.UserName == userName);
                    transaction.Commit();
                    return userList.List();
                }
            }
        }
    }
}
