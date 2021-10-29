// ------------------------------------------------
// Copyright (c) Coalitions of Inspired Developers
// FREE TO USE FOR THE WORLD
// ------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Phoenix.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            Ok("Hello Adam, Eve will tempt you to the forbidden tree!");
    }
}
