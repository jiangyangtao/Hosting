using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Yangtao.Hosting.Common;
using Yangtao.Hosting.Extensions;

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

        public virtual Task<int> GetCountAsync(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);
            return queryable.CountAsync();
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

        public virtual IQueryable<TEntity> BuildSortPaginationQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildSortQuery(repository);

            return queryable.Skip(Start).Take(Size);
        }

        public virtual IQueryable<TEntity> BuildSortQuery(IEntityRepository<TEntity> repository)
        {
            var queryable = BuildQuery(repository);

            var sortExpression = GetSortExpression();
            if (sortExpression.IsNullOrEmpty()) sortExpression = $"{nameof(EntityBase.CreateTime)} desc";

            return queryable.OrderBy(sortExpression);
        }
    }

    public abstract class QueryParamBase<TEntity> : PaginationBase<TEntity> where TEntity : BaseEntity
    {
        protected QueryParamBase(PagingParameter parameter)
        {
            Start = parameter.startIndex;
            Size = parameter.size;
            Sort = parameter.sort;
        }
    }
}
