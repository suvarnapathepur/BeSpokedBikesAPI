﻿using BeSpokedBikesAPI.Model;

namespace BeSpokedBikesAPI.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
