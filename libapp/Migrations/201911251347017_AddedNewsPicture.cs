namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewsPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Picture");
        }
    }
}
