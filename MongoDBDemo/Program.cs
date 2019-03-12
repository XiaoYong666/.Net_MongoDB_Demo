using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBDemo
{

	public class User
	{
		public object _id { get; set; }

		public string name { get; set; }

		public string title { get; set; }

		public int age { get; set; }

	}
	class Program
	{
		protected static IMongoDatabase _database;

		protected static IMongoClient _client;
		static void Main(string[] args)
		{

			var mongoDbHelper = new MongoDbCsharpHelper(ConfigurationManager.ConnectionStrings["mongodbConn"].ConnectionString, "test");
			//按条件筛选查询
			var list1 = mongoDbHelper.Find<User>("testCollection", t => t._id != null);
			list1.ForEach(p =>
				{
					Console.WriteLine("编号：" + p._id + ",姓名：" + p.name + ",标题：" + p.title + ",年龄：" + p.age);
				});

			//分页查询
			int rsCount = 0;
			var list2 = mongoDbHelper.FindByPage<User, User>("testCollection", t => t.name == "测试5", t => t, 1, 2, out rsCount);

		    
			//新增
		    mongoDbHelper.Insert<User>("testCollection", new User {name = "帮助测试类", title = "新测试", age = 22});

			//修改
			mongoDbHelper.Update<User>("testCollection", new User {name = "帮助测试类修改", title = "新测试修改", age = 20},
				t => t.name == "帮助测试类");

			//删除
			mongoDbHelper.Delete<User>("testCollection",t => t.name == "帮助测试类修改");

			//新增数据库DB
//			mongoDbHelper.ClearCollection<User>("testCollection");
		
			Console.ReadKey();
		}
	}
}
