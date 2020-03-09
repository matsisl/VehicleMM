using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IDataSource
    {
        SQLiteAsyncConnection GetConnection();
    }
}
