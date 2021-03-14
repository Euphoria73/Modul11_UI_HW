using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Modul11_UI_HW.Model;
using System.Windows.Input;
using Modul11_UI_HW.Commands;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

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

        /// <summary>
        /// Коллекция организации
        /// </summary>
        public ObservableCollection<Department> GetOrganization
        {
            get
            {
                PopulateStructure(_myOrganization[0].Departments, "Department ", 5);
                return _myOrganization;
            }

            set => SetProperty(ref _myOrganization, value);
        }

        #region Команды управления программой

        public ICommand CreateCommand { get; } //Команда для открытия организации

        private bool CanCreateCommandExecute(object file) => true;

        //Создание структуры компании 
        public void OnCreateCommandExecuted(object file)
        {                    
            MessageBox.Show($"{_myOrganization[0].Departments.Count}");
        }

        public ICommand OpenCommand { get; } //Команда для открытия организации

        private bool CanOpenCommandExecute(object path) => true;

        //для открытия записи организации в формате JSON
        public void OnOpenCommandExecuted(object path)
        {                     
            var dlg = new OpenFileDialog
            {
                Title = "Открыть файл",
                Filter = "Файл json (*.json)|*.json",
                InitialDirectory = Environment.CurrentDirectory,
                RestoreDirectory = true

            };
            if (dlg.ShowDialog() != true) return;

            var file = dlg.FileName;

            using StreamReader reader = File.OpenText(file);
            var fileText = reader.ReadToEnd(); //Прочитываем до конца             
            GetOrganization = JsonConvert.DeserializeObject<ObservableCollection<Department>>(fileText); //Присваиваем структуру, хранящуюся в файле
        }

        public ICommand SaveCommand { get; } //Команда для открытия организации

        private bool CanSaveCommandExecute(object path)
        {
            string f_text = JsonConvert.SerializeObject(GetOrganization, Formatting.Indented,
                            new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            if (f_text == null || f_text == "[]") return false; //проверяем, что структура не пустая
            return true;
        }

        //для сохранения записи организации в формате JSON
        public async void OnSaveCommandExecutedAsync(object path)
        {
            string f_text = JsonConvert.SerializeObject(GetOrganization, Formatting.Indented,
                             new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            var fileName = path as string;

            if (fileName == null)
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Сохранение файла",
                    Filter = "Файл json (*.json)|*.json",
                    InitialDirectory = Environment.CurrentDirectory,
                    RestoreDirectory = true
                };

                if (dialog.ShowDialog() != true) return;
                fileName = dialog.FileName;
            }

            using var writer = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
            await writer.WriteAsync(f_text).ConfigureAwait(false);
        }
        #endregion

        private static ObservableCollection<Department> _myOrganization = new ObservableCollection<Department>
                { new Department
                    {
                        NameDepartment = $"Top Secret",
                        managerDepartment = new CEO("Vasya", "Pupkin")
                    }
                };

        public ViewModel()
        {
            CreateCommand = new RelayCommand(OnCreateCommandExecuted, CanCreateCommandExecute);
            OpenCommand = new RelayCommand(OnOpenCommandExecuted, CanOpenCommandExecute); 
            SaveCommand = new RelayCommand(OnSaveCommandExecutedAsync, CanSaveCommandExecute);
        }

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
