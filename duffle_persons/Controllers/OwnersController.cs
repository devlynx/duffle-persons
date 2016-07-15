#region Copyright (c) 2016 Periwinkle Software Limited
// MIT License -- https://opensource.org/licenses/MIT
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
// associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

namespace duffle_persons.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Boilerplate;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Processors.Interfaces;
    using Swashbuckle.SwaggerGen.Annotations;

    /// <summary>
    /// Class OwnersController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ownersController : Controller
    {
        public ownersController(IOwnersProcessor ownersProcessor, IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            OwnersProcessor = ownersProcessor;
        }

        private IHostingEnvironment HostingEnvironment { get; set; }

        private IOwnersProcessor OwnersProcessor { get; set; }

        // DELETE api/owners/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }

        // GET api/owners
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Owner>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<Owner>))]
        public IActionResult Get()
        {
            return Ok(OwnersProcessor.GetOwners());
        }

        // GET api/owners/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            return "value";
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new JSendBuilder().Success().Data(OwnersProcessor.PingData(HostingEnvironment)));
        }

        // POST api/owners
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [HttpPost]
        [Route("create")]
        public void Create([FromBody]string value)
        {
        }

        // PUT api/owners/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]string value)
        {
        }
    }
}