namespace element_backstage.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class elementDB : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“elementDB”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“element_backstage.Models.elementDB”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“elementDB”
        //连接字符串。
        public elementDB()
            : base("name=elementDB")
        {
        }
        public DbSet<Customers> Customers { get; set; }
        //public virtual DbSet<Customers> Customers { get; set; }
    }
    
}