namespace BugTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingProjectFromBugClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bugs", "projectId", "dbo.Projects");
            DropIndex("dbo.Bugs", new[] { "projectId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Bugs", "projectId");
            AddForeignKey("dbo.Bugs", "projectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
