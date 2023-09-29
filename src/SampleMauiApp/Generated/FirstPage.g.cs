using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;


public partial class FirstPage 
{
    
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


    protected override async void OnNavigatedTo(Microsoft.Maui.Controls.NavigatedToEventArgs args)
    {
        await ViewModel.OnNavigatedToAsync();

        base.OnNavigatedTo(args);
    }


    protected override async void OnNavigatedFrom(Microsoft.Maui.Controls.NavigatedFromEventArgs args)
    {
        await ViewModel.OnNavigatedFromAsync();

        base.OnNavigatedFrom(args);
    }


    protected override void OnAppearing()
    {
        SetupViewModelIfNotAlready();
        OnAppearingInternal();

        base.OnAppearing();
    }

    partial void OnAppearingInternal();


}