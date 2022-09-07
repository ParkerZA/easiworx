using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using my.domain.lib.core.Repository;
using my.domain.lib.core.test.Models;

namespace my.domain.lib.core.test
{
    [TestClass]
    public class RepositoryTest
    {
        readonly baseGenericNHRepository repository;
        public RepositoryTest()
        {
            var ConnectionInfo = new ConnectionInfo(ConnectionTypes.MySQL)
            {
                ServerName = "localhost",
                DbName = "mytestdb",
                DbSchema = "Schema1",
                PortNumber = "3306",
                UserName= "mytestdb",
                UserPwd = "mytestdb",
                AssemblyType = typeof(TestModel),
                MappingNamespace = "my.domain.lib.core.test.Models"
            };

            repository = new MySqlRepository(ConnectionInfo);

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
                repository.Configure(true, true);

                Assert.IsTrue(repository.IsConfigured);

                Assert.IsFalse(repository.IsInError);

            }
            catch(Exception x)
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

                repository.Add<TestModel,int>(new TestModel() { PersistedParam = "Value1" });

                Assert.IsFalse(repository.IsInError);

            }
            catch (Exception x)
            {
                Assert.Fail(x.Message);
            }

        }

        [TestMethod]
        public void Test_Repository_List()
        {
            try
            {
                repository.Configure();

                var res = repository.List<TestModel, int>(x=>x.PersistedParam =="Value1");

                Assert.IsFalse(res.Count()==0);

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
