namespace SpellCheck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestIdToTestAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestAnswers", "TestId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestAnswers", "TestId");
        }
    }
}
