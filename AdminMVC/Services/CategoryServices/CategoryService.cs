using AdminMVC.Contracts;
using AdminMVC.ViewModels.CategoryViewModels;
using Data.Context;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMVC.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Color = model.Color,
                IconCssClass = model.IconCssClass,
                CreatedAt = DateTime.Now
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Color = category.Color,
                    IconCssClass = category.IconCssClass
                }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                IconCssClass = category.IconCssClass
            };

            return viewModel;
        }

        public async Task UpdateCategoryAsync(CategoryViewModel model)
        {
            var category = await _dbContext.Categories.FindAsync(model.Id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            category.Name = model.Name;
            category.Color = model.Color;
            category.IconCssClass = model.IconCssClass;

            await _dbContext.SaveChangesAsync();
        }

        Task ICategoryService.DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
