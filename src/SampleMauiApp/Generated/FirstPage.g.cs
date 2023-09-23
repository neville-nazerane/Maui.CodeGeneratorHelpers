using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;

public partial class FirstPage {
    
    private FirstViewModel viewModel = null;

    public FirstViewModel ViewModel
    {
        get
        {
            SetupViewModelIfNotAlready();
            return viewModel;
        }
    }

    private void SetupViewModelIfNotAlready()
    {
        if (viewModel is null)
        {
            viewModel = Shell.Current.Handler.MauiContext.Services.GetService<FirstViewModel>();
            BindingContext = viewModel;
        }
    }

    protected override void OnAppearing()
    {
        SetupViewModelIfNotAlready();
        
        OnAppearingInternal();
        base.OnAppearing();
    }

    partial void OnAppearingInternal();
    
    protected override bool OnBackButtonPressed()
    {
        var res = OnBackButtonPressedInternal();
        return res && base.OnBackButtonPressed();
    }

    protected virtual bool OnBackButtonPressedInternal() => true;

    protected override void OnBindingContextChanged()
    {
        
        OnBindingContextChangedInternal();
        base.OnBindingContextChanged();
    }

    partial void OnBindingContextChangedInternal();

    protected void OnChildMeasureInvalidated(object sender, EventArgs e)
    {
        
        OnChildMeasureInvalidatedInternal(sender, e);
    }

    partial void OnChildMeasureInvalidatedInternal(object sender, EventArgs e);

    protected override void OnDisappearing()
    {
        
        OnDisappearingInternal();
        base.OnDisappearing();
    }

    partial void OnDisappearingInternal();

    protected void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {
        
        OnNavigatedFromInternal(args);
    }

    partial void OnNavigatedFromInternal(Microsoft.Maui.Controls.NavigatedFromEventArgs args);

    protected void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {
        
        OnNavigatedToInternal(args);
    }

    partial void OnNavigatedToInternal(Microsoft.Maui.Controls.NavigatedToEventArgs args);

    protected void OnNavigatingFrom(Microsoft.Maui.Controls.NavigatingFromEventArgs args)
    {
        
        OnNavigatingFromInternal(args);
    }

    partial void OnNavigatingFromInternal(Microsoft.Maui.Controls.NavigatingFromEventArgs args);

    protected override void OnParentSet()
    {
        
        OnParentSetInternal();
        base.OnParentSet();
    }

    partial void OnParentSetInternal();

    protected override void OnSizeAllocated(double width, double height)
    {
        
        OnSizeAllocatedInternal(width, height);
        base.OnSizeAllocated(width, height);
    }

    partial void OnSizeAllocatedInternal(double width, double height);


}