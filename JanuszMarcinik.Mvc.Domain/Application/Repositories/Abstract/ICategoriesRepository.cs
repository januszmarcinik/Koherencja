using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface ICategoriesRepository
    {
        Category GetById(long id);
        IEnumerable<Category> GetList();
        Category Create(Category entity);
        Category Update(Category entity);
        void Delete(long id);
    }
}