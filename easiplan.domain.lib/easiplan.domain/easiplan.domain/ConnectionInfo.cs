using my.domain.lib.core.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace easiplan.domain
{
    //
	public class MySqlConnectionInfo : BaseEntity<int>, IConnectionInfo
	{
		public string ConnectionType
		{
			get;
			set;
		}

		public bool CreateNewSchema
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Database Name is Required")]
		public string DbName
		{
			get;
			set;
		}

		public string DbSchema
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Port Number is Required")]
		public string PortNumber
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Server Name or IP is Required")]
		public string ServerName
		{
			get;
			set;
		}

		public bool UpdateSchema
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Database UserName is Required")]
		public string UserName
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Database UserPassword is Required")]
		public string UserPwd
		{
			get;
			set;
		}

		public Type AssemblyType
		{
			get;
			set;
		}
		public string MappingNamespace { get; set; } = "easiplan.domain.Entities";

		public string ConnectionString(bool CreateDb = false)
		{
			return string.Format("server={0};Port={1};userid={2};database={3};password={4};Persist Security Info=True;", ServerName, PortNumber, UserName, CreateDb ? "" : DbName, UserPwd);
		}

		public void CloseSession()
		{
			throw new NotImplementedException();
		}

		public void EnsureConnectionClosed()
		{
			throw new NotImplementedException();
		}

		public void EnsureConnectionOpen()
		{
			throw new NotImplementedException();
		}

		public MySqlConnectionInfo(ConnectionTypes connectionType)
		{
			ConnectionType = connectionType.ToString();
		}
	}
}
