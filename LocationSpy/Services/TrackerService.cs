namespace LocationSpy.Services
{
    #region using directives

    using Models;
    using SquirrelFramework.Domain.Service;
    using System;
    using System.Linq.Expressions;

    #endregion using directives

    public class TrackerService : ServiceBase<TrackerItem, TrackerRepository>
    {
        public TrackerItem Get(Expression<Func<TrackerItem, bool>> filter)
        {
            return this.Repository.Get(filter);
        }

        public long Delete(Expression<Func<TrackerItem, bool>> filter)
        {
            return base.Repository.DeleteMany(filter);
        }
    }
}