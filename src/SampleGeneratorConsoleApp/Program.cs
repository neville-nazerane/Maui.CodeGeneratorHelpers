using Maui.CodeGeneratorHelpers;





await CodeGenerationBuilder.WithNewInstance()
                       .WithMobileProjectName("SampleMauiApp")
                       .WithExecutionLocations("SampleGeneratorConsoleApp")
                       .GenerateAsync();
