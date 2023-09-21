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

        public static string GenerateInjectionMethod(IEnumerable<string> injections)
            => $@"
    
".Trim();

        public static string GenerateUtilClass(string @namespace,
                                                IEnumerable<string> methods)
            => $@"


namespace {@namespace};

public class GenerationUtils
{{
    


}}

".Trim();

    }
}
