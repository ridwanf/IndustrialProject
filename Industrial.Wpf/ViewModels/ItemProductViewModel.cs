using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Media;
using System.Security.AccessControl;
using Core;
using Core.Common.UI;
using Industrial.Data.Repositories;
using Industrial.Repository.Repositories;
using Industrial.Service.Services;
using Industrial.Service.ViewModel.Master;
using Industrial.Wpf.Infrastructures;

namespace Industrial.Wpf.ViewModels
{
    public class ItemProductViewModel : BindableBase
    {
        public IItemProductService _Service = new ItemProductService();
        private ObservableCollection<ItemProductModel> _items;
        private AddEditItemViewModel _currentItem;


        public RelayCommand NewItem { get; private set; }
        public RelayCommand<ItemProductModel> EditItemCommand { get; private set; }
        public RelayCommand<ItemProductModel> DeleteItemCommand { get; private set; }
        public RelayCommand AddItemCommand { get; private set; }
        public RelayCommand TestAlert { get; private set; }

        public event Action<ItemProductModel> AddCustomerRequested = delegate { };
        public event Action<ItemProductModel> EditCustomerRequested = delegate { };
        public event Action<ItemProductModel> DeleteCustomerRequested = delegate { }; 


        public ItemProductViewModel(IItemProductService service)
        {
            _Service = service;
            EditItemCommand = new RelayCommand<ItemProductModel>(OnEditCommand);
            DeleteItemCommand = new RelayCommand<ItemProductModel>(OnDeleteCommand);
            AddItemCommand = new RelayCommand(OnAddCommand);
        }

        private void OnAddCommand()
        {
            ItemProductModel item = new ItemProductModel();
            CurrentItem = new AddEditItemViewModel(_Service,item);
            CurrentItem.ItemProduct = item;
            CurrentItem.EditMode = false;
        }

        private void OnDeleteCommand(ItemProductModel obj)
        {
            throw new System.NotImplementedException();
        }

        private void OnEditCommand(ItemProductModel obj)
        {
            if (obj!=null)
            {
                CurrentItem = new AddEditItemViewModel(_Service,obj);
                CurrentItem.EditMode = true;
            }
        }


        public ObservableCollection<ItemProductModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items,value); }
        }


        public async void LoadItems()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }

            Items = new ObservableCollection<ItemProductModel>(await _Service.GetAllAsync());
        }

        public AddEditItemViewModel CurrentItem
        {
            get { return _currentItem; }
            set { SetProperty(ref _currentItem,value); }
        }

        //public ItemProductViewModel(ObservableCollection<ItemProductModel> items,
        //    RelayCommand newItem,
        //    RelayCommand editItem, ItemProductService service)
        //{
        //    NewItem = newItem;
        //    EditItem = editItem;

        //    _Service = service;
        //    DeleteItem = new RelayCommand(DeleteSelectedItem, o => o != null);
        //    Items = items;
        //}


        //private void DeleteSelectedItem(object obj)
        //{
        //    if (MessageBox.Show("Are you sure you want to delete this Match record?",
        //        "Delete Match",
        //            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        Items.Remove(selectedItem);
        //        SelectedItem = null;
        //    }
        //}
    }
}
