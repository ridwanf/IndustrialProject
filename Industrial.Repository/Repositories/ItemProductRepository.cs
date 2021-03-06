﻿using Industrial.Data.Domain;
using Industrial.Data.Repositories;

namespace Industrial.Repository.Repositories
{
    public class ItemProductRepository : BaseRepository<ItemProduct>, IItemProductRepository
    {
        public void SoftDelete(ItemProduct item)
        {
            item.IsActive = false;
            Update(item);
        }
    }
}