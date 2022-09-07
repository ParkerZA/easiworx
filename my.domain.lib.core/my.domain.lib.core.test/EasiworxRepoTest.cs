using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using my.domain.lib.core.Extensions;
using my.domain.lib.core.Repository;
using my.domain.lib.core.test.Models;
using my.domain.lib.core.test.Models.Views;
using my.domain.lib.core.test.Repository;

namespace my.domain.lib.core.test
{
    [TestClass]
    public class EasiworxRepositoryTest
    {
        readonly EasiworxRepository repository;
        
        public EasiworxRepositoryTest()
        {
            //var ConnectionInfo = new ConnectionInfo(ConnectionTypes.MySQL)
            //{
            //    ServerName = "easiplan.southafricanorth.cloudapp.azure.com",
            //    DbName = "easiworx_test",
            //    DbSchema = "easiworx_test",
            //    PortNumber = "3306",
            //    UserName= "easiworx_test",
            //    UserPwd = "e@s1worx_T3st",
            //    AssemblyType = typeof(ClientDetailsView),
            //    MappingNamespace = "my.domain.lib.core.test.Models.Views"
            //};

            var ConnectionInfo = new ConnectionInfo(ConnectionTypes.MySQL)
            {
                ServerName = "easiplan.southafricanorth.cloudapp.azure.com",
                DbName = "mojaff1",
                DbSchema = "mojaff1",
                PortNumber = "3306",
                UserName = "mojaff",
                UserPwd = "mojaff@easiplan",
                AssemblyType = typeof(ClientDetailsView),
                MappingNamespace = "my.domain.lib.core.test.Models.Views"
            };
            repository = new EasiworxRepository(ConnectionInfo);

            //repository.PreLoadEvent += Repository_PreLoadEvent;
            //repository.PostLoadEvent += Repository_PostLoadEvent;
            //repository.PreUpdateEvent += Repository_PreUpdateEvent;
            //repository.PreInsertEvent += Repository_PreInsertEvent;
        }
        [TestMethod]
        public void Test_Configure_New_RepositorySchema()
        {
            try
            {
                repository.Configure();//true, true

                Assert.IsTrue(repository.IsConfigured);

                Assert.IsFalse(repository.IsInError);

            }
            catch(Exception x)
            {
                Assert.Fail(x.Message);
            }

        }

        [TestMethod]
        public void Test_Configure_And_MigrateUp()
        {
            try
            {
                repository.Configure();//true, true

                if (repository.IsConfigured)
                {
                    repository.DBMigrateUp(typeof(Migration_500).Assembly);
                }

                Assert.IsFalse(repository.IsInError);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }
        }
       
        [TestMethod]
        public void Test_Repository_List_View()
        {
            try
            {
                repository.Configure();

                var res = repository.List<ClientDetailsView, int>(x=>x.LastName.StartsWith("Jatt") && x.ClientId>0);

                Assert.IsFalse(res.Count()==0);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }

        [TestMethod]
        public void Test_Repository_List_View_Encrypt()
        {
            try
            {
                repository.Configure();

                string idNo = "6706075130084";//.Encrypt("key1");

                var res = repository.List<ClientDetailsView, int>(x => x.ClientId > 0).Where(x=> x.IdentificationNo.StartsWith(idNo));

                Assert.IsFalse(res.Count() == 0);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }
        [TestMethod]
        public void Test_Repository_Add()
        {
            try
            {
                repository.Configure();

                repository.Add<TestModel, int>(new TestModel() { PersistedParam = "Value1" });

                Assert.IsFalse(repository.IsInError);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }
        [TestMethod]
        public void Test_Repository_Get()
        {
            try
            {
                repository.Configure();

                var res = repository.Get<TestModel, int>(1);

                Assert.IsFalse(res==null);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }

        [TestMethod]
        public void Test_Repository_Update()
        {
            try
            {
                repository.Configure();

                var res = repository.Get<TestModel, int>(1);

                Assert.IsFalse(res == null);

                res.PersistedParam = "Value2";

                repository.Update<TestModel,int>(res);


            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }
    }
}
