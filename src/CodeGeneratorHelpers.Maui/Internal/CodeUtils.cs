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
    public static IServiceCollection AddGeneratedInjections(this IServiceCollection services)
        => services{string.Join("\n                   ", injections)};
".Trim();

        public static string GenerateUtilClass(string @namespace,
                                                IEnumerable<string> methods,
                                                IEnumerable<string> usings)
            => $@"
{string.Join('\n', usings.Select(u => $"using {u};").ToArray())}

namespace {@namespace};

public static class GenerationUtils
{{
    
{string.Join('\n', methods.Select(u => $"   {u}").ToArray())}

}}

".Trim();

    }
}
