using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace PasswordVaultAPI.API.Logs
{
	public class LogConfig
	{

		public static Logger CreateLogConfig(IConfiguration configuration)
		{

			var sqlColumn = new SqlColumn
			{
				ColumnName = "UserName",
				DataType = System.Data.SqlDbType.NVarChar,
				PropertyName = "UserName",
				DataLength = 100,
				AllowNull = true
			};

			var columnOpt = new ColumnOptions
			{
				AdditionalColumns = new Collection<SqlColumn> { sqlColumn }
			};

			var log = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("Logs/log.txt")
				.WriteTo.MSSqlServer(
					connectionString: configuration.GetConnectionString("SqlCon"),
					sinkOptions: new MSSqlServerSinkOptions { AutoCreateSqlTable = true, TableName = "Logs" },
					columnOptions: columnOpt
				)
				.MinimumLevel.Information()
				.Enrich.FromLogContext()
				.CreateLogger();

			return log;
		}


	}


}
