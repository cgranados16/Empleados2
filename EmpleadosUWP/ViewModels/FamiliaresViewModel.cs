using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models;
using EmpleadosUWP.Helpers;
using EmpleadosUWP.Models;
using EmpleadosUWP.Services;

using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.ViewModels
{
    public class FamiliaresViewModel : Observable
    {
        public FamiliaresViewModel()
        {
            SaveFamilyMemberCommand = new RelayCommand(async () => await SaveFamilyMember());
            //SaveFamilyMemberCommand = new RelayCommand(SaveFamilyMember);
            AddFamilyMemberCommand = new RelayCommand(AddFamilyMember);
            Family.CollectionChanged += ContentCollectionChanged;
        }

        private FamiliarViewModel _selected;

        public FamiliarViewModel Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<FamiliarViewModel> Family { get; private set; } = new ObservableCollection<FamiliarViewModel>();

        public RelayCommand SaveFamilyMemberCommand { get; private set; }
        public RelayCommand AddFamilyMemberCommand { get; private set; }

        /// <summary>
        /// Saves family member to database
        /// </summary>
        private async Task SaveFamilyMember()
        {
            try
            {
                _selected.Model.Familiar = _selected.Familiar.Model;
                if (_selected.Empleado != null && _selected.Model.Familiar != null) await App.Repository.Family.UpsertAsync(_selected.Model);
                if (!Family.Contains(_selected)) Family.Add(_selected);
            } catch (FamiliarSavingException ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "No se pudo guardar.",
                    Content = $"Ocurrió un error al guardar:\n{ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

        private void AddFamilyMember()
        {
            var newFamilyMember = new FamiliarViewModel(new Familiares(App.SelectedEmployee.IdEmpleado));
            newFamilyMember.IsNewEmployee = true;
            Selected = newFamilyMember;
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            Family.Clear();

            var data = await App.Repository.Family.GetEmployeeFamilyAsync(App.SelectedEmployee.IdEmpleado);

            foreach (var item in data)
            {
                Family.Add(new FamiliarViewModel(item));
            }

            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = Family.FirstOrDefault();
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //This will get called when the collection is changed
            Debug.Print("Change");
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (FamiliarViewModel item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (FamiliarViewModel item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            void EntityViewModelPropertyChanged(object sender2, PropertyChangedEventArgs e2)
            {
                
                Debug.Print("Modificado");
                NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                
            }
        }

        public class FamiliarSavingException : Exception
        {

            public FamiliarSavingException() : base("Error guardando el familiar.")
            {
            }

            public FamiliarSavingException(string message) : base(message)
            {
            }

            public FamiliarSavingException(string message,
                Exception innerException) : base(message, innerException)
            {

            }
        }
    }
}
