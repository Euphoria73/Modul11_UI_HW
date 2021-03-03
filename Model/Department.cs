﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Modul11_UI_HW
{
    class Department
    {        
        /// <summary>
        /// Название департамента
        /// </summary>
        public string NameDepartment { get; set; }
        /// <summary>
        /// Начальник департамента
        /// </summary>
        public Manager managerDepartment;
        /// <summary>
        /// Новый экземпляр коллекции департаментов
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        /// <summary>
        /// Коллекция работников департамента
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        /// <summary>
        /// Контроль выбранного департамента/ведомства в TreeView для манипуляций с ним
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// Департамент-родитель ведомств (subdivision)
        /// </summary>
        public Department ParentDepartment { get; set; }

        /// <summary>
        /// Пустой конструктор департамента
        /// </summary>
        public Department()
        {           

        }
        /// <summary>
        /// Конструктор заполняемого департамента
        /// </summary>
        /// <param name="NameDepartment"></param>
        /// <param name="CountEmployees"></param>
        public Department(string NameDepartment, int CountEmployees)
        {
            this.IsSelected = false;
            this.NameDepartment = NameDepartment;
            this.managerDepartment = new Manager();
            this.Employees = new ObservableCollection<Employee>();

            for (int i = 0; i < CountEmployees; i++)
            {
                var employeePosition = EmployeeGenerator.random.Next(0, 2);

                if (employeePosition == 0)
                {
                    Employees.Add(new Worker());
                }
                else
                {
                    Employees.Add(new Intern());
                }
            }
        }
        /// <summary>
        /// Добавление ведомства в департамент-родитель
        /// </summary>
        /// <param name="subdivision">Ведомство</param>
        public void AddDepartment(Department subdivision)
        {
            subdivision.ParentDepartment = this;
            Departments.Add(subdivision);
        }
    }
}
