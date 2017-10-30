namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _005_questionnaire_key_type : DbMigration
    {
        public override void Up()
        {
            AddColumn("Questionnaire.Questionnaires", "KeyType", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("Questionnaire.Questionnaires", "KeyType");
        }
    }
}