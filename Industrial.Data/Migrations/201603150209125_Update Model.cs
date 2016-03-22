namespace Industrial.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Item", newName: "ItemProduct");
            CreateTable(
                "dbo.Bank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BranchId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemBOM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseUOM = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegisterDate = c.DateTime(nullable: false),
                        ContractDate = c.DateTime(nullable: false),
                        PicName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumber2 = c.String(),
                        FaxNumber = c.String(),
                        FaxNumber2 = c.String(),
                        Email = c.String(),
                        Email2 = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ItemProduct", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.ItemProduct", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemProduct", "CreatedDate");
            DropColumn("dbo.ItemProduct", "Quantity");
            DropTable("dbo.Supplier");
            DropTable("dbo.ItemBOM");
            DropTable("dbo.Branch");
            DropTable("dbo.Bank");
            RenameTable(name: "dbo.ItemProduct", newName: "Item");
        }
    }
}
