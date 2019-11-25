namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewsPictureBookPublisher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Publisher", c => c.String());
            AddColumn("dbo.News", "Picture", c => c.Binary());
            DropColumn("dbo.News", "ContentPreview");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "ContentPreview", c => c.String());
            DropColumn("dbo.News", "Picture");
            DropColumn("dbo.Books", "Publisher");
        }
    }
}
