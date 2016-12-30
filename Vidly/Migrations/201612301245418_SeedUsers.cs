namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'da83b991-6e04-42b8-8505-5d7a047a7c58', N'guest@vidly.com', 0, N'AKV15utbxaO2TEPV0pQpyxArXrDfvJxF8jhOOfrI37PlrJsRDEaZF6nIZXCZOeSNBA==', N'36a7c611-b1c1-4852-a560-cbb216bd0dec', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f669b127-d1a9-4ceb-bcfb-86f4bc941fc5', N'admin@vidly.com', 0, N'AJyJAmP+OgRp0jZmSMPfghL9SloXO+te41LUSY+4YszzgLfP9zP3XeZVdr33O/loLA==', N'4e438d57-b4db-4af2-bd73-18328a1a806d', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                ");
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'050ec9bd-c768-4f70-a6a8-f096bffc408c', N'CanMangeMovie')");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f669b127-d1a9-4ceb-bcfb-86f4bc941fc5', N'050ec9bd-c768-4f70-a6a8-f096bffc408c')");
        }

        public override void Down()
        {
        }
    }
}
