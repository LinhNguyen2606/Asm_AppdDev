namespace Asm_AppdDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTrainersAndTraineesToCoursesTalbe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineesToCourses",
                c => new
                    {
                        TraineeId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TraineeId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.TrainersToCourses",
                c => new
                    {
                        TrainerId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainerId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainersToCourses", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.TrainersToCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TraineesToCourses", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.TraineesToCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TrainersToCourses", new[] { "CourseId" });
            DropIndex("dbo.TrainersToCourses", new[] { "TrainerId" });
            DropIndex("dbo.TraineesToCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineesToCourses", new[] { "TraineeId" });
            DropTable("dbo.TrainersToCourses");
            DropTable("dbo.TraineesToCourses");
        }
    }
}
