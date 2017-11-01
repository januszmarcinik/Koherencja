using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System;
using System.Linq;

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

        public Interviewee GetById(int id)
        {
            return context.Interviewees.Find(id);
        }

        public IEnumerable<Interviewee> GetList(DateTime? dateFrom = null, DateTime? dateTo = null, int? ageId = null, int? sexId = null,
            int? educationId = null, int? martialStatusId = null, int? materialStatusId = null, int? placeOfResidenceId = null, int? seniorityId = null)
        {
            var query = context.Interviewees.AsQueryable();

            if (dateFrom.HasValue)
            {
                query = query.Where(x => x.InterviewDate >= dateFrom);
            }

            if (dateTo.HasValue)
            {
                var dateToIncremented = dateTo.Value.AddDays(1);
                query = query.Where(x => x.InterviewDate <= dateToIncremented);
            }

            if (ageId.HasValue)
            {
                query = query.Where(x => x.AgeId == ageId);
            }

            if (sexId.HasValue)
            {
                query = query.Where(x => x.SexId == sexId);
            }

            if (educationId.HasValue)
            {
                query = query.Where(x => x.EducationId == educationId);
            }

            if (martialStatusId.HasValue)
            {
                query = query.Where(x => x.MartialStatusId == martialStatusId);
            }

            if (materialStatusId.HasValue)
            {
                query = query.Where(x => x.MaterialStatusId == materialStatusId);
            }

            if (placeOfResidenceId.HasValue)
            {
                query = query.Where(x => x.PlaceOfResidenceId == placeOfResidenceId);
            }

            if (seniorityId.HasValue)
            {
                query = query.Where(x => x.SeniorityId == seniorityId);
            }

            return query.AsEnumerable();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            context.Interviewees.Remove(entity);
            context.SaveChanges();
        }
    }
}