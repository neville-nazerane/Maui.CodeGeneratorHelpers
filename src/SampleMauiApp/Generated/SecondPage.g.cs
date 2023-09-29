using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;

public partial class SecondPage 
{
    
    private SecondViewModel viewModel = null;

    public SecondViewModel ViewModel
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
            viewModel = Shell.Current.Handler.MauiContext.Services.GetService<SecondViewModel>();
            BindingContext = viewModel;
        }
    }


    protected override async void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {
        OnNavigatedToInternal();
        await ViewModel.OnNavigatedToAsync();

        base.OnNavigatedTo(args);
    }

    partial void OnNavigatedToInternal();


    protected override async void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {
        OnNavigatedFromInternal();
        await ViewModel.OnNavigatedFromAsync();

        base.OnNavigatedFrom(args);
    }

    partial void OnNavigatedFromInternal();


    protected override void OnAppearing()
    {
        SetupViewModelIfNotAlready();
        OnAppearingInternal();

        base.OnAppearing();
    }

    partial void OnAppearingInternal();


}