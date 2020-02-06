namespace ContactList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "primaryPhone", c => c.String());
            AlterColumn("dbo.Contacts", "secondaryPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "secondaryPhone", c => c.Long());
            AlterColumn("dbo.Contacts", "primaryPhone", c => c.Long(nullable: false));
        }
    }
}
