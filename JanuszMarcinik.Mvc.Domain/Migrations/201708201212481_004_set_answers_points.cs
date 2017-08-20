namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _004_set_answers_points : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [Ankieta].[Questionnaire].[Answers] SET [Points] = [Value]");
        }
        
        public override void Down()
        {
            Sql("UPDATE [Ankieta].[Questionnaire].[Answers] SET [Points] = 0");
        }
    }
}