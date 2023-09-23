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
                                                 IEnumerable<PageEventData> _events)
            => @$"
{PrintUsings(usings)}

namespace {@namespace};

public partial class {pageName} {{
    
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

    protected override void OnAppearing()
    {{
        SetupViewModelIfNotAlready();
        {PrintEventCall(_events, PageEventType.OnAppearing)}
        OnAppearingInternal();
        base.OnAppearing();
    }}

    partial void OnAppearingInternal();
    
    protected override bool OnBackButtonPressed()
    {{
        {PrintEventCall(_events, PageEventType.OnBackButtonPressed)}
        OnBackButtonPressedInternal();
        return base.OnBackButtonPressed();
    }}

    partial void OnBackButtonPressedInternal();

    protected override void OnBindingContextChanged()
    {{
        {PrintEventCall(_events, PageEventType.OnBindingContextChanged)}
        OnBindingContextChangedInternal();
        base.OnBindingContextChanged();
    }}

    partial void OnBindingContextChangedInternal();

    protected void OnChildMeasureInvalidated(object sender, EventArgs e)
    {{
        {PrintEventCall(_events, PageEventType.OnChildMeasureInvalidated)}
        OnChildMeasureInvalidatedInternal(sender, e);
    }}

    partial void OnChildMeasureInvalidatedInternal(object sender, EventArgs e);

    protected override void OnDisappearing()
    {{
        {PrintEventCall(_events, PageEventType.OnDisappearing)}
        OnDisappearingInternal();
        base.OnDisappearing();
    }}

    partial void OnDisappearingInternal();

    protected void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {{
        {PrintEventCall(_events, PageEventType.OnNavigatedFrom)}
        OnNavigatedFromInternal(args);
    }}

    partial void OnNavigatedFromInternal(Microsoft.Maui.Controls.NavigatedFromEventArgs args);

    protected void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {{
        {PrintEventCall(_events, PageEventType.OnNavigatedTo)}
        OnNavigatedToInternal(args);
    }}

    partial void OnNavigatedToInternal(Microsoft.Maui.Controls.NavigatedToEventArgs args);

    protected void OnNavigatingFrom(Microsoft.Maui.Controls.NavigatingFromEventArgs args)
    {{
        {PrintEventCall(_events, PageEventType.OnNavigatingFrom)}
        OnNavigatingFromInternal(args);
    }}

    partial void OnNavigatingFromInternal(Microsoft.Maui.Controls.NavigatingFromEventArgs args);

    protected override void OnParentSet()
    {{
        {PrintEventCall(_events, PageEventType.OnParentSet)}
        OnParentSetInternal();
        base.OnParentSet();
    }}

    partial void OnParentSetInternal();

    protected override void OnSizeAllocated(double width, double height)
    {{
        {PrintEventCall(_events, PageEventType.OnSizeAllocated)}
        OnSizeAllocatedInternal(width, height);
        base.OnSizeAllocated(width, height);
    }}

    partial void OnSizeAllocatedInternal(double width, double height);


}}
    
".Trim();

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

        private static string PrintEventMethod(IEnumerable<PageEventData> _events,
                                               PageEventType eventType)
        {
            var methods = _events.Where(e => e.Type == eventType).ToArray();

            var lines = methods
                            .Select(m => $"{(m.IsAwaitable ? "await" : string.Empty)} {m.FunctionName}();")
                            .ToArray();

            if (!lines.Any() && eventType != PageEventType.OnAppearing) return null;

            var methodContent = string.Join('\n', lines);
            return eventType switch
            {
                PageEventType.OnAppearing => $@"
protected override void OnAppearing()
{{
    SetupViewModelIfNotAlready();
    {(!lines.Any() ? "OnAppearingInternal();" : null)}
    {methodContent}
    base.OnAppearing();
}}

{(!lines.Any() ? "partial void OnAppearingInternal();" : null)}
".Trim(),



                PageEventType.OnBackButtonPressed => $@"
protected override bool OnBackButtonPressed()
{{
    {methodContent}
    return base.OnBackButtonPressed();
}}
".Trim(),



                PageEventType.OnBindingContextChanged => $@"
protected override void OnBindingContextChanged()
{{
    {methodContent}
    base.OnBindingContextChanged();
}}
".Trim(),



                PageEventType.OnChildMeasureInvalidated => $@"
protected virtual void OnChildMeasureInvalidated(object sender, EventArgs e)
{{
    {methodContent}
    base.OnChildMeasureInvalidated(sender, e);
}}
".Trim(),



                PageEventType.OnDisappearing => $@"
protected override void OnDisappearing()
{{
    {methodContent}
    base.OnDisappearing();
}}
".Trim(),



                PageEventType.OnNavigatedFrom => $@"
protected virtual void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
{{
    {methodContent}
    base.OnNavigatedFrom(args);
}}
".Trim(),
                PageEventType.OnNavigatedTo => $@"
protected virtual void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
{{
    {methodContent}
    base.OnNavigatedTo(args);
}}
".Trim(),



                PageEventType.OnNavigatingFrom => $@"
protected virtual void OnNavigatingFrom(Microsoft.Maui.Controls.NavigatingFromEventArgs args)
{{
    {methodContent}
    base.OnNavigatingFrom(args);
}}
".Trim(),



                PageEventType.OnParentSet => $@"
protected override void OnParentSet()
{{
    {methodContent}
    base.OnParentSet();
}}
".Trim(),



                PageEventType.OnSizeAllocated => $@"
protected override void OnSizeAllocated(double width, double height)
{{
    {methodContent}
    base.OnSizeAllocated(width, height);
}}
".Trim(),
                _ => null,
            };
        }

        //private static string PrintEventCall(IEnumerable<PageEventData> _events,
        //                                     PageEventType eventType)
        //{
        //    var methods = _events.Where(e => e.Type == eventType).ToArray();

        //    var lines = methods
        //                    .Select(m => $"{(m.IsAwaitable ? "await" : string.Empty)} {m.FunctionName}();")
        //                    .ToArray();

        //    return string.Join('\n', lines);
        //}

        private static string PrintUsings(IEnumerable<string> usings)
        {
            return string.Join('\n', usings.Select(u => $"using {u};").ToArray());
        }
    }
}
