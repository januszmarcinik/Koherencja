namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _008_add_workplace_dictionary : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Questionnaire.Interviewees");
            AddColumn("Questionnaire.Interviewees", "WorkplaceId", c => c.Int(nullable: false));
            CreateIndex("Questionnaire.Interviewees", "WorkplaceId");
            AddForeignKey("Questionnaire.Interviewees", "WorkplaceId", "Dictionaries.BaseDictionaries", "BaseDictionaryId");
        }
        
        public override void Down()
        {
            DropForeignKey("Questionnaire.Interviewees", "WorkplaceId", "Dictionaries.BaseDictionaries");
            DropIndex("Questionnaire.Interviewees", new[] { "WorkplaceId" });
            DropColumn("Questionnaire.Interviewees", "WorkplaceId");
        }
    }
}
