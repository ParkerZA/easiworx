using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.domain.lib.core.test.Repository
{
    [Migration(500, "Create ContactDetailsView")]
    public class Migration_500 : Migration
    {

        public override void Up()
        {
            Execute.EmbeddedScript(@"my.domain.lib.core.test.DbScripts.ClientDetailsView1.sql");
        }

        public override void Down()
        {
            Execute.Sql("DROP VIEW 'ClientContactView'");
        }
    }
}
