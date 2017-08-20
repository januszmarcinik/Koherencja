using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class AnswersRepository : IAnswersRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Answer GetById(int id)
        {
            return context.Answers.Find(id);
        }

        public IEnumerable<Answer> GetList(int questionId)
        {
            return context.Answers
                .Where(x => x.QuestionId == questionId);
        }

        public Answer Create(Answer entity)
        {
            entity.OrderNumber = context.Answers.Where(x => x.QuestionId == entity.QuestionId).Count() + 1;

            context.Answers.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public Answer Update(Answer entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            context.Answers.Remove(entity);
            context.SaveChanges();
        }

        public List<string> GetDescriptions(int questionId)
        {
            var descriptions = new List<string>();
            var answers = GetList(questionId);

            foreach (var answer in answers)
            {
                if (!string.IsNullOrEmpty(answer.Description))
                {
                    descriptions.Add(answer.Description);
                }
                else
                {
                    descriptions.Add(answer.OrderNumber.ToString());
                }
            }

            return descriptions;
        }
    }
}