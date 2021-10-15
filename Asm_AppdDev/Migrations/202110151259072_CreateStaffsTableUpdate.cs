namespace Asm_AppdDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStaffsTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Staffs", new[] { "StaffId" });
            DropPrimaryKey("dbo.Staffs");
            AddColumn("dbo.Staffs", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Staffs", "StaffId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Staffs", "Id");
            CreateIndex("dbo.Staffs", "StaffId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staffs", new[] { "StaffId" });
            DropPrimaryKey("dbo.Staffs");
            AlterColumn("dbo.Staffs", "StaffId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Staffs", "Id");
            AddPrimaryKey("dbo.Staffs", "StaffId");
            CreateIndex("dbo.Staffs", "StaffId");
        }
    }
}
