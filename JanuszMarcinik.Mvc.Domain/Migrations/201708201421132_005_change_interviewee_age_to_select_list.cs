namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005_change_interviewee_age_to_select_list : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE [Ankieta].[Questionnaire].[Results]");
            Sql("DELETE FROM [Ankieta].[Questionnaire].[Interviewees]");

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
