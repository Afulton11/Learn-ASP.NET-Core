using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace DatabaseFactory.Data
{
    public class SQLiteDatabase : Database
    {
        public override IDbCommand CreateCommand() =>
            new SqliteCommand();

        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            var cmd = CreateCommand();
            cmd.CommandText = commandText;
            cmd.Connection = connection;

            return cmd;
        }

        public override IDataParameter CreateParameter(string parameterName, object parameterValue) =>
            new SqliteParameter(parameterName, parameterValue);

        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            var cmd = CreateCommand();
            cmd.CommandText = procName;
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        public override IDbConnection CreateConnection() =>
            new SqliteConnection(Configuration.ConnectionString);

        public override IDbConnection CreateOpenConnection()
        {
            var connection = CreateConnection();
            connection.Open();

            return connection;
        }

        public void BatchCommit(params IDbCommand[] commands)
        {
            SqliteDataReader rec = null;
            rec.
        }

        public void ExecuteCommand()
    }
}
