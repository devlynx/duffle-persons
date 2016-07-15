﻿#region Copyright (c) 2016 Periwinkle Software Limited
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

namespace duffle_persons.Processors.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boilerplate;
    using duffle_persons.Processors.Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Models;
    using Repositories.Interfaces;

    public class OwnersProcessor : IOwnersProcessor
    {
        public OwnersProcessor(IOwnersRepository repository)
        {
            this.repository = repository;
        }
        private IOwnersRepository repository { get; set; }

        public async Task<JSendBuilder> GetOwners()
        {
            List<Owner> owners = await repository.SelectAsync();

            return new JSendBuilder()
                .Success()
                .Data(owners);
        }

        public IMicroserviceInfo PingData(IHostingEnvironment hostingEnvironment)
        {
            return new MicroserviceInfo { module = "owners", environment = hostingEnvironment.EnvironmentName };
        }
    }
}