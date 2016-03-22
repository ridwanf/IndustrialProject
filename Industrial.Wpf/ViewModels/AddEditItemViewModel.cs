﻿using System;
using System.ComponentModel;
using Core.Common.UI;
using Industrial.Service.Services;
using Industrial.Service.ViewModel.Master;

namespace Industrial.Wpf.ViewModels
{
    public class AddEditItemViewModel : BindableBase
    {
        private readonly IItemProductService _service;
        private bool _editMode;
        private ItemProductModel _itemProduct;

        public AddEditItemViewModel(IItemProductService service, ItemProductModel itemProductModel)
        {
            _service = service;
            _itemProduct = new ItemProductModel()
            {
                Description = itemProductModel.Description,
                Id = itemProductModel.Id,
                Name = itemProductModel.Name,
                Quantity = itemProductModel.Quantity,
                Price = itemProductModel.Price
            };
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        public ItemProductModel ItemProduct
        {
            get { return _itemProduct; }
            set { SetProperty(ref _itemProduct, value); }
        }

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        public event Action Done = delegate { };


        private bool CanSave()
        {
            return !ItemProduct.HasErrors;
        }

        private async void OnSave()
        {
            UpdateItem(ItemProduct, _itemProduct);
            if (EditMode)
            {
                await _service.EditAsync(_itemProduct);
            }
            else
            {
                await _service.CreateAsync(_itemProduct);
            }
        }

        private void UpdateItem(ItemProductModel source, ItemProductModel target)
        {
            target.Quantity = source.Quantity;
            target.Price = source.Price;
            target.Name = source.Name;
            target.Id = source.Id;
        }

        private void CopyItem(ItemProductModel source, ItemProductModel target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.Quantity = source.Quantity;
                target.Price = source.Price;
                target.Name = source.Name;
                target.Id = source.Id;
            }
        }

        public void SetItem(ItemProductModel item)
        {
            _itemProduct = item;
            if (ItemProduct != null) ItemProduct.ErrorsChanged -= RaiseCanExecuteChanged;
            ItemProduct = new ItemProductModel();
            ItemProduct.ErrorsChanged += RaiseCanExecuteChanged;
            CopyItem(item, ItemProduct);
        }

        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void OnCancel()
        {
            Done();
        }
    }
}
