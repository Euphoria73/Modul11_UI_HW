using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Modul11_UI_HW.Model;
using System.Windows.Input;

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

        private static ObservableCollection<Department> _myOrganization;

        /// <summary>
        /// Коллекция организации
        /// </summary>
        public ObservableCollection<Department> MyOrganization 
        {
            get
            {
                foreach (var item in _myOrganization)
                {
                    item.managerDepartment.ManagerGetSalary(item);
                }
                return _myOrganization;
            }

            set => SetProperty(ref _myOrganization, value);
        }

        ViewModel() 
        {            
            GetPopulateStructure(10); //пока так для проверки TODO:исправить на корректные данные
        }

        private ObservableCollection<Department> _departments;

        #region Команды управления программой

        public ICommand CreateComand { get; } //Команда для открытия организации

        private bool isCreateCommandExecute(object file) => true;

        public void GetPopulateStructure(int countDivisions)
        {
            PopulateStructure(_departments, _departments[0].NameDepartment, countDivisions);
        }

        private void PopulateStructure(ObservableCollection<Department> deps, string nameDepartment, int countDivisions)
        {
            //если _departments будет null, то присвоим ему значение справа ??=
            _departments ??= new ObservableCollection<Department>
                { new Department
                    {
                        NameDepartment = $"Department ",
                        managerDepartment = new CEO("Vasya", "Pupkin")
                    }
                };
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
