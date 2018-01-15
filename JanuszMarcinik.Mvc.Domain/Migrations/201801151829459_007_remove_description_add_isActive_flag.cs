namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _007_remove_description_add_isActive_flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("Questionnaire.Questionnaires", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            DropColumn("Questionnaire.Questionnaires", "Description");
        }
        
        public override void Down()
        {
            AddColumn("Questionnaire.Questionnaires", "Description", c => c.String());
            DropColumn("Questionnaire.Questionnaires", "IsActive");
        }
    }
}
