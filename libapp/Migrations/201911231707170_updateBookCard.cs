namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateBookCard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Picture", c => c.Binary());
        }
    }
}
