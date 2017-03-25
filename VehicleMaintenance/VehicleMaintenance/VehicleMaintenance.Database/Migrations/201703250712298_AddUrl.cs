namespace VehicleMaintenance.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvalidAssociation",
                c => new
                    {
                        InvalidAssociationId = c.Int(nullable: false, identity: true),
                        VehicleTypeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvalidAssociationId)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleType", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.VehicleType",
                c => new
                    {
                        VehicleTypeId = c.Int(nullable: false, identity: true),
                        VehicleTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CreatedOrUpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Make = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        OdometerReading = c.Int(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.VehicleType", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "VehicleId", "dbo.Vehicle");
            DropForeignKey("dbo.Vehicle", "VehicleTypeId", "dbo.VehicleType");
            DropForeignKey("dbo.Schedule", "TaskId", "dbo.Task");
            DropForeignKey("dbo.InvalidAssociation", "VehicleTypeId", "dbo.VehicleType");
            DropForeignKey("dbo.InvalidAssociation", "TaskId", "dbo.Task");
            DropIndex("dbo.Vehicle", new[] { "VehicleTypeId" });
            DropIndex("dbo.Schedule", new[] { "TaskId" });
            DropIndex("dbo.Schedule", new[] { "VehicleId" });
            DropIndex("dbo.InvalidAssociation", new[] { "TaskId" });
            DropIndex("dbo.InvalidAssociation", new[] { "VehicleTypeId" });
            DropTable("dbo.Vehicle");
            DropTable("dbo.Schedule");
            DropTable("dbo.VehicleType");
            DropTable("dbo.Task");
            DropTable("dbo.InvalidAssociation");
        }
    }
}
