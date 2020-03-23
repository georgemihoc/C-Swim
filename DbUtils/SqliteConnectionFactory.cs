using System;
using System.Data;
using System.Data.SQLite;

namespace ConnectionUtils
{
	public class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection createConnection()
		{
		//Mono Sqlite Connection
		String connectionString = "Data Source=C:\\UBB\\MPP\\Laborator2CS\\DbUtils\\swimDb;Version=3";
			return new SQLiteConnection(connectionString);

			// Windows Sqlite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
			//String connectionString = "Data Source=tasks.db;Version=3";
			//return new SqliteConnection(connectionString);
		}
	}
}
