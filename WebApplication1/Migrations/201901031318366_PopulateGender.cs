namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGender : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genders (Name) Values ('Male')");
            Sql("Insert into Genders (Name) Values ('Female')");
            Sql("Insert into Genders (Name) Values ('Other')");
        }
        
        public override void Down()
        {
        }
    }
}
