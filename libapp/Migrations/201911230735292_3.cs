namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.AddUsers", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.AddUsers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.AddUsers", "Role", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddUsers", "Role", c => c.String());
            AlterColumn("dbo.AddUsers", "Password", c => c.String());
            AlterColumn("dbo.AddUsers", "UserName", c => c.String());
            AlterColumn("dbo.AddUsers", "Email", c => c.String());
        }
    }
}
