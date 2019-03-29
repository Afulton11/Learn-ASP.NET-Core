using System;
namespace DatabaseFactory.config
{
    public interface IDatabaseConfiguration
    {
        string Name { get; }
        string ConnectionStringName { get; }
        string ConnectionString { get; }
    }
}
