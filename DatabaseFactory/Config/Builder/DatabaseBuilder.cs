using System;
using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseFactory.Config.Builder
{
    public sealed class DatabaseBuilder : IDatabaseBuilder, IDatabaseBuild
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private DatabaseOptions _databaseOptions;

        public IDatabaseBuild BuildWithOptions(Func<IDatabaseOptionsBuilder, DatabaseOptions> buildDatabaseOptions)
        {
            var builder = DatabaseOptionsBuilder.CreateInstance();

            _databaseOptions = buildDatabaseOptions(builder);

            return this;
        }

        public IDatabase Build()
        {
            // TODO add a 'ContextType' parameter to DatabaseOptions and use this instead.
            _serviceCollection.AddDatabase(typeof(Data.SQLiteDatabase), _databaseOptions);

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var client = serviceProvider.GetService<IDatabase>();

            return client;
        }


    }
}
