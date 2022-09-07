using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.domain.test
{
   

    [Migration(100, "Seeding tables")]
    public class InitialSeed : Migration
    {

        public override void Up()
        {
           //Add seed data to static tables

        }

        public override void Down()
        {
            //Remove seed data from static tables
        }
    }
}
