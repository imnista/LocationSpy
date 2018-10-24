namespace LocationSpy.Services
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Models;
    using SquirrelFramework.Domain.Service;

    #endregion

    public class LocatorService : ServiceBase<LocatorItem, LocatorRepository>
    {
        public LocatorItem Get(Expression<Func<LocatorItem, bool>> filter)
        {
            return this.Repository.Get(filter);
        }

        public IEnumerable<LocatorItem> GetAllByPageSortBy(int pageIndex, int pageSize,
            Expression<Func<LocatorItem, object>> sortBy, bool isSortByDescending = false)
        {
            return this.Repository.GetAllByPageSortBy(pageIndex, pageSize, sortBy, isSortByDescending);
        }

        public long Delete(Expression<Func<LocatorItem, bool>> filter)
        {
            return this.Repository.DeleteMany(filter);
        }
    }
}