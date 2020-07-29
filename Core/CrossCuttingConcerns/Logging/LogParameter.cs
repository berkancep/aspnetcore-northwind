using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; }  // Product Table
        public object Value { get; set; } // Id:1, Name:Elma
        public string Type { get; set; } // Product
    }
}
