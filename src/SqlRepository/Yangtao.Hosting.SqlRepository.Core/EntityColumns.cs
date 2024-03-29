﻿using System.Linq.Expressions;
using Yangtao.Hosting.SqlRepository.Abstractions;
using Yangtao.Hosting.Extensions;
using System.Collections;

namespace Yangtao.Hosting.SqlRepository.Core
{
    internal class EntityColumns<TEntity> : IEntityColumns<TEntity> where TEntity : class
    {
        private readonly IList<string> Columns;
        private readonly IList<Expression<Func<TEntity, object>>> ColumnExpressions;

        internal EntityColumns()
        {
            Columns = new List<string>();
            ColumnExpressions = new List<Expression<Func<TEntity, object>>>();
        }

        public void Add(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return;
            if (expression.Body == null) return;

            var fieldName = AnalysisExpression(expression.Body);
            if (fieldName.IsNullOrEmpty()) return;

            var exist = Columns.Any(a => a == fieldName);
            if (exist) return;

            Columns.Add(fieldName);
            ColumnExpressions.Add(expression);
        }

        public void Remove(Expression<Func<TEntity, object>> expression)
        {
            var exist = Exist(expression);
            if (exist == false) return;

            var fieldName = AnalysisExpression(expression.Body);
            var index = Columns.IndexOf(fieldName);
            if (index < 0) return;

            Columns.RemoveAt(index);

            var expressionIndex = GetExpressionIndex(fieldName);
            if (expressionIndex < 0) return;

            ColumnExpressions.RemoveAt(expressionIndex);
        }

        public string[] GetColumns()
        {
            if (Columns.IsNullOrEmpty()) return Array.Empty<string>();

            return Columns.ToArray();
        }

        public bool IsEmpty => Columns.Count == 0;

        private bool Exist(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return false;
            if (expression.Body == null) return false;

            var fieldName = AnalysisExpression(expression.Body);
            if (fieldName.IsNullOrEmpty()) return false;

            return Columns.Any(a => a == fieldName);
        }

        private static string AnalysisExpression(Expression expression)
        {
            if (expression is MemberExpression memberExpression) return memberExpression.Member.Name;
            if (expression is UnaryExpression unaryExpression) return AnalysisExpression(unaryExpression.Operand);

            return string.Empty;
        }

        private int GetExpressionIndex(string fieldName)
        {
            for (int i = 0; i < ColumnExpressions.Count; i++)
            {
                var expression = ColumnExpressions[i];
                var name = AnalysisExpression(expression);
                if (fieldName == name) return i;
            }

            return -1;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string GetUpdateSetSql(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Expression<Func<TEntity, object>>> GetEnumerator() => ColumnExpressions.GetEnumerator();
    }
}