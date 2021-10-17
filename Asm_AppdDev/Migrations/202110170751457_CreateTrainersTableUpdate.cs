namespace Asm_AppdDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTrainersTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trainers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainers", "Email", c => c.String(nullable: false));
        }
    }
}
