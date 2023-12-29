namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IEntityBase : IModel
    {
        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public string? CreateUser { set; get; }

        public string? UpdateUser { set; get; }
    }

    /// <summary>
    /// 实体接口，Id 类型默认 string
    /// </summary>
    public interface IEntity : IEntityBase
    {
        public string Id { get; set; }
    }

    public interface IEntity<TKeyType> : IEntityBase
    {
        public TKeyType Id { get; set; }
    }
}