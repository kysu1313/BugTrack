namespace BugTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectIdToBugClass : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Bugs", name: "project_Id", newName: "projectId");
            RenameIndex(table: "dbo.Bugs", name: "IX_project_Id", newName: "IX_projectId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Bugs", name: "IX_projectId", newName: "IX_project_Id");
            RenameColumn(table: "dbo.Bugs", name: "projectId", newName: "project_Id");
        }
    }
}
