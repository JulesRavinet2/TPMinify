using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Minify.Model;
using Minify.Interfaces;

namespace Minify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MinifyController : ControllerBase
    {
        public MinifyController(IRepository Database = null) {
            if(Database == null) {
                database = new MongoRepository();
            }
            else {
                database = Database;
            }
        }

        public IRepository database;
        [HttpPost]
        public void Add([FromBody] MinifyData data)
        {
            var token = new TokenGenerator();
            data._id = token.Generate();
            data.Key = data._id;
            database.Add(data);
        }

        [HttpGet]
        public IEnumerable<MinifyData> Get()
        {
            return database.Get();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            database.Delete(id);
        }
    }
}