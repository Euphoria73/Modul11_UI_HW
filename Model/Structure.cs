using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Modul11_UI_HW.Model
{
    public class Structure
    {
        /// <summary>
        /// Поле структуры организации
        /// </summary>
        private static readonly ObservableCollection<Department> departments = new ObservableCollection<Department>
                { new Department
                    {
                        NameDepartment = $"Department ",
                        managerDepartment = new CEO("Vasya", "Pupkin")
                    }
                };
                
        public void GetPopulateStructure(int countDivisions)
        {
            PopulateStructure(departments, departments[0].NameDepartment, countDivisions);
        }

        //Рекурсивный метод заполнения 
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
    }
}
