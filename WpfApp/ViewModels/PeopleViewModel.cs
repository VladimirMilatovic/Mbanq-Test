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

namespace WpfApp.ViewModels
{
    public class PeopleViewModel : Screen
    {
        private readonly IPerson_DataAccess _person_DataAccess;
        private IWindowManager _window;


        public PeopleViewModel()
        {
            _person_DataAccess = new Person_SQLDataAccess(App.Config);

            _window = IoC.Get<IWindowManager>();

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadData();
        }

        private async Task LoadData()
        {
            People = await _person_DataAccess.PersonGetAllAsync();
        }

        private List<PersonDataModel> people;

        public List<PersonDataModel> People
        {
            get { return people; }
            set
            {
                people = value;
                NotifyOfPropertyChange(() => People);
            }
        }

        private PersonDataModel selectedPerson;

        public PersonDataModel SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value; }
        }

        public async Task New()
        {
            PersonViewModel viewModel = new PersonViewModel();

            var newPerson = IoC.Get<PersonViewModel>();
            SelectedPerson = new PersonDataModel();
            newPerson.Person = SelectedPerson;

            var result = await _window.ShowDialogAsync(newPerson);
            if ((result != null) && result.Value == true)
            {
                await LoadData();
            }
        }

        public async Task Edit()
        {
            PersonViewModel viewModel = new PersonViewModel();

            var newPerson = IoC.Get<PersonViewModel>();
            newPerson.Person = selectedPerson;

            var result = await _window.ShowDialogAsync(newPerson);
            if ((result != null) && result.Value == true)
            {
                await LoadData();
            }
        }

        public bool CanDelete()
        {
            bool output = false;

            if (selectedPerson?.Id != 0)
            {
                output = true;
            }

            return output;
        }

        public async Task Delete()
        {
            var output = await _person_DataAccess.PersonDeleteAsync(SelectedPerson.Id);
            if (output == true)
            {
                await LoadData();
            }
        }
    }
}
