namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Booking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Client", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Client");
        }
    }
}
