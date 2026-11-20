using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;
using static Dapper.SqlMapper;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class PaginationBase
    {
        private const string FieldDelimiter = ",";
        private const string SortDelimiter = "-";


        public int Start { set; get; } = 0;

        public int Size { set; get; } = 10;

        public string? Sort
        {
            set
            {
                if (value.IsNullOrEmpty()) return;

                var arr = value.Split(FieldDelimiter);
                Sorts = arr.Select(a => BuildFieldSort(a)).ToArray();
            }
            get
            {
                if (FieldSorts.IsNullOrEmpty()) return string.Empty;

                var r = FieldSorts.Select(a => $"{a.Field}{SortDelimiter}{a.SortType}");
                return string.Join(FieldDelimiter, r);
            }
        }

        public IEnumerable<FieldSort> FieldSorts => Sorts;

        private FieldSort[] Sorts { set; get; } = Array.Empty<FieldSort>();

        private static FieldSort BuildFieldSort(string item)
        {
            var filed = item;
            var sort = SortType.Asc;
            if (item.Contains(SortDelimiter))
            {
                var r = item.Split(SortDelimiter);
                filed = r[0];

                if (r.Length > 1)
                {
                    var sortType = r[1];
                    if (sortType.NotNullAndEmpty())
                    {
                        var result = Enum.TryParse(sortType, true, out SortType value);
                        if (result) sort = value;
                    }
                }
            }

            return new FieldSort
            {
                Field = filed,
                SortType = sort,
            };
        }

        public string GetSortExpression()
        {
            if (Sorts.IsNullOrEmpty()) return string.Empty;

            var expressions = Sorts.Select(a =>
            {
                if (a.SortType == SortType.Asc) return a.Field;

                return $"{a.Field} desc";
            });
            return string.Join(",", expressions);
        }
    }


    public abstract class PaginationBase<TEntity> : PaginationBase where TEntity : BaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IEntityRepository<TEntity> repository);

        public virtual Task<int> GetCountAsync(IEntityRepository<TEntity> repository, CancellationToken cancellationToken = default)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> BuildPaginationQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildOrderByDescCreateTimeQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public virtual IQueryable<TEntity> BuildDefaultOrderPaginationQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime).Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildSortQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);

            var sortExpression = GetSortExpression();
            if (sortExpression.IsNullOrEmpty()) sortExpression = $"{nameof(EntityBase.CreateTime)} desc";

            return queryable.OrderBy(sortExpression);
        }

        public virtual IQueryable<TEntity> BuildSortPaginationQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildSortQuery(repository);

            return queryable.Skip(Start).Take(Size);
        }
    }

    public abstract class GuidPaginationBase<TEntity> : PaginationBase where TEntity : GuidBaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IGuidEntityRepository<TEntity> repository);

        public virtual Task<int> GetCountAsync(IGuidEntityRepository<TEntity> repository, CancellationToken cancellationToken = default)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> BuildPaginationQuery(IGuidEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildOrderByDescCreateTimeQuery(IGuidEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public virtual IQueryable<TEntity> BuildDefaultOrderPaginationQuery(IGuidEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime).Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildSortQuery(IGuidEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);

            var sortExpression = GetSortExpression();
            if (sortExpression.IsNullOrEmpty()) sortExpression = $"{nameof(EntityBase.CreateTime)} desc";

            return queryable.OrderBy(sortExpression);
        }


        public virtual IQueryable<TEntity> BuildSortPaginationQuery(IGuidEntityRepository<TEntity> repository)
        {
            var queryable = BuildSortQuery(repository);

            return queryable.Skip(Start).Take(Size);
        }
    }

    public abstract class IntegerPaginationBase<TEntity> : PaginationBase where TEntity : IntegerBaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IIntegerEntityRepository<TEntity> repository);

        public virtual Task<int> GetCountAsync(IIntegerEntityRepository<TEntity> repository, CancellationToken cancellationToken = default)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> BuildPaginationQuery(IIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildOrderByDescCreateTimeQuery(IIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public virtual IQueryable<TEntity> BuildDefaultOrderPaginationQuery(IIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime).Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildSortQuery(IIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);

            var sortExpression = GetSortExpression();
            if (sortExpression.IsNullOrEmpty()) sortExpression = $"{nameof(EntityBase.CreateTime)} desc";

            return queryable.OrderBy(sortExpression);
        }
    }

    public abstract class BigIntegerPaginationBase<TEntity> : PaginationBase where TEntity : BigIntegerBaseEntity
    {
        public abstract IQueryable<TEntity> BuildQuery(IBigIntegerEntityRepository<TEntity> repository);

        public virtual Task<int> GetCountAsync(IBigIntegerEntityRepository<TEntity> repository, CancellationToken cancellationToken = default)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> BuildPaginationQuery(IBigIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildOrderByDescCreateTimeQuery(IBigIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime);
        }

        public virtual IQueryable<TEntity> BuildDefaultOrderPaginationQuery(IBigIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.OrderByDescending(a => a.CreateTime).Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildSortQuery(IBigIntegerEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);

            var sortExpression = GetSortExpression();
            if (sortExpression.IsNullOrEmpty()) sortExpression = $"{nameof(EntityBase.CreateTime)} desc";

            return queryable.OrderBy(sortExpression);
        }
    }
}
