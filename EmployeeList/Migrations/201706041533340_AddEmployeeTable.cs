namespace EmployeeList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "DateInPost", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "DateInPost", c => c.DateTime());
            AlterColumn("dbo.Employees", "Birthday", c => c.DateTime());
        }
    }
}
