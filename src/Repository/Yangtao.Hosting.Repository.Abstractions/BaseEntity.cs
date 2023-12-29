using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.Common;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class BaseEntity<TKeyType> : IEntity<TKeyType>, ICloneableEntity<IEntity>
    {
        [Key]
        public TKeyType Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string? CreateUser { get; set; }

        public string? UpdateUser { get; set; }

        public IEntity Clone()
        {
            return (IEntity)MemberwiseClone();
        }
    }

    public abstract class BaseEntity : IEntity<string>
    {
        protected BaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAsString).ToString();
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string? CreateUser { get; set; }

        public string? UpdateUser { get; set; }

        public IEntity Clone()
        {
            return (IEntity)MemberwiseClone();
        }
    }

    public abstract class GuidBaseEntity : BaseEntity<Guid>
    {
        protected GuidBaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
        }
    }

    public abstract class IntegerBaseEntity : BaseEntity<int>
    {

    }

    public abstract class BigIntegerBaseEntity : BaseEntity<long>
    {

    }
}
