using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Maui.Core
{
    public class ForQueryParameterAttribute : Attribute
    {

        public string PropertyName { get; }
        public string ParamName { get; }

        public ForQueryParameterAttribute(string propertyName, string paramName)
        {
            PropertyName = propertyName;
            ParamName = paramName;
        }

    }
}
