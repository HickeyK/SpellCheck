namespace SpellCheck.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTaken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestOccurances", "DateTestTaken", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestOccurances", "DateTestTaken");
        }
    }
}
