using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Pages;

public partial class FirstPage {

    private FirstViewModel viewModel = null;

    public FirstViewModel ViewModel => viewModel ??= Shell.Current.Handler.MauiContext.Services.GetService<FirstViewModel>();
   

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = ViewModel;
        await ViewModel.OnNavigatedToAsync(args);
        OnNavigatedToInternal(args);
        base.OnNavigatedTo(args);
    }

    protected virtual void OnNavigatedToInternal(NavigatedToEventArgs args) { }

}