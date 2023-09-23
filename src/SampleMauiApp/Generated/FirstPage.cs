using Android.Telephony;
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

    //protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    await ViewModel.OnNavigatedToAsync();
    //    OnNavigatedToInternal(args);
    //    base.OnNavigatedTo(args);
    //}

    //protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) { }

}