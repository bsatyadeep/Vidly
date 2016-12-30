namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopupateMembershipTypeandGeners : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes(Id,Name,SignUpFee,DurationInMonths,DiscountRate) values(1,'Pay as You Go',0,0,0)");
            Sql("INSERT INTO MembershipTypes(Id,Name,SignUpFee,DurationInMonths,DiscountRate) values(2,'Monthly',10,1,10)");
            Sql("INSERT INTO MembershipTypes(Id,Name,SignUpFee,DurationInMonths,DiscountRate) values(3,'Quaterly',20,3,15)");
            Sql("INSERT INTO MembershipTypes(Id,Name,SignUpFee,DurationInMonths,DiscountRate) values(4,'Yearly',300,12,30)");

            Sql("INSERT INTO Genres(Name) values('Comedy')");
            Sql("INSERT INTO Genres(Name) values('Action')");
            Sql("INSERT INTO Genres(Name) values('Family')");
            Sql("INSERT INTO Genres(Name) values('Romance')");
            Sql("INSERT INTO Genres(Name) values('Horror')");
            Sql("INSERT INTO Genres(Name) values('Adventure')");
            Sql("INSERT INTO Genres(Name) values('Thirilar')");
        }

        public override void Down()
        {
        }
    }
}
