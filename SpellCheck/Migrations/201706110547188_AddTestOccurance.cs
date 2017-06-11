namespace SpellCheck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestOccurance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestOccurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpellTestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpellTests", t => t.SpellTestId, cascadeDelete: true)
                .Index(t => t.SpellTestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestOccurances", "SpellTestId", "dbo.SpellTests");
            DropIndex("dbo.TestOccurances", new[] { "SpellTestId" });
            DropTable("dbo.TestOccurances");
        }
    }
}
