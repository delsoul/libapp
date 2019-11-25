namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            AddColumn("dbo.Bookings", "User", c => c.String());
            DropColumn("dbo.Bookings", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Bookings", "User");
            CreateIndex("dbo.Bookings", "User_Id");
            AddForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
