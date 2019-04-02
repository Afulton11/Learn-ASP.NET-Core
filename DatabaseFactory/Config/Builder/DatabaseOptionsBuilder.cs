using System;
namespace DatabaseFactory.Config.Builder
{
    public sealed class DatabaseOptionsBuilder : IDatabaseOptionsBuilder, IDatabaseOptionsBuild
    {
        private readonly DatabaseOptions _databaseOptions = new DatabaseOptions();

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseOptionsBuilder"/> class for building <see cref="DatabaseOptions"/>
        /// </summary>
        /// <returns>The instance.</returns>
        public static IDatabaseOptionsBuilder CreateInstance()
            => new DatabaseOptionsBuilder();

        private DatabaseOptionsBuilder() {}


        public IDatabaseOptionsBuild useSqliteServer(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _databaseOptions.ConnectionString = connectionString;

            return this;
        }

        public DatabaseOptions Build() => _databaseOptions;
    }
}
