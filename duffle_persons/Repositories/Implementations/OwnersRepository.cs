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

namespace duffle_persons.Repositories.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.Model;
    using Amazon.Runtime;
    using Boilerplate;
    using Interfaces;
    using Models;

    public class OwnersRepository : IOwnersRepository
    {
        private AmazonDynamoDBClient client { get; set; }

        public OwnersRepository(IRepositoryIdentity repositoryIdentity)
        {
            BasicAWSCredentials awsCredentials = new BasicAWSCredentials(
                repositoryIdentity.AccessKeyId, repositoryIdentity.SecretAccessKey);
            client = new AmazonDynamoDBClient(awsCredentials, repositoryIdentity.Region);
        }

        public async Task<bool> AddOrReplaceAsync(Owner owner)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePhysicalAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Owner>> SelectAsync()
        {
            QueryRequest query = new QueryRequest();
            query.TableName = "owners";
            QueryResponse queryResponse = await client.QueryAsync(query);
            return new List<Owner>();
        }

        public async Task<Owner> SelectAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
