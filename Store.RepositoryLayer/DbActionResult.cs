using System;
using System.Collections.Generic;
using System.Text;

namespace Store.RepositoryLayer
{
    public class DbActionResult
    {
        public bool Success{ get; set; }
        public String Message { get; set; }

    }


    public class DbActionResult<T> 
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
