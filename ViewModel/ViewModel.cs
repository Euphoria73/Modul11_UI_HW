using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Modul11_UI_HW.Model;
using System.Windows.Input;
using Modul11_UI_HW.Commands;

namespace Modul11_UI_HW.ViewModel
{
    class ViewModel : ViewModelBase
    {
        string _title = "";

        /// <summary>
        /// Заголовок главного окна
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        Department selectedItem;

        /// <summary>
        /// Выбранный департамент в treeview
        /// </summary>
        public Department SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private static ObservableCollection<Department> _myOrganization = new ObservableCollection<Department>
                { new Department
                    {
                        NameDepartment = "Top Organization",
                        managerDepartment = new CEO("Vasya", "Pupkin")
                    }
                };
        /// <summary>
        /// Коллекция организации
        /// </summary>
        public ObservableCollection<Department> MyOrganization
        {
            get
            {
                PopulateStructure(_myOrganization[0].Departments, "Department ", 5);
                foreach (var item in _myOrganization)
                {
                    item.managerDepartment.ManagerGetSalary(item);
                }
                return _myOrganization;
            }

            set => SetProperty(ref _myOrganization, value);
        }

        public ViewModel()
        {
            OpenComand = new RelayCommand(OnOpenCommandExecuted, CanOpenCommandExecute); //пока так для проверки TODO:исправить на корректные данные
        }

        #region Команды управления программой

        public ICommand OpenComand { get; } //Команда для открытия организации

        private bool CanOpenCommandExecute(object file) => true;

        //для открытия записи организации в формате JSON
        public void OnOpenCommandExecuted(object file)
        {
            //PopulateStructure(_myOrganization, _myOrganization[0].NameDepartment, 10);
        }

        private bool CanSaveCommandExecute(object file) => true;

        //для сохранения записи организации в формате JSON
        public void OnSaveCommandExecuted(object file)
        {
            //PopulateStructure(_myOrganization, _myOrganization[0].NameDepartment, 10);
        }

        private void PopulateStructure(ObservableCollection<Department> deps, string nameDepartment, int countDivisions)
        {
            if (countDivisions == 0)
            {
                return;
            }                   
            else
            {
                for (int i = 0; i < countDivisions; i++)
                {
                    deps.Add(new Department(nameDepartment + $"{i + 1}", 2));
                    PopulateStructure(deps[i].Departments, deps[i].NameDepartment + ".", countDivisions - 1);
                }
            }
        }


        #endregion
    }
}
