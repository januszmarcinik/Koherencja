﻿using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Data.Entity;

namespace JanuszMarcinik.Mvc.Domain.Application.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("JanuszMarcinikConnection") { }

        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Interviewee> Interviewees { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<BaseDictionary> BaseDictionaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Questionnaires
            // Questionnaire -> Questions
            modelBuilder.Entity<Questionnaire>()
                        .HasMany<Question>(s => s.Questions)
                        .WithRequired(s => s.Questionnaire)
                        .HasForeignKey(s => s.QuestionnaireId);

            // Questionnaire -> Results
            modelBuilder.Entity<Questionnaire>()
                        .HasMany<Result>(s => s.Results)
                        .WithRequired(s => s.Questionnaire)
                        .HasForeignKey(s => s.QuestionnaireId)
                        .WillCascadeOnDelete(false);

            // Question -> Answers
            modelBuilder.Entity<Question>()
                        .HasMany<Answer>(s => s.Answers)
                        .WithRequired(s => s.Question)
                        .HasForeignKey(s => s.QuestionId);

            // Question -> Results
            modelBuilder.Entity<Question>()
                        .HasMany<Result>(s => s.Results)
                        .WithRequired(s => s.Question)
                        .HasForeignKey(s => s.QuestionId);

            // Answer -> Results
            modelBuilder.Entity<Answer>()
                        .HasMany<Result>(s => s.Results)
                        .WithRequired(s => s.Answer)
                        .HasForeignKey(s => s.AnswerId)
                        .WillCascadeOnDelete(false);

            // Interviewee -> Results
            modelBuilder.Entity<Interviewee>()
                        .HasMany<Result>(s => s.Results)
                        .WithRequired(s => s.Interviewee)
                        .HasForeignKey(s => s.IntervieweeId);

            // BaseDictionary -> Interviewees (Sexes)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.Sexes)
                        .WithRequired(s => s.Sex)
                        .HasForeignKey(s => s.SexId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (Seniorities)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.Seniorities)
                        .WithRequired(s => s.Seniority)
                        .HasForeignKey(s => s.SeniorityId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (PlaceOfResidences)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.PlaceOfResidences)
                        .WithRequired(s => s.PlaceOfResidence)
                        .HasForeignKey(s => s.PlaceOfResidenceId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}