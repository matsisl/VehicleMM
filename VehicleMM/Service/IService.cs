using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IService<TEntity> where TEntity:class
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        int Create(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
    }
}
