namespace SpellCheck.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpellingId = c.Int(nullable: false),
                        FinalAnswer = c.String(),
                        NumberOfTries = c.Int(nullable: false),
                        AnswerStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestAnswers");
        }
    }
}
