﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models;
using EmpleadosUWP.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.ViewModels
{
    public class FamiliaresViewModel : Observable
    {
        public FamiliaresViewModel()
        {
            SaveFamilyMemberCommand = new RelayCommand(async () => await SaveFamilyMember());
            AddFamilyMemberCommand = new RelayCommand(AddFamilyMember);
            RemoveFamilyMemberCommand = new RelayCommand(async () => await RemoveFamilyMember());
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
        public RelayCommand RemoveFamilyMemberCommand { get; private set; }

        /// <summary>
        /// Saves family member to database
        /// </summary>
        private async Task SaveFamilyMember()
        {
            try
            {
                _selected.Model.Familiar = _selected.Familiar.Model;
                if (_selected.Empleado != null && _selected.Model.Familiar != null)
                {
                    var result = await App.Repository.Family.UpsertAsync(_selected.Model);
                    if (!Family.Contains(_selected)) Family.Add(new FamiliarViewModel(result));
                }
            } catch (Exception ex)
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

        /// <summary>
        /// Shows the form to the add a new family member.
        /// </summary>
        private void AddFamilyMember()
        {
            var newFamilyMember = new FamiliarViewModel(new Familiares(App.SelectedEmployee.IdEmpleado));
            newFamilyMember.IsNewEmployee = true;
            Selected = newFamilyMember;
        }

        /// <summary>
        /// Remove the selected family member from the database.
        /// </summary>
        private async Task RemoveFamilyMember()
        {
            try {
                _selected.Model.Familiar = _selected.Familiar.Model;
                if (_selected.Empleado != null) {
                    var result = await ShowRemoveDialog();
                    switch (result) {
                        case ContentDialogResult.Primary:
                            await App.Repository.Family.DeleteAsync(_selected.IdEmpleado, _selected.Model.Familiar.IdPersona);
                            if (Family.Contains(_selected)) Family.Remove(_selected);
                            break;
                        default:
                            break;
                    }
                }
            } catch (Exception) {
                var dialog = new ContentDialog() {
                    Title = "No se pudo eliminar.",
                    Content = $"Ocurrió un error al eliminar.",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

        private async Task<ContentDialogResult> ShowRemoveDialog()
        {
            ContentDialog dialog = new ContentDialog {
                Title = "¿Realmente desea eliminar este familiar?",
                Content = $"¿Eliminar a ¨{_selected.Model.Familiar.Nombre}¨ de la familia?",
                CloseButtonText = "Cancelar",
                PrimaryButtonText = "Eliminar"
            };
            return await dialog.ShowAsync();
        }

        /// <summary>
        /// Loads the family members of the selected employee Async.
        /// </summary>
        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            Family.Clear();
            var data = await App.Repository.Family.GetEmployeeFamilyAsync(App.SelectedEmployee.IdEmpleado);
            foreach (var item in data){
                Family.Add(new FamiliarViewModel(item));
            }
            if (viewState == MasterDetailsViewState.Both){
                Selected = Family.FirstOrDefault();
            }
        }

        /// <summary>
        /// Called when an item in the collection has changed so it can update the ViewModel
        /// </summary>
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (FamiliarViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (FamiliarViewModel item in e.NewItems)
                {
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            void EntityViewModelPropertyChanged(object sender2, PropertyChangedEventArgs e2){
                NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            }

        }

        public class FamiliarSavingException : Exception
        {

            public FamiliarSavingException() : base("Error guardando el familiar."){}

            public FamiliarSavingException(string message) : base(message){}

            public FamiliarSavingException(string message,
                Exception innerException) : base(message, innerException){}
        }
    }
}
