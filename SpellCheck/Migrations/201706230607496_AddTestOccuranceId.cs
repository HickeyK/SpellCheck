namespace SpellCheck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestOccuranceId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestAnswers", "SpellTestId", c => c.Int(nullable: false));
            AddColumn("dbo.TestAnswers", "TestOccuranceId", c => c.Int(nullable: false));
            DropColumn("dbo.TestAnswers", "TestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestAnswers", "TestId", c => c.Int(nullable: false));
            DropColumn("dbo.TestAnswers", "TestOccuranceId");
            DropColumn("dbo.TestAnswers", "SpellTestId");
        }
    }
}
