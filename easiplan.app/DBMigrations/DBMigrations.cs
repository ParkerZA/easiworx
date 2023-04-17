using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.DBMigrations
{

    /// <summary>
    /// Migrate InitialAmount to CurrentValue
    /// </summary>
    [Migration(400, "CurrentAmount to InitialAmount")]
    public class _2017JAN_400 : Migration
    {
      
        public override void Up()
        {
            Execute.Sql("update retirement set CurrentAmount = InitialAmount");
            Execute.Sql("update retirement set InitialAmount=0");

        }

        public override void Down()
        {
            Execute.Sql("update retirement set InitialAmount = CurrentAmount");
            Execute.Sql("update retirement set CurrentAmount=0");
        }
    }
    [Migration(401, "CurrentAmount to InitialAmount")]
    public class _2017JAN_401 : Migration
    {

        public override void Up()
        {
            Execute.Sql("update fund set CurrentAmount = InitialAmount");
            Execute.Sql("update fund set InitialAmount=0");

        }

        public override void Down()
        {
            Execute.Sql("update fund set InitialAmount = CurrentAmount");
            Execute.Sql("update fund set CurrentAmount=0");
        }
    }
    [Migration(402, "CurrentAmount to InitialAmount")]
    public class _2017JAN_402 : Migration
    {

        public override void Up()
        {
            Execute.Sql("update life set CurrentAmount = InitialAmount");
            Execute.Sql("update life set InitialAmount=0");

            Execute.Sql("update medical set CurrentAmount = InitialAmount");
            Execute.Sql("update medical set InitialAmount=0");

            Execute.Sql("update education set CurrentAmount = InitialAmount");
            Execute.Sql("update education set InitialAmount=0");

            Execute.Sql("update investment set CurrentAmount = InitialAmount");
            Execute.Sql("update investment set InitialAmount=0");

            Execute.Sql("update incomeasset set CurrentAmount = InitialAmount");
            Execute.Sql("update incomeasset set InitialAmount=0");

        }

        public override void Down()
        {
            Execute.Sql("update life set InitialAmount = CurrentAmount");
            Execute.Sql("update life set CurrentAmount=0");

            Execute.Sql("update medical set InitialAmount = CurrentAmount");
            Execute.Sql("update medical set CurrentAmount=0");

            Execute.Sql("update education set InitialAmount = CurrentAmount");
            Execute.Sql("update education set CurrentAmount=0");

            Execute.Sql("update investment set InitialAmount = CurrentAmount");
            Execute.Sql("update investment set CurrentAmount=0");

            Execute.Sql("update incomeasset set InitialAmount = CurrentAmount");
            Execute.Sql("update incomeasset set CurrentAmount=0");
        }
    }

    [Migration(404, "Status to String")]
    public class _2017APR_404 : Migration
    {

        public override void Up()
        {

            Alter.Table("have").AlterColumn("Status").AsAnsiString().Nullable();
        }

        public override void Down()
        {
            Alter.Table("have").AlterColumn("Status").AsInt32().Nullable();
        }
    }

    [Migration(405, "Status to String")]
    public class _2017APR_405 : Migration
    {

        public override void Up()
        {

            Alter.Table("additionalinfo").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("addressdetail").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("asset").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("bankdetail").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("benefit").AlterColumn("Status").AsAnsiString().Nullable();
           // Alter.Table("client").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientadditionalinfo").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientaddress").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientassets").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientbank").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientchecklist").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientcontacts").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientdependent").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientdependents").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientdetails").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientexpenses").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientfee").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientfees").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientfna").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientfnaeducation").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientincomes").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientinstructions").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientliabilities").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientlockstatus").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientmeetings").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientnotes").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("clientportfolio").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("company").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("contactdetail").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("dbversion").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("documenttemplate").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("education").AlterColumn("Status").AsAnsiString().Nullable();
            //Alter.Table("educationneed").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("emailtemplate").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("estate").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("expense").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("fund").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("have").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("income").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("incomeasset").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("instruction").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("investment").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("liability").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("life").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lifeinsurer").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lifeproduct").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lifeproviders").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lisp").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lispfund").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("lispproviders").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("medical").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("medicalaid").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("medicalplan").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("medicalproviders").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("need").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("note").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("provider").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("retirement").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("sysconfig").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("user").AlterColumn("Status").AsAnsiString().Nullable();
            Alter.Table("want").AlterColumn("Status").AsAnsiString().Nullable();
         

        }

        public override void Down()
        {
            Alter.Table("have").AlterColumn("Status").AsInt32().Nullable();
        }
    }

    [Migration(406, "Update Table EducationNeed")]
    public class _2017AUG_406 : Migration
    {

        public override void Up()
        {

            //Delete.Column("NeedType")
            //    .Column("Type")
            //    .Column("Description")
            //    .Column("InitialAmount")
            //    .Column("CurrentAmount")
            //    .Column("GrowthPercentage")
            //    .Column("MonthlyContribution")
            //    .Column("EscalationPercentage")
            //    .Column("FutureAmount")
            //    .Column("InvestmentAge")
            //    .Column("Periods")
            //    .Column("InflationPercentage")
            //    .Column("ReferenceNo")
            //    .Column("ReferenceId")
            //    .Column("InstructionId")
            //    .Column("Status")
            //    .Column("FundsSplitPerc")
            //    .Column("IsImplemented")
            //    .Column("IsCancelled")
            //    .Column("CreateDate")
            //    .Column("UpdateDate")
            //     .Column("UpdateBy")
            //    .FromTable("educationneed");

        }

        public override void Down()
        {
           
        }
    }

    [Migration(407, "Change size of comments field in Instruction table")]
    public class _2017OCT_407 : Migration
    {

        public override void Up()
        {

            Alter.Table("instruction").AlterColumn("Type").AsString(4000).Nullable();
            Alter.Table("instruction").AlterColumn("Comment").AsString(4000).Nullable();

        }

        public override void Down()
        {

        }
    }

    [Migration(408, "Create ContactDetailsView")]
    public class _20211222_408 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.ClientDetailsView1.sql");
        }

        public override void Down()
        {
           
        }
    }

    [Migration(409, "Update ContactDetailsView")]
    public class _20211222_409 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.ClientDetailsView1.sql");
        }

        public override void Down()
        {
            //Execute.Sql("DROP VIEW 'ClientContactView'");
        }
    }

    [Migration(410, "Update ContactDetailsView")]
    public class _20211222_410 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.ClientDetailsView1.sql");
        }

        public override void Down()
        {
            //Execute.Sql("DROP VIEW 'ClientContactView'");
        }
    }

    [Migration(501, "Update ContactDetailsView")]
    public class _20211222_501 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.ClientDetailsView1.sql");
        }

        public override void Down()
        {
           // Execute.Sql("DROP VIEW 'ClientContactView'");
        }
    }

    [Migration(502, "Update ContactDetailsView")]
    public class _20211222_502 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.ClientDetailsView1.sql");
        }

        public override void Down()
        {
            // Execute.Sql("DROP VIEW 'ClientContactView'");
        }
    }

    [Migration(503, "Update clientretirementportfolio_view")]
    public class _20230417_503 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"DBMigrations.clientretirementportfolio_view.sql");
        }

        public override void Down()
        {
            
        }
    }
}
