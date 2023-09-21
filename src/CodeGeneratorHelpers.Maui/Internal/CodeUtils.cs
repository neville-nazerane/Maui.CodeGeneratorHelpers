using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CodeGeneratorHelpers.Internal
{
    internal class CodeUtils
    {

        public static IEnumerable<string> GenerateTransientInjections(IEnumerable<string> classNames)
            => classNames.Select(c => $".AddTransient<{c}>()")
                         .ToArray();


        public string GenerateUtilClass(string @namespace,
                                        string fileName, 
                                        IEnumerable<string> methods)
            => $@"


public class {fileName}
{{
    
}}

".Trim();

    }
}
