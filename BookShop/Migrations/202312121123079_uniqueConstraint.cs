namespace BookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueConstraint : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Classifications", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Classifications", new[] { "Name" });
        }
    }
}
