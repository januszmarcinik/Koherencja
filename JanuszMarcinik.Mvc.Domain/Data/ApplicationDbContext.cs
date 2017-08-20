using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace JanuszMarcinik.Mvc.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("JanuszMarcinikConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Interviewee> Interviewees { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BaseDictionary> BaseDictionaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Questionnaires
            // Questionnaire -> Questions
            modelBuilder.Entity<Questionnaire>()
                        .HasMany<Question>(s => s.Questions)
                        .WithRequired(s => s.Questionnaire)
                        .HasForeignKey(s => s.QuestionnaireId);

            // Questionnaire -> Categories
            modelBuilder.Entity<Questionnaire>()
                        .HasMany<Category>(s => s.Categories)
                        .WithRequired(s => s.Questionnaire)
                        .HasForeignKey(s => s.QuestionnaireId);

            // Questionnaire -> Results
            modelBuilder.Entity<Questionnaire>()
                        .HasMany<Result>(s => s.Results)
                        .WithRequired(s => s.Questionnaire)
                        .HasForeignKey(s => s.QuestionnaireId)
                        .WillCascadeOnDelete(false);

            // Category -> Questions
            modelBuilder.Entity<Category>()
                        .HasMany<Question>(s => s.Questions)
                        .WithOptional(s => s.Category)
                        .HasForeignKey(s => s.CategoryId)
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

            // BaseDictionary -> Interviewees (Educations)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.Educations)
                        .WithRequired(s => s.Education)
                        .HasForeignKey(s => s.EducationId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (PlaceOfResidences)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.PlaceOfResidences)
                        .WithRequired(s => s.PlaceOfResidence)
                        .HasForeignKey(s => s.PlaceOfResidenceId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (MartialStatuses)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.MartialStatuses)
                        .WithRequired(s => s.MartialStatus)
                        .HasForeignKey(s => s.MartialStatusId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (MaterialStatuses)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.MaterialStatuses)
                        .WithRequired(s => s.MaterialStatus)
                        .HasForeignKey(s => s.MaterialStatusId)
                        .WillCascadeOnDelete(false);

            // BaseDictionary -> Interviewees (Ages)
            modelBuilder.Entity<BaseDictionary>()
                        .HasMany<Interviewee>(s => s.Ages)
                        .WithRequired(s => s.Age)
                        .HasForeignKey(s => s.AgeId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}