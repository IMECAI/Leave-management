using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Contracts
{
    //Generic enough to pass in any class. C.R.U.D
    public interface IRepositoryBase<T> where T : class
    {
        //Accepting any type of array objects passed in, T is a generic class
        ICollection<T> FindAll();// a Function to get all record class types i am looking for
        T FindById(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
