using Dapper.Contrib.Extensions;
using Yangtao.Hosting.Common;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class EntityBase : IEntityBase, ICloneableEntity<IEntityBase>
    {
        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string? CreateUser { get; set; }

        public string? UpdateUser { get; set; }

        public IEntityBase Clone()
        {
            return (IEntityBase)MemberwiseClone();
        }
    }

    public abstract class BaseEntity : EntityBase, IEntity<string>
    {
        protected BaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAsString).ToString();
        }

        [ExplicitKey]
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }
    }

    public abstract class GuidBaseEntity : EntityBase, IEntity<Guid>
    {
        protected GuidBaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
        }

        [ExplicitKey]
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
    }

    public abstract class IntegerBaseEntity : EntityBase, IEntity<int>
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
    }

    public abstract class BigIntegerBaseEntity : EntityBase, IEntity<long>
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }
    }
}
