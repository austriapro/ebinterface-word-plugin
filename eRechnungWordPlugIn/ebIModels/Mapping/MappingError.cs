using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Mapping
{
    public class MappingError
    {        
        public MappingError(Type type,string message)
        {
            Property = type.FullName;
            Message = message;
        }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}
