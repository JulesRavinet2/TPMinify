using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Minify.Controllers
{
    [Controller]
    [Route("/redirect")]
    public class RedirectController : ControllerBase
    {
        [HttpGet]
        [Route("/redirect/{id}")]
        public IActionResult Get(string id)
        {
            var database = new MongoRepository();
            var documents = database.Get(id);
            return Redirect(documents.Url);
        }
    }
}