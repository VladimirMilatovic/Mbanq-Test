using Core.IDataAccess;
using Core.IDataModels;
using Core.Models;
using Microsoft.Extensions.Configuration;
using SQLDataAccess.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess.DataAccess
{
    public class Person_SQLDataAccess : IPerson_DataAccess
    {
        private readonly SqlDataHandlers _sqlDataHandlers;

        public Person_SQLDataAccess(IConfiguration config)
        {
            _sqlDataHandlers= new SqlDataHandlers(config);
        }
        public async Task<bool> PersonDeleteAsync(int id)
        {
            var p = new {Id=id};
            var output = await _sqlDataHandlers.DeleteData("dbo.Person_Delete", p);

            return output;
        }

        public async Task<List<PersonDataModel>> PersonGetAllAsync()
        {
            var output = await _sqlDataHandlers.LoadData<PersonDataModel, dynamic>("dbo.Person_GetAll", null);

            return output;
        }

        public async Task<PersonDataModel> PersonInsertAsnyc(PersonDataModel personDataModel)
        {
            var output = await _sqlDataHandlers.SaveDataWithRefresh("dbo.Person_Insert", personDataModel);
            return output;
        }

        public async Task PersonUpdateAsync(PersonDataModel personDataModel)
        {
            await _sqlDataHandlers.SaveData("dbo.Person_Update", personDataModel);
        }
    }
}
