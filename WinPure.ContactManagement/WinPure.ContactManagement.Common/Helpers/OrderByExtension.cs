#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text; 

#endregion

namespace WinPure.ContactManagement.Common.Helpers
{
    public static class OrderByExtension
    {
        /// <summary>
        /// This is the dynamic order by function that the <see cref="OrderBy"/> method needs to work.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static MethodCallExpression OrderByProperty<T>(this IQueryable<T> query, String propertyName, T item)
          where T : class
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            return Expression.Call(typeof(Queryable), "OrderBy", new[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
        }
    }
}
