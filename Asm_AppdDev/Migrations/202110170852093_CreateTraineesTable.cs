namespace Asm_AppdDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTraineesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        Education = c.String(nullable: false),
                        TraineeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "TraineeId" });
            DropTable("dbo.Trainees");
        }
    }
}
