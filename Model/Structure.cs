using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Modul11_UI_HW.Model
{
    static class Structure
    {        
        /// <summary>
        /// Поле структуры организации
        /// </summary>
        private static readonly ObservableCollection<Department> departments = new ObservableCollection<Department>
                { new Department
                    {
                        NameDepartment = $"Top Secret",
                        managerDepartment = new CEO("Vasya", "Pupkin")
                    }
                };

        public static ObservableCollection<Department> GetDepartments
        {
            get
            {
                PopulateStructure(departments[0].Departments, "Department ", 5);
                return departments;
            }        
        }
               
        /// <summary>
        /// Заполнение структуры данными
        /// </summary>
        /// <param name="deps"></param>
        /// <param name="nameDepartment"></param>
        /// <param name="countDivisions"></param>
        private static void PopulateStructure(ObservableCollection<Department> deps, string nameDepartment, int countDivisions)
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
