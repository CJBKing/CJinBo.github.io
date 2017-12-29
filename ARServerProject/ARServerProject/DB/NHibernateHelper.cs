using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iesi.Collections;

namespace ARServerProject.DB
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory = null;
        private static void InitSessionFactory()
        {
            MySQLConfiguration mySQlConfig = MySQLConfiguration.Standard.ConnectionString(db => db.Server("127.0.0.1").Database("ardatabase").Username("root").Password("root"));
            sessionFactory = Fluently.Configure().Database(mySQlConfig).Mappings(x => x.FluentMappings.AddFromAssemblyOf<NHibernateHelper>()).BuildSessionFactory();
        }
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    InitSessionFactory();
                return sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
       

    
     }
}
