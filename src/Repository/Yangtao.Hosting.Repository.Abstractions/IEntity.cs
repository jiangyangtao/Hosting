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
    /// 实体接口
    /// </summary>
    public interface IEntity<TKeyType> : IEntityBase
    {
        public TKeyType Id { get; set; }
    }
}