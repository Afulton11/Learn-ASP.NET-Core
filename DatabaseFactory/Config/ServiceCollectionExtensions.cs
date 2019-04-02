using System;
using System.Linq;
using DatabaseFactory.Config.Builder;
using DatabaseFactory.Data;
using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DatabaseFactory.Config
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase<TDb>(
            this IServiceCollection services,
            Func<IDatabaseOptionsBuilder, DatabaseOptions> buildDatabaseOptions)
            where TDb : Database =>
            services
                .BuildOptions(buildDatabaseOptions)
                .RegisterDependencies()
                .RegisterDatabase(typeof(TDb));

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            Type databaseType,
            DatabaseOptions options)
        {
            if(databaseType != typeof(Database))
            {
                throw new ArgumentException("Must extend Database!", nameof(databaseType));
            }

            return services
                .RegisterOptions(options)
                .RegisterDependencies()
                .RegisterDatabase(databaseType);
        }

        private static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.RegisterAllTypes(typeof(IQueryProcessor));
            services.RegisterAllTypes(typeof(IHandleCommand<>));
            services.RegisterAllTypes(typeof(IHandleQuery<,>));
            services.RegisterAllTypes(typeof(IRepsoitory<>));

            return services;
        }

        private static IServiceCollection RegisterDatabase(this IServiceCollection services, Type type)
        {
            services.TryAddSingleton(new ServiceDescriptor(typeof(IDatabase), type));

            return services;
        }

        private static void RegisterAllTypes(this IServiceCollection services, Type type)
        {
            var assembly = type.Assembly;
            var typesFromAssemblies = assembly.DefinedTypes.Where(x => x.GetInterfaces().Contains(type));

            foreach (var implType in typesFromAssemblies)
                services.Add(new ServiceDescriptor(type, implType, ServiceLifetime.Transient));
        }

        private static IServiceCollection BuildOptions(
            this IServiceCollection services,
            Func<IDatabaseOptionsBuilder, DatabaseOptions> buildDatabaseOptions)
        {
            var builder = DatabaseOptionsBuilder.CreateInstance();
            var options = buildDatabaseOptions(builder);

            return services.RegisterOptions(options);
        }

        private static IServiceCollection RegisterOptions(
            this IServiceCollection services,
            DatabaseOptions options)
        {
            services.TryAddSingleton(Options.Create(options));

            return services;
        }

    }
}
