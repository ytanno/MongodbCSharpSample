//nuget  official mongodb c# driver
//licence Apache-2.0
using MongoDB.Driver;
using MongoDB.Driver.Linq;


using System;
using System.Diagnostics;
using System.Linq;

namespace MyMongoDBSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Process dbProcess;
			if ( !CheckProcess(@"mongod", out dbProcess) )
			{
				//example
				//install http://www.mongodb.org/downloads  windows 64bit  .msi
				//mongodb 2.6.4
				var dbExe = @"C:\Program Files\MongoDB 2.6 Standard\bin\mongod.exe";
				var dataFolder = @"I:\dev\mongodb\data\";
				dbProcess = StartDB(dbExe, dataFolder);
			}

			var cli = new MongoClient();
			var dbName = "TestDB";
			var db = cli.GetServer().GetDatabase(dbName);

			var tableName = @"MyTable";
			var col = db.GetCollection<MyData>(tableName);

			var myData1 = new MyData()
			{
				LogTime = DateTime.Now,
				Name = "tanno",
			};

			//allRemove
			col.RemoveAll();


			//insert
			col.Insert(myData1);

			//search
			var linq = col.AsQueryable().FirstOrDefault();
			Console.WriteLine(col.AsQueryable().Count());

			//remove
			var query = new QueryDocument("_id", linq._id);
			col.Remove(query);

			Console.WriteLine(col.AsQueryable().Count());

			EndProcess(dbProcess);

			Console.WriteLine("Press Enter and End");
			Console.ReadLine();
		}

		private static bool CheckProcess(string processName, out Process dbProcess)
		{
			dbProcess = new Process();
			foreach ( var p in Process.GetProcesses() )
			{
				if ( p.ProcessName == processName )
				{
					dbProcess = p;
					return true;
				}
			}
			return false;
		}

		private static void EndProcess(Process p)
		{
			p.Kill();
		}

		private static Process StartDB(string exeFileName, string dataSaveFolderPath)
		{
			var mongoDB = new Process();
			mongoDB.StartInfo.FileName = exeFileName;
			mongoDB.StartInfo.Arguments = @" --dbpath=" + dataSaveFolderPath;
			//mongoDB.StartInfo.UseShellExecute = false;
			//mongoDB.StartInfo.CreateNoWindow = true; // not open mongodb console
			mongoDB.Start();

			return mongoDB;
		}
	}
}
