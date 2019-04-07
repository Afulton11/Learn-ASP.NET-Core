using System;
using System.Data;
using System.Reflection;
using DatabaseFactory.Config;
using DatabaseFactory.Data.Contracts;
using EnsureThat;

namespace DatabaseFactory.Data
{
    public abstract class Database : IDatabase
    {
        protected readonly DatabaseOptions options;

        protected Database()
            : this(new DatabaseOptions<Database>())
        {
        }

        protected Database(DatabaseOptions options)
        {
            EnsureArg.IsNotNull(options, nameof(options));

            if (!options.ContextType.GetTypeInfo().IsAssignableFrom(GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException(
$@"parameter {nameof(options)} must be assignable from {GetType().GetTypeInfo().FullName}!"
                    );
            }

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
