using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteDbTest
{
    public class TestModel
    {
        [BsonId]
        public long Id { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
    }
}
