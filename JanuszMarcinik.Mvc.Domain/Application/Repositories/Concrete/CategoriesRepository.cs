using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Category GetById(long id)
        {
            return context.Categories.Find(id);
        }

        public IEnumerable<Category> GetList()
        {
            return context.Categories;
        }

        public Category Create(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public Category Update(Category entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            context.Categories.Remove(entity);
            context.SaveChanges();
        }
    }
}