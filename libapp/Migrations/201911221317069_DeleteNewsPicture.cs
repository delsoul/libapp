namespace libapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNewsPicture : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Picture", c => c.Binary());
        }
    }
}
