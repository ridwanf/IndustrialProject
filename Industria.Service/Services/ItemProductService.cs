using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core;
using Industrial.Data.Domain;
using Industrial.Repository.Repositories;
using Industrial.Service.ViewModel.Master;

namespace Industrial.Service.Services
{
    public class ItemProductService
    {
        private readonly IItemProductRepository _itemRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ItemProductService(IItemProductRepository itemRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _itemRepository = itemRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public IEnumerable<ItemProductViewModel> GetAll()
        {
            var viewData = new List<ItemProductViewModel>();
            List<ItemProduct> data = _itemRepository.FindAll(a => a.IsActive).ToList();
            Mapper.Map(data, viewData);
            return viewData;
        }

        public ItemProductViewModel FindById(int id)
        {
            var data = new ItemProductViewModel();
            ItemProduct result = _itemRepository.FindBy(a => a.Id == id).First();
            if (result != null)
            {
                Mapper.Map(result, data);
                return data;
            }
            return null;
        }

        public ItemProductViewModel Create(ItemProductViewModel item)
        {
            var data = new ItemProduct();
            Mapper.Map(item, data);
            using (_unitOfWorkFactory.Create())
            {
                _itemRepository.Add(data);
            }

            Mapper.Map(data, item);
            return item;
        }

        public ItemProductViewModel Edit(ItemProductViewModel item)
        {
            var data = new ItemProduct();
            Mapper.Map(item, data);
            using (_unitOfWorkFactory.Create())
            {
                _itemRepository.Update(data);
            }
            Mapper.Map(data, item);
            return item;
        }

        public ItemProductViewModel Delete(int id)
        {
            var item = _itemRepository.FindBy(a => a.Id == id).FirstOrDefault();
            var itemView = new ItemProductViewModel();
            if (item == null)
                return null;
            else
            {
                _itemRepository.SoftDelete(item);
                Mapper.Map(item, itemView);
                return itemView;
            }
        }
    }
}