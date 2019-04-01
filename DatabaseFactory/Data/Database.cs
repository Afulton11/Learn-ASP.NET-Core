using System;
using System.Data;
using DatabaseFactory.Config;

namespace DatabaseFactory.Data
{
    public abstract class Database
    {
        public DatabaseConfiguration Configuration { get; set; }

        public abstract IDbConnection CreateConnection();
        public abstract IDbCommand CreateCommand();
        public abstract IDbConnection CreateOpenConnection();
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
        public abstract IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
        public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

    }
}
