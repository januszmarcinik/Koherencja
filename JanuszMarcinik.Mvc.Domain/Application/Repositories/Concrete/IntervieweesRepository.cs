using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class IntervieweesRepository : IIntervieweesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Interviewee Create(Interviewee entity)
        {
            context.Interviewees.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public IEnumerable<Interviewee> GetList()
        {
            return context.Interviewees;
        }
    }
}