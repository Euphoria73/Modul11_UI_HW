using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Modul11_UI_HW.Model;

namespace Modul11_UI_HW.ViewModel
{
    class ViewModel : ViewModelBase
    {
        private static ObservableCollection<Department> _myOrganization;     
      
        public ObservableCollection<Department> MyOrganization //Коллекция для отображения с головы (так же есть возможность создать несколько организаций)
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
            Structure structure = new Structure();
            structure.GetPopulateStructure(4);
        }

    }
}
