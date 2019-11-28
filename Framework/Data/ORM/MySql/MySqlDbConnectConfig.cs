using Framework.Data.ORM;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Framework.Data.ORM
{
    public class MySqlDbConnectionConfig : ConfigManager<MySqlDbConntionConfigureItem>
    {
        static MySqlDbConnectionConfig mySqlDbConnectionConfig;
        readonly static object obj = new object();
        private MySqlDbConnectionConfig() { }
        public static string GetSqlDbConntionConfigure()
        {
            lock (obj)
            {
                if (mySqlDbConnectionConfig == null)
                {
                    mySqlDbConnectionConfig = new MySqlDbConnectionConfig();
                    return mySqlDbConnectionConfig.LoadConfig();
                }
                return mySqlDbConnectionConfig.LoadConfig();
            }
        }

        public string LoadConfig()
        {
            //"bin\\Debug\\netcoreapp2.2\\ConfigResource\\mysqlconfig.xml"
            string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}{ConfigurationManager.AppSettings["sqlconfigPath"]}";
            XDocument xDoc = this.LocalConfig(configPath);
            var sc = from t in xDoc.Root.Elements("sqlconntionitem")
                     select MySqlDbConnectionConfig.CreaterConfigureItem(t);
            var s = sc.First();

            return sc.First().ToString();
        }
    }
}

public class MySqlDbConntionConfigureItem : IDBConfig
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DbName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Key { get; internal set; }


    public override string ToString()
    {
        return $"server={this.Host};user id={UserName};password={Password};persistsecurityinfo=True;port={this.Port};database={DbName};SslMode=none;Allow User Variables=True;Pooling=true;Max Pool Size=600;";
    }
}