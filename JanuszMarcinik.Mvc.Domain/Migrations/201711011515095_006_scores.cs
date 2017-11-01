namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006_scores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Questionnaire.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        IntervieweeId = c.Int(nullable: false),
                        QuestionnaireId = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        PointsAvailableToGet = c.Int(nullable: false),
                        PointsEarned = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("Questionnaire.Categories", t => t.CategoryId)
                .ForeignKey("Questionnaire.Interviewees", t => t.IntervieweeId, cascadeDelete: true)
                .ForeignKey("Questionnaire.Questionnaires", t => t.QuestionnaireId, cascadeDelete: true)
                .Index(t => t.IntervieweeId)
                .Index(t => t.QuestionnaireId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Questionnaire.Scores", "QuestionnaireId", "Questionnaire.Questionnaires");
            DropForeignKey("Questionnaire.Scores", "IntervieweeId", "Questionnaire.Interviewees");
            DropForeignKey("Questionnaire.Scores", "CategoryId", "Questionnaire.Categories");
            DropIndex("Questionnaire.Scores", new[] { "CategoryId" });
            DropIndex("Questionnaire.Scores", new[] { "QuestionnaireId" });
            DropIndex("Questionnaire.Scores", new[] { "IntervieweeId" });
            DropTable("Questionnaire.Scores");
        }
    }
}
