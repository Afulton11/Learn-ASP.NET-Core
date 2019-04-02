using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DatabaseFactory.Config;
using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.Options;

namespace DatabaseFactory.Data
{
    public abstract class Database : IDatabase
    {
        protected readonly IOptions<DatabaseOptions> options;

        public Database(IOptions<DatabaseOptions> options)
        {
            this.options = options;
        }

        public abstract IDbConnection CreateConnection();
        public abstract IDbCommand CreateCommand();
        public abstract IDbConnection CreateOpenConnection();
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
        public abstract IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
        public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

    }
}
