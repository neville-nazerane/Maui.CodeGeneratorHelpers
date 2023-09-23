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

        public static string GeneratePartialPage(string @namespace,
                                                 IEnumerable<string> usings,
                                                 string pageName,
                                                 string viewModelName)
            => @$"
{PrintUsings(usings)}

namespace {@namespace};

public partial class {pageName} {{

    private {viewModelName} viewModel = null;

    public {viewModelName} ViewModel
    {{
        get
        {{
            if (viewModel is null)
            {{
                viewModel = Shell.Current.Handler.MauiContext.Services.GetService<{viewModelName}>();
                BindingContext = viewModel;
            }}
            return viewModel;
        }}
    }}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {{
        await ViewModel.OnNavigatedToAsync();
        OnNavigatedToInternal(args);
        base.OnNavigatedTo(args);
    }}

    protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) {{ }}

}}
    
".Trim();

        public static string GenerateUtilClass(string @namespace,
                                                IEnumerable<string> methods,
                                                IEnumerable<string> usings)
            => $@"
{PrintUsings(usings)}

namespace {@namespace};

public static class GenerationUtils
{{
    
{string.Join('\n', methods.Select(u => $"   {u}").ToArray())}

}}

".Trim();

        private static string PrintUsings(IEnumerable<string> usings)
        {
            return string.Join('\n', usings.Select(u => $"using {u};").ToArray());
        }
    }
}
