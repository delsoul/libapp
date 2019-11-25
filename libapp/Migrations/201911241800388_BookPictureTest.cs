namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookPictureTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Picture");
        }
    }
}
