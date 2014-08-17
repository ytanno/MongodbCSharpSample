using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyMongoDBSample
{
	[BsonIgnoreExtraElements]
	public class MyData
	{
		[BsonId]
		public BsonObjectId _id { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime LogTime { get; set; }

		public string Name;
	}
}