namespace BugTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBugSeverityEnum2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bugs", "BugSeverity_Id", "dbo.Severities");
            DropIndex("dbo.Bugs", new[] { "BugSeverity_Id" });
            AddColumn("dbo.Bugs", "BugSeverity", c => c.Int(nullable: false));
            DropColumn("dbo.Bugs", "BugSeverity_Id");
            DropTable("dbo.Severities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Severities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bugs", "BugSeverity_Id", c => c.Int());
            DropColumn("dbo.Bugs", "BugSeverity");
            CreateIndex("dbo.Bugs", "BugSeverity_Id");
            AddForeignKey("dbo.Bugs", "BugSeverity_Id", "dbo.Severities", "Id");
        }
    }
}
