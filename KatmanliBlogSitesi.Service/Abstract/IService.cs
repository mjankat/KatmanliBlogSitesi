using KatmanliBlogSitesi.Data.Abstract;
using KatmanliBlogSitesi.Entities;

namespace KatmanliBlogSitesi.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {
    }
}
