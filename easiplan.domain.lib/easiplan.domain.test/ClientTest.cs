using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using easiplan.domain;
using easiplan.domain.Entities;
using easiplan.domain.Services;
using easiplan.domain.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Registry;
using my.domain.lib.core.Repository;

namespace easiplan.domain.test
{
    [TestClass]
    public class ClientTest
    {
        IGenericRepository _Repository;
        public ClientTest()
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

            string RegistryKey = "FinX";

            //TO DO : Support other types of connections
            ConnectionTypes connType = (ConnectionTypes)Enum.Parse(typeof(ConnectionTypes), RegistryWrapper.ReadRegistry(RegistryKey, "DbType", "MySQL"), true);

            var connectionInfo = new MySqlConnectionInfo(connType)
            {
                ServerName = "easiplan.southafricanorth.cloudapp.azure.com",//RegistryWrapper.ReadRegistry(RegistryKey, "DbServer"),
                DbName = "easiworx_test",//RegistryWrapper.ReadRegistry(RegistryKey, "DbCatalog"),
                DbSchema = "easiworx_test",//RegistryWrapper.ReadRegistry(RegistryKey, "DbSchema"),
                PortNumber = "3306",//RegistryWrapper.ReadRegistry(RegistryKey, "DbPort"),
                UserName = "easiworx_test",//RegistryWrapper.ReadRegistry(RegistryKey, "DbAdminUser").Decrypt(RegistryKey),
                UserPwd = "e@s1worx_T3st",//RegistryWrapper.ReadRegistry(RegistryKey, "DbAdminPwd").Decrypt(RegistryKey),
                AssemblyType = typeof(DomainServices)
            };

            _Repository = new MySqlRepository(connectionInfo) { UserName = "easiworx_test"}; //"taurique.toffie@gmail.com"

            _Repository.PreLoadEvent += Repository_PreLoadEvent;
            _Repository.PostLoadEvent += Repository_PostLoadEvent;
            _Repository.PreUpdateEvent += Repository_PreUpdateEvent;
            _Repository.PreInsertEvent += Repository_PreInsertEvent;

            _Repository.Configure();

        }

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

        [TestMethod]
        public void Client_PropertyChanged_DateOfBirth()
        {
            var client = new Client();

            client.ClientDetails.DateOfBirth = DateTime.Parse("07/06/1967");

            Assert.AreEqual(53, client.ClientDetails.Age);

            Assert.AreEqual(53, client.ClientPortfolio._CurrentAge);
            Assert.AreEqual(53, client.ClientFna._CurrentAge);
            //Assert.AreEqual(53, client.ClientFnaInvestment.CurrentAge);


            Assert.AreEqual(true, client.ClientDetails.IsModified);
            Assert.AreEqual(true, client.IsModified);
        }

        [TestMethod]
        public void Client_PropertyChanged_Surname()
        {

            var client = new Client();

            client.ClientDetails.ClientTitle = "Mr";
            client.ClientDetails.FirstName = "Mogamad";
            client.ClientDetails.MidName = "Yusuf";
            client.ClientDetails.LastName = "Jattiem";

            Assert.AreEqual("Jattiem, Mr Mogamad Yusuf", client.ClientDetails.Fullname);
            Assert.AreEqual("MY", client.ClientDetails.Initials);

            Assert.AreEqual("Mr MY Jattiem", client.BankDetails.AcctName);

            Assert.AreEqual(true, client.ClientDetails.IsModified);
            Assert.AreEqual(true, client.IsModified);

        }

        [TestMethod]
        public void Client_PropertyChanged_IdentificationNo()
        {

            var client = new Client();

            client.ClientDetails.IdentificationNo = "6706075130084";

            Assert.AreEqual(DateTime.Parse("07/06/1967"), client.ClientDetails.DateOfBirth);
            Assert.AreEqual("Male", client.ClientDetails.Gender);
            Assert.AreEqual("South African", client.ClientDetails.Nationality);

            Assert.AreEqual(true, client.ClientDetails.IsModified);
            Assert.AreEqual(true, client.IsModified);

        }

        [TestMethod]
        public void ClientAssets_PropertyChanged_Value()
        {

            var client = new Client();

            Asset asset = new Asset() { Class="Property", Description="New Property", Value = 10000 };

            client.ClientAssets.AssetsBindingList.Add(asset);

            client.Calculate();

            Assert.AreEqual(10000, client.ClientNettWorth);
            

            Assert.AreEqual(true, client.ClientAssets.IsModified);
            Assert.AreEqual(true, client.IsModified);

        }

        [TestMethod]
        public async Task Client_Load()
        {
            ClientDetailsService clientDetailsService = new ClientDetailsService(_Repository);
            ClientService clientService = new ClientService(_Repository);

            var clientDetails = await Task.Run(()=>clientDetailsService.Get(1));
            var client = await Task.Run(()=>clientService.Get(clientDetails.ClientId));

            Assert.AreEqual(1, client.Id);



        }
        [TestMethod]
        public void ClientRetirementPortfolio_View_Test()
        {
            var clientRetirementPortfolioService = new ClientRetirementPortfolioService(_Repository);
            
            try
            {
                var results = (List<ClientRetirementPortfolio_View>)clientRetirementPortfolioService.ListView(null);
                Assert.IsTrue(results != null && results.Count > 0);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [TestMethod]
        public void ClientDetailsView_Test()
        {
            var clientDetailsService = new ClientDetailsService(_Repository);

            try
            {
                var results = (List<ClientDetailsView>)clientDetailsService.ListView(null);
                Assert.IsTrue(results != null && results.Count > 0);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [TestMethod]
        public void ClientParallelLoad_Test()
        {
            try
            {
                //var clientService = new ClientRetirementPortfolioService(_Repository);
                var clientService = new ClientService(_Repository);
                Client client = null;
                //int i=1;
                //var listView = clientService.ListView(l => l.FirstName == "Yusuf");
                client = clientService.Get(1);
                //var result = Parallel.For(i, 100,x => clientService.ListView(l => l.FirstName == "Yusuf"));
                Assert.IsTrue(client != null);
            }
            catch (Exception)
            {

                throw;
            }
                        



        }
    }
}
