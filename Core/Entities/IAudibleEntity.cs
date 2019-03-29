using System;
namespace Core.Data
{
    public interface IAudibleEntity
    {
        string CreationUser { get; set; }

        DateTime CreationDateTime { get; set; }

        string LastUpdateUser { get; set; }

        DateTime? LastUpdateDateTime { get; set; }
    }
}
