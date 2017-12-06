namespace element_backstage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Sex = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Birth = c.DateTime(nullable: false, precision: 0),
                        Address = c.String(maxLength: 500, storeType: "nvarchar"),
                        EidtTime = c.DateTime(nullable: false, precision: 0),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        Del = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.MyEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MyEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Customers");
        }
    }
}
