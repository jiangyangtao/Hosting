using System.ComponentModel.DataAnnotations;
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

    public abstract class BaseEntity<TKeyType> : EntityBase, IEntity<TKeyType>
    {
        [Key]
        public TKeyType Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<string>
    {
        protected BaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAsString).ToString();
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
