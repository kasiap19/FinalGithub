namespace ExamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TaskId);
            
            AddColumn("dbo.Students", "Name", c => c.String());
            AlterColumn("dbo.Students", "Username", c => c.String());
            AlterColumn("dbo.Students", "Password", c => c.String());
            DropColumn("dbo.Students", "UserType");
            DropColumn("dbo.Students", "Email");
            DropTable("dbo.Teachers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        UserType = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            AddColumn("dbo.Students", "Email", c => c.String());
            AddColumn("dbo.Students", "UserType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Status", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Status", "StudentId", "dbo.Students");
            DropIndex("dbo.Status", new[] { "TaskId" });
            DropIndex("dbo.Status", new[] { "StudentId" });
            AlterColumn("dbo.Students", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Students", "Username", c => c.String(nullable: false));
            DropColumn("dbo.Students", "Name");
            DropTable("dbo.Tasks");
            DropTable("dbo.Status");
        }
    }
}
