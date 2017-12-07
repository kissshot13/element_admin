namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Password = c.String(maxLength: 100, storeType: "nvarchar"),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        Del = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
        }
    }
}
