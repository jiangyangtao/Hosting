﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core.Providers
{
    internal class ViewRepositoryProvider<TView> : IViewRepositoryProvider<TView> where TView : BaseView
    {
        private readonly DbContext _dbContext;

        public ViewRepositoryProvider(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TView> Get() => _dbContext.Set<TView>();


        public IQueryable<TView> Get(Expression<Func<TView, bool>> predicate) => _dbContext.Set<TView>().Where(predicate);


        public IQueryable<TView> SqlQuery(FormattableString sql) => _dbContext.Database.SqlQuery<TView>(sql);


        public IQueryable<TView> SqlQueryRaw(string sql, params object[] parameters) => _dbContext.Database.SqlQueryRaw<TView>(sql, parameters);
    }
}