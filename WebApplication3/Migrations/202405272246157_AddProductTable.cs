namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FarmerId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Category = c.String(nullable: false, maxLength: 50),
                        ProductionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FarmerId, cascadeDelete: true)
                .Index(t => t.FarmerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "FarmerId", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "FarmerId" });
            DropTable("dbo.Products");
        }
    }
}
