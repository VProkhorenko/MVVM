﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM
{
    public class ApplicationViewModel : INotifyPropertyChanged //модель представления
    {
        private Phone selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }



        // команда добавления нового объекта
        //Команда хранится в свойстве AddCommand и представляет собой объект выше определенного класса RelayCommand. Этот объект в конструкторе принимает действие - делегат Action<object>. Здесь действие представлено в виде лямбда-выражения, которое добавляет в коллекцию Phones новый объект Phone и устанавливает его в качестве выбранного.
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Phone phone = new Phone();
                      Phones.Insert(0, phone);
                      SelectedPhone = phone;
                  }));
            }
        }


        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Phone phone = obj as Phone;
                      if (phone != null)
                      {
                          Phones.Remove(phone);
                      }
                  },
                 (obj) => Phones.Count > 0)); //условие выполнения команды
            }
        }


        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<Phone>
            {
                new Phone { Title="iPhone 7", Company="Apple", Price=56000 },
                new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
                new Phone {Title="Elite x3", Company="HP", Price=56000 },
                new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }





        public class DefaultDialogService : IDialogService
        {
            public string FilePath { get; set; }

            public bool OpenFileDialog()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;
                    return true;
                }
                return false;
            }

            public bool SaveFileDialog()
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    FilePath = saveFileDialog.FileName;
                    return true;
                }
                return false;
            }

            public void ShowMessage(string message)
            {
                MessageBox.Show(message);
            }
        }



    }
}
