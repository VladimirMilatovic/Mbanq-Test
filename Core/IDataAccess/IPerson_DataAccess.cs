using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDataAccess
{
    public interface IPerson_DataAccess
    {
        Task<List<PersonDataModel>> PersonGetAllAsync();
        Task<PersonDataModel> PersonInsertAsnyc(PersonDataModel personDataModel);
        Task PersonUpdateAsync(PersonDataModel personDataModel);
        Task<bool> PersonDeleteAsync(int id);
    }
}
