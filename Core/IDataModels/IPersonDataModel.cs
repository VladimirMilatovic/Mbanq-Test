using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDataModels
{
    public interface IPersonDataModel
    {
        int Id { get; set; }
        string OIB { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Place { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string Mail { get; set; }
    }
}
