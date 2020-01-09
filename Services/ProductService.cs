using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Domain.Services.Communication;
using API.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMemoryCache _cache;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> ListAsync(int? categoryId)
        {
            // Here I list products from cache if they exist, but now the data can vary according to the category ID.
            // I have to compose a cache to avoid returning wrong data.
            var cacheKey = GetCacheKeyForCategoryId(categoryId);

            var products = await _cache.GetOrCreateAsync(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepository.ListAsync(categoryId);
            });

            return products;
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                /*
                 Notice here we have to check if the category ID is valid before adding the product, to avoid errors.
                 You can create a method into the CategoryService class to return the category and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                    return new ProductResponse("Invalid category.");

                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");

            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new ProductResponse("Invalid category.");

            existingProduct.Name = product.Name;
            existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.CategoryId = product.CategoryId;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }

        private string GetCacheKeyForCategoryId(int? categoryId)
        {
            var key = CacheKeys.ProductsList.ToString();
            if (categoryId.HasValue && categoryId > 0) key = string.Concat(key, categoryId.Value);

            return key;
        }
    }
}