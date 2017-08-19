namespace JanuszMarcinik.Mvc.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002_alter_questionnaires_entity : DbMigration
    {
        public override void Up()
        {
            DropColumn("Questionnaire.Questionnaires", "EditDisable");
            DropColumn("Questionnaire.Questionnaires", "Active");
        }
        
        public override void Down()
        {
            AddColumn("Questionnaire.Questionnaires", "Active", c => c.Boolean(nullable: false));
            AddColumn("Questionnaire.Questionnaires", "EditDisable", c => c.Boolean(nullable: false));
        }
    }
}
