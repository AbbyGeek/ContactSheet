namespace ContactList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "primaryPhone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "primaryPhone", c => c.String());
        }
    }
}
