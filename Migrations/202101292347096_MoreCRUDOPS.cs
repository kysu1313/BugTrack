namespace BugTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreCRUDOPS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "projectName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bugs", "projectName");
        }
    }
}
