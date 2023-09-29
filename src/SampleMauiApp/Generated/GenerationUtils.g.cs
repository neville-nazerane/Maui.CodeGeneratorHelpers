using SampleMauiApp.Pages;
using SampleMauiApp.ViewModels;

namespace SampleMauiApp.Generated;

public static class GenerationUtils
{
    
   public static IServiceCollection AddGeneratedInjections(this IServiceCollection services)
        => services.AddTransient<FirstPage>()
                   .AddTransient<ParamsPage>()
                   .AddTransient<SecondPage>()
                   .AddTransient<FirstViewModel>()
                   .AddTransient<ParamsViewModel>()
                   .AddTransient<SecondViewModel>();

}