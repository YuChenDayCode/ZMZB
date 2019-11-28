using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.ORM
{
    interface IDBConfig
    {
        string Host { get; set; }
        string Port { get; set; }
        string DbName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
