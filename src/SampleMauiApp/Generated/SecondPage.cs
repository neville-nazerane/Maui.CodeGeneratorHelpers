using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;

public partial class SecondPage {

    private SecondViewModel viewModel = null;

    public SecondViewModel ViewModel
    {
        get
        {
            if (viewModel is null)
            {
                viewModel = Shell.Current.Handler.MauiContext.Services.GetService<SecondViewModel>();
                BindingContext = viewModel;
            }
            return viewModel;
        }
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await ViewModel.OnNavigatedToAsync(args);
        OnNavigatedToInternal(args);
        base.OnNavigatedTo(args);
    }

    protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) { }

}