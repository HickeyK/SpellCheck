namespace SpellCheck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Spellings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                        ContextSentence = c.String(),
                        SpellTest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpellTests", t => t.SpellTest_Id)
                .Index(t => t.SpellTest_Id);
            
            CreateTable(
                "dbo.SpellTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spellings", "SpellTest_Id", "dbo.SpellTests");
            DropIndex("dbo.Spellings", new[] { "SpellTest_Id" });
            DropTable("dbo.SpellTests");
            DropTable("dbo.Spellings");
        }
    }
}
