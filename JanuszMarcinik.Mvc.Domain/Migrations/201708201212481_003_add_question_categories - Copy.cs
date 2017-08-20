namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003_add_question_categories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Questionnaire.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        QuestionnaireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("Questionnaire.Questionnaires", t => t.QuestionnaireId, cascadeDelete: true)
                .Index(t => t.QuestionnaireId);
            
            AddColumn("Questionnaire.Answers", "Points", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("Questionnaire.Questions", "CategoryId", c => c.Int());
            CreateIndex("Questionnaire.Questions", "CategoryId");
            AddForeignKey("Questionnaire.Questions", "CategoryId", "Questionnaire.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("Questionnaire.Questions", "CategoryId", "Questionnaire.Categories");
            DropForeignKey("Questionnaire.Categories", "QuestionnaireId", "Questionnaire.Questionnaires");
            DropIndex("Questionnaire.Categories", new[] { "QuestionnaireId" });
            DropIndex("Questionnaire.Questions", new[] { "CategoryId" });
            DropColumn("Questionnaire.Questions", "CategoryId");
            DropColumn("Questionnaire.Answers", "Points");
            DropTable("Questionnaire.Categories");
        }
    }
}
