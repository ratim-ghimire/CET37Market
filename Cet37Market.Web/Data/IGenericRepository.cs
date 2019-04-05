namespace Cet37Market.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class //Necessary for implementation of interface
    {
        IQueryable<T> GetAll();  //Returns all datas requested

        Task<T> GetByIdAsync(int id);  //gets data according to id passed

        Task CreateAsync(T entity);  //Creates the entity data in table as per data passed

        Task UpdateAsync(T entity); //To update the data 

        Task DeleteAsync(T entity); //To delete the data 

        Task<bool> ExistAsync(int id); //To verify if the data  exists


    }
}
