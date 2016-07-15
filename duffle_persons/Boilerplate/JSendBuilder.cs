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

namespace duffle_persons.Boilerplate
{
    using System;

    // http://labs.omniti.com/labs/jsend

    // Type     Description                                             Required Keys        Optional Keys
    // -------  ------------------------------------------------------  -------------------  -------------
    // success  All went well, and (usually) some data was returned.    status, data
    // fail     There was a problem with the data submitted, or some 
    //          precondition of the API call wasn't satisfied           status, data  
    // error    An error occurred in processing the request, 
    //          i.e.an exception was thrown                             status, message      code, data
    
    /// <summary>
    /// JSendBuilder is a fluent interface used to build a JSend response.
    /// </summary>
    public class JSendBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JSendBuilder"/> class.
        /// </summary>
        public JSendBuilder()
        {
            status = "success";
            data = null;
            message = null;
            code = null;
        }

        /// <summary>
        /// Add an opetional error code to the JSendBuilder instanace. Should only be used in an error status.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>JSendBuilder.</returns>
        public JSendBuilder Code(string code)
        {
            this.code = code;
            return this;
        }

        /// <summary>
        /// Add data content to the JSendBuilder instanace.
        /// </summary>
        /// <param name="data">The data to add.</param>
        /// <returns>JSendBuilder.</returns>
        public JSendBuilder Data(object data)
        {
            this.data = data;
            return this;
        }

        public JSendBuilder Error()
        {
            status = "error";
            return this;
        }

        public JSendBuilder Exception(Exception exception)
        {
            status = "error";
            message = String.Format("{0} exception: {1}", exception.GetType(), exception.Message);
            return this;
        }

        public JSendBuilder Fail()
        {
            status = "fail";
            return this;
        }

        /// <summary>
        /// Add an error message to the JSendBuilder instanace. Should only be used in an error status.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>JSendBuilder.</returns>
        public JSendBuilder Message(string message)
        {
            this.message = message;
            return this;
        }

        public JSendBuilder Success()
        {
            status = "success";
            return this;
        }

        public string code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
