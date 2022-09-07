using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.estate.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Registry;
using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Finx.domain.test
{
    [TestClass]
    public class EstateAnalysisTest
    {
        IGenericRepository _ClientRepository;
        IGenericRepository _EstateAnalysisRepository;
        public EstateAnalysisTest()
        {
            //Set Current Culture
            CultureInfo CInfo = new CultureInfo("en-ZA", true);
            CInfo.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
            CInfo.DateTimeFormat.LongDatePattern = "dd-MMM-yyyy hh:mm:ss";
            CInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            CInfo.NumberFormat.CurrencyDecimalDigits = 2;
            CInfo.NumberFormat.NaNSymbol = "0";
            CInfo.NumberFormat.NumberDecimalSeparator = ".";
            CInfo.NumberFormat.PercentDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = CInfo;
            Thread.CurrentThread.CurrentUICulture = CInfo;

            var connectionInfo1 = new MySqlConnectionInfo(ConnectionTypes.MySQL)
            {
                ServerName = "easiplan.southafricanorth.cloudapp.azure.com",
                DbName = "easiworx_test",
                DbSchema = "easiworx_test",
                PortNumber = "3306",
                UserName = "easiworx_test",
                UserPwd = "e@s1worx_T3st",
                AssemblyType = typeof(DomainServices),
                MappingNamespace = "easiplan.domain.Entities"
            };

            _ClientRepository = new MySqlRepository(connectionInfo1) { UserName = "Test" };


            //var connectionInfo = new MySqlConnectionInfo(ConnectionTypes.MySQL)
            //{
            //    ServerName = "localhost",
            //    DbName = "easiworx_app",
            //    DbSchema = "easiworx_app",
            //    PortNumber = "3306",
            //    UserName = "skywalk",
            //    UserPwd = "s83Mw3E3",
            //    AssemblyType = typeof(EstateAnalysis),
            //    MappingNamespace = "easiplan.domain.estate.Entities"
            //};

            var connectionInfo2 = new MySqlConnectionInfo(ConnectionTypes.MySQL)
            {
                ServerName = "easiplan.southafricanorth.cloudapp.azure.com",
                DbName = "easiworx_test",
                DbSchema = "easiworx_test",
                PortNumber = "3306",
                UserName = "easiworx_test",
                UserPwd = "e@s1worx_T3st",
                AssemblyType = typeof(EstateAnalysis),
                MappingNamespace = "easiplan.domain.estate.Entities"
            };

            _EstateAnalysisRepository = new MySqlRepository(connectionInfo2) { UserName = "Test" };

            _EstateAnalysisRepository.PreLoadEvent += Repository_PreLoadEvent;
            _EstateAnalysisRepository.PostLoadEvent += Repository_PostLoadEvent;
            _EstateAnalysisRepository.PreUpdateEvent += Repository_PreUpdateEvent;
            _EstateAnalysisRepository.PreInsertEvent += Repository_PreInsertEvent;

        }

        private void ConfigureRepos()
        {
            
            _ClientRepository.Configure();

           // _EstateAnalysisRepository.Configure();

        }
        [TestMethod]
        public void EstateDomain_Can_Create_SchemaTables()
        {
            try
            {
                _EstateAnalysisRepository.Configure(true,true);

                Assert.IsTrue(_EstateAnalysisRepository.IsConfigured == true && _EstateAnalysisRepository.IsInError==false);
            }
            catch(Exception x)
            {
                
                Assert.Fail(x.Message);

            }
        }

        [TestMethod]
        public void EstateDomain_Can_Update_SchemaTables()
        {
            try
            {
                _EstateAnalysisRepository.Configure(false,true);

                Assert.IsTrue(_EstateAnalysisRepository.IsConfigured == true);
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);

            }
        }

        [TestMethod]
        public void EstateDomain_Can_Run_DataMigration()
        {
            try
            {
                _EstateAnalysisRepository.Configure();

                _EstateAnalysisRepository.DBMigrateUp(typeof(InitialSeed).Assembly);

                Assert.IsTrue(_EstateAnalysisRepository.IsConfigured == true);
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);

            }
        }

        #region Client
        [TestMethod]
        public void ClientDomain_Can_getClient()
        {
            try
            {
                ConfigureRepos();

                var client = _ClientRepository.Get<Client,int>(1);

                Assert.IsTrue(client.Name != "");
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);

            }
        }
        #endregion

        #region Estate

        [TestMethod]
        public void EstateDomain_Can_Initialise_EstateAnalysis()
        {
            try
            {
                ConfigureRepos();

                EstateAnalysis estate = new EstateAnalysis();

                estate.Client = _ClientRepository.Get<Client, int>(1);

                estate.Initialise();

                Assert.IsTrue(estate.Client.ClientDetails.FirstName != "");
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);

            }
        }

        [TestMethod]
        public void EstateDomain_Can_Calculate_FixedProperty()
        {
            try
            {
                ConfigureRepos();

                EstateAnalysis estate = new EstateAnalysis();

                estate.Client = _ClientRepository.Get<Client, int>(1);

                estate.Initialise();

                estate.ExecutorsFees = 100000.00;

                Assert.IsTrue(estate.PropertyAssets.Count>0);
            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);

            }
        }
        #endregion

        #region Repository Events
        private static void Repository_PreInsertEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;

            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = "Test";
        }
        private static void Repository_PreUpdateEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;

            if (entity.IsModified)
            {
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = "Test";

            }

        }
        private static void Repository_PreLoadEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;
            entity.IsLoading = true;
        }
        private static void Repository_PostLoadEvent(object sender, my.domain.lib.core.Domain.EntityEventArgs e)
        {
            BaseEntity<int> entity = (BaseEntity<int>)sender;
            entity.IsLoading = false;

        }
        #endregion
    }
}
