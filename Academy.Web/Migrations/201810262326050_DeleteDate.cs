namespace Academy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "EnrollmentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "EnrollmentDate", c => c.DateTime(nullable: false));
        }
    }
}
