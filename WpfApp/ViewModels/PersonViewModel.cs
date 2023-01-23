using Caliburn.Micro;
using Core.IDataAccess;
using Core.Models;
using Microsoft.Extensions.Configuration;
using SQLDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.ViewModels
{
    public class PersonViewModel : Screen
    {
        private readonly IPerson_DataAccess _person_DataAccess;

        private PersonDataModel person;

        public PersonDataModel Person
        {
            get { return person; }
            set { person = value; }
        }

        public PersonViewModel()
        {
            _person_DataAccess = new Person_SQLDataAccess(App.Config);
        }

        public async Task Save()
        {
            if (Person.Id > 0)
            {
                try
                {
                    await _person_DataAccess.PersonUpdateAsync(Person);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else
            {
                try
                {
                    Person = await _person_DataAccess.PersonInsertAsnyc(Person);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

            await TryCloseAsync(true);
        }

        public async Task Cancel()
        {
            await TryCloseAsync(false);
        }
    }
}
