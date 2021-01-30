namespace BugTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'21db60b2-948e-43aa-9ee7-4eadd872b4a8', N'admin@vidly.com', 0, N'AKJn9YSINTPFB3l4V4KoKwPFApc5qGJYl8vAEBlmRYTKgqG/6F2d7UX3JkiyNTI52g==', N'e54f9c98-8a90-4366-be09-ceef11f43740', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2c0ab387-b742-4069-91a2-ac7f72124a5b', N'guest@vidly.com', 0, N'AGP0WZv2qbag0hFyR1MvLBfepzb+AK2ZtYtRLsh21nzwX1oh/B6k5l4U1yjGZx8SeA==', N'9ad1a7d8-c84b-44be-81e4-3500bad55df7', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd1ee34d8-a044-4f43-9a9f-4bb112ef08c2', N'CanManageProjects')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'21db60b2-948e-43aa-9ee7-4eadd872b4a8', N'd1ee34d8-a044-4f43-9a9f-4bb112ef08c2')

            
");
        }
        
        public override void Down()
        {
        }
    }
}
