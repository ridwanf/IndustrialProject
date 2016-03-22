using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using Industrial.Data.Domain;
using Industrial.Repository.Repositories;
using Industrial.Service.Mappers.Master;
using Industrial.Service.ViewModel.Master;

namespace Industrial.Service.Services
{
    public class ItemProductService : IItemProductService
    {
        MainDataContext context = new MainDataContext();
        private readonly IItemProductRepository _itemRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ItemProductService(IItemProductRepository itemRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _itemRepository = itemRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public ItemProductService()
        {

        }


        public Task<List<ItemProductModel>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public List<ItemProductModel> GetAll()
        {
            List<ItemProduct> data = _itemRepository.FindAll(a => a.IsActive).ToList();

            return data.ConvertToListModel().ToList();
        }


        public Task<ItemProductModel> FindByIdAsync(int id)
        {
            return Task.Run(() => FindById(id));
        }
        public ItemProductModel FindById(int id)
        {
            var data = new ItemProductModel();
            ItemProduct result = _itemRepository.FindBy(a => a.Id == id).First();
            if (result != null)
            {
                Mapper.Map(result, data);
                return data;
            }
            return null;
        }

        public async Task<ItemProductModel> CreateAsync(ItemProductModel item)
        {
            await Task.Run(() => Create(item));
            return item;
        }

        private ItemProductModel Create(ItemProductModel item)
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

        public async Task<ItemProductModel> EditAsync(ItemProductModel item)
        {
            await Task.Run(() => Edit(item));
            return item;
        }

        public ItemProductModel Edit(ItemProductModel item)
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

        public async Task<ItemProductModel> DeleteAsync(int id)
        {
            var item = await Task.Run(() => Delete(id));
            return item;
        }

        public ItemProductModel Delete(int id)
        {
            var item = _itemRepository.FindBy(a => a.Id == id).FirstOrDefault();
            var itemView = new ItemProductModel();
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