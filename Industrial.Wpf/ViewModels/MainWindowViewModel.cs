﻿using Core.Common.UI;
using Industrial.Service.ViewModel.Master;
using Industrial.Wpf.Infrastructures;
using Microsoft.Practices.Unity;

namespace Industrial.Wpf.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private ItemProductViewModel _itemProductViewModel;
        
        
        public RelayCommand<string> NavCommand { get; private set; }

        private BindableBase _currentViewModel;


        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            _itemProductViewModel = ContainerHelper.Container.Resolve<ItemProductViewModel>();
        }

      


        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        private void OnNav(string destinataion)
        {
            switch (destinataion)
            {
                case "itemProduct":
                    CurrentViewModel = _itemProductViewModel;
                    break;
                default:
                    CurrentViewModel = _itemProductViewModel;
                    break;

            }
        }
    }
}
