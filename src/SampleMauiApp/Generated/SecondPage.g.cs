using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;

public partial class SecondPage {

    private SecondViewModel viewModel = null;

    public SecondViewModel ViewModel => viewModel ??= Shell.Current.Handler.MauiContext.Services.GetService<SecondViewModel>();
   

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = ViewModel;
        await ViewModel.OnNavigatedToAsync(args);
        OnNavigatedToInternal(args);
        base.OnNavigatedTo(args);
    }

    protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) { }

}