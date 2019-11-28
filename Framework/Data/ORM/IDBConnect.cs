using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.Data.ORM
{
    public interface IDbConnect
    {
        IDbConnection GetIDbConnection();
        IDbCommand GetIDbCommand();

        void Close();
    }
}
