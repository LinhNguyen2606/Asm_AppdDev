namespace Asm_AppdDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTrainersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        Specialty = c.String(),
                        TrainerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainers", new[] { "TrainerId" });
            DropTable("dbo.Trainers");
        }
    }
}
