namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004_change_interviewee_age_to_select_list : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [Questionnaire].[Answers] SET [Points] = [Value]");

            Sql("TRUNCATE TABLE [Questionnaire].[Results]");
            Sql("DELETE FROM [Questionnaire].[Interviewees]");

            AddColumn("Questionnaire.Interviewees", "AgeId", c => c.Int(nullable: false));
            CreateIndex("Questionnaire.Interviewees", "AgeId");
            AddForeignKey("Questionnaire.Interviewees", "AgeId", "Dictionaries.BaseDictionaries", "BaseDictionaryId");
            DropColumn("Questionnaire.Interviewees", "Age");
        }
        
        public override void Down()
        {
            AddColumn("Questionnaire.Interviewees", "Age", c => c.Int(nullable: false));
            DropForeignKey("Questionnaire.Interviewees", "AgeId", "Dictionaries.BaseDictionaries");
            DropIndex("Questionnaire.Interviewees", new[] { "AgeId" });
            DropColumn("Questionnaire.Interviewees", "AgeId");
        }
    }
}
