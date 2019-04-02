using System;
namespace DatabaseFactory.Config.Builder
{
    public interface IDatabaseOptionsBuilder
    {
        /// <summary>
        /// Builds a SQLite database instance with the given connectionString.
        /// </summary>
        /// <param name="connectionString">The string to connect to the server</param>
        IDatabaseOptionsBuild useSqliteServer(string connectionString);
    }

    public interface IDatabaseOptionsBuild
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseOptions"/> class that configues the Database.
        /// </summary>
        /// <returns>A new <see cref="DatabaseOptions"/> instance</returns>
        DatabaseOptions Build();
    }
}
