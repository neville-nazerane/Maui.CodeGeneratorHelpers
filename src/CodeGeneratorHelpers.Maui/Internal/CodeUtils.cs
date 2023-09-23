using CodeGeneratorHelpers.Maui.Internal;
using CodeGeneratorHelpers.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CodeGeneratorHelpers.Internal
{
    internal class CodeUtils
    {

        internal static IEnumerable<string> GenerateTransientInjections(IEnumerable<string> classNames)
            => classNames.Select(c => $".AddTransient<{c}>()")
                         .ToArray();

        internal static string GenerateInjectionMethod(IEnumerable<string> injections)
            => $@"
    public static IServiceCollection AddGeneratedInjections(this IServiceCollection services)
        => services{string.Join("\n                   ", injections)};
".Trim();

        internal static string GeneratePartialPage(string @namespace,
                                                 IEnumerable<string> usings,
                                                 string pageName,
                                                 string viewModelName,
                                                 IEnumerable<PageEventData> events)
        {

            var eventsGrouped = events.GroupBy(e => e.Type).ToList();
            var methods = eventsGrouped.Select(g => PrintEventMethod(g, g.Key)).ToList();

            if (!eventsGrouped.Any(e => e.Key == PageEventType.OnAppearing))
                methods.Add(PrintEventMethod(Array.Empty<PageEventData>(), PageEventType.OnAppearing));

            return @$"
{PrintUsings(usings)}

namespace {@namespace};

public partial class {pageName} 
{{
    
    private {viewModelName} viewModel = null;

    public {viewModelName} ViewModel
    {{
        get
        {{
            SetupViewModelIfNotAlready();
            return viewModel;
        }}
    }}

    private void SetupViewModelIfNotAlready()
    {{
        if (viewModel is null)
        {{
            viewModel = Shell.Current.Handler.MauiContext.Services.GetService<{viewModelName}>();
            BindingContext = viewModel;
        }}
    }}

{string.Join('\n', methods)}

}}


".Trim();

        }

        internal static string GenerateUtilClass(string @namespace,
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

        private static string PrintEventLines(IEnumerable<PageEventData> methods, PageEventType eventType)
        {
            var builder = new StringBuilder();

            switch (eventType)
            {
                case PageEventType.OnBackButtonPressed:
                    {
                        for (int i = 0; i < methods.Count(); i++)
                        {
                            var m = methods.ElementAt(i);
                            builder.AppendLine($"        var res{i + 1} = {(m.IsAwaitable ? "await" : string.Empty)} ViewModel.{m.FunctionName}();");
                            builder.AppendLine($"        if (!res{i + 1}) return false;");
                        }

                        break;
                    }

                case PageEventType.OnSizeAllocated:
                    {
                        foreach (var m in methods)
                            builder.AppendLine($"        {(m.IsAwaitable ? "await" : string.Empty)} ViewModel.{m.FunctionName}(width, height);");
                        break;
                    }
                default:
                    {
                        foreach (var m in methods)
                            builder.AppendLine($"        {(m.IsAwaitable ? "await" : string.Empty)} ViewModel.{m.FunctionName}();");
                        break;
                    }
            }
            return builder.ToString();
        }

        private static string PrintEventMethod(IEnumerable<PageEventData> events,
                                               PageEventType eventType)
        {
            var methods = events.Where(e => e.Type == eventType).ToArray();

            var methodContent = PrintEventLines(methods, eventType);

            if (!methods.Any() && eventType != PageEventType.OnAppearing) return null;

            return eventType switch
            {
                PageEventType.OnAppearing => $@"
    protected override {methods.AsyncIfHasAsync()}void OnAppearing()
    {{
        SetupViewModelIfNotAlready();
        {(!methods.Any() ? "OnAppearingInternal();" : null)}
{methodContent}
        base.OnAppearing();
    }}

    {(!methods.Any() ? "partial void OnAppearingInternal();" : null)}
",


                PageEventType.OnBackButtonPressed => $@"
    protected override {methods.AsyncIfHasAsync()}bool OnBackButtonPressed()
    {{
{methodContent}
        return base.OnBackButtonPressed();
    }}
",

                PageEventType.OnBindingContextChanged => $@"
    protected override {methods.AsyncIfHasAsync()}void OnBindingContextChanged()
    {{
{methodContent}
        base.OnBindingContextChanged();
    }}
",

                PageEventType.OnChildMeasureInvalidated => $@"
    protected override {methods.AsyncIfHasAsync()}void OnChildMeasureInvalidated(object sender, EventArgs e)
    {{
{methodContent}
        base.OnChildMeasureInvalidated(sender, e);
    }}
",

                PageEventType.OnDisappearing => $@"
    protected override {methods.AsyncIfHasAsync()}void OnDisappearing()
    {{
{methodContent}
        base.OnDisappearing();
    }}
",

                PageEventType.OnNavigatedFrom => $@"
    protected override {methods.AsyncIfHasAsync()}void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {{
{methodContent}
        base.OnNavigatedFrom(args);
    }}
",

                PageEventType.OnNavigatedTo => $@"
    protected override {methods.AsyncIfHasAsync()}void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {{
{methodContent}
        base.OnNavigatedTo(args);
    }}
",

                PageEventType.OnNavigatingFrom => $@"
    protected override {methods.AsyncIfHasAsync()}void OnNavigatingFrom(Microsoft.Maui.Controls.NavigatingFromEventArgs args)
    {{
{methodContent}
        base.OnNavigatingFrom(args);
    }}
",

                PageEventType.OnParentSet => $@"
    protected override {methods.AsyncIfHasAsync()}void OnParentSet()
    {{
{methodContent}
        base.OnParentSet();
    }}
",

                PageEventType.OnSizeAllocated => $@"
    protected override {methods.AsyncIfHasAsync()}void OnSizeAllocated(double width, double height)
    {{
{methodContent}
        base.OnSizeAllocated(width, height);
    }}
",
                _ => null,
            };
        }

        private static string PrintUsings(IEnumerable<string> usings)
        {
            return string.Join('\n', usings.Select(u => $"using {u};").ToArray());
        }
    }
}
