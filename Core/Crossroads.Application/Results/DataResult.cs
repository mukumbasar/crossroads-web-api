﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T? Data { get; set; }

        public DataResult(T? data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }

        public DataResult(T? data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }
    }
}
