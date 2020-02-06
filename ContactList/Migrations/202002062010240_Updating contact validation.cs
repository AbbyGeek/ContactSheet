namespace ContactList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatingcontactvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "lastName", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "company", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "secondaryPhone", c => c.Long());
            AlterColumn("dbo.Contacts", "address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "address", c => c.String());
            AlterColumn("dbo.Contacts", "secondaryPhone", c => c.Long(nullable: false));
            AlterColumn("dbo.Contacts", "email", c => c.String());
            AlterColumn("dbo.Contacts", "company", c => c.String());
            AlterColumn("dbo.Contacts", "lastName", c => c.String());
            AlterColumn("dbo.Contacts", "firstName", c => c.String());
        }
    }
}
