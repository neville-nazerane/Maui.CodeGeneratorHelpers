using CodeGeneratorHelpers.Maui.Internal;
using CodeGeneratorHelpers.Maui.Models;
using Maui.CodeGeneratorHelpers.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CodeGeneratorHelpers
{
    public class CodeGenerationBuilder
    {

        string viewModelPath = "ViewModels";
        string pagesPath = "Pages";
        string generatedFolderName = "Generated";

        string viewModelSuffix = "ViewModel";
        string pageSuffix = "Page";

        string mobileAppLocation = null;
        string mobileProjectName = null;

        readonly ICollection<PageEventData> _pageEventDatas = new List<PageEventData>();

        IEnumerable<string> executionLocations = Array.Empty<string>();

        public static CodeGenerationBuilder WithNewInstance() => new();

        /// <summary>
        /// Possible project paths generator could be executed from
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public CodeGenerationBuilder WithExecutionLocations(params string[] locations)
        {
            executionLocations = locations;
            return this;
        }

        public CodeGenerationBuilder AddPageToViewModelEvent(PageEventType type, string functionName, bool isAwaitable = false)
        {
            _pageEventDatas.Add(new(type, functionName, isAwaitable));
            return this;
        }

        public CodeGenerationBuilder WithGeneratedFolderName(string folderName)
        {
            generatedFolderName = folderName;
            return this;
        }


        public CodeGenerationBuilder WithMobileProjectName(string name)
        {
            mobileProjectName = name;
            mobileAppLocation ??= name;
            return this;
        }


        public CodeGenerationBuilder WithViewModelPath(string path)
        {
            viewModelPath = path;
            return this;
        }

        public CodeGenerationBuilder WithPagesPath(string path)
        {
            pagesPath = path;
            return this;
        }

        public CodeGenerationBuilder WithViewModelSuffix(string suffix)
        {
            viewModelSuffix = suffix;
            return this;
        }

        public CodeGenerationBuilder WithPageSuffix(string suffix)
        {
            pageSuffix = suffix;
            return this;
        }

        public CodeGenerationBuilder WithMobileAppLocation(string location)
        {
            mobileAppLocation = location;
            return this;
        }

        public async Task GenerateAsync()
        {

            if (mobileProjectName is null)
                throw new ArgumentNullException(nameof(mobileProjectName), "Specify mobile project using WithMobileProjectName()");
            var locations = new HashSet<string>(executionLocations)
            {
                mobileAppLocation
            };

            //string rootPath = Directory.GetCurrentDirectory().ToFullPath(locations);
            string fullMobilePath = mobileAppLocation.ToFullPath(locations);
            string fullPagePath = fullMobilePath.Combine(pagesPath);
            string fullViewModelPath = fullMobilePath.Combine(viewModelPath);
            string generationPath = fullMobilePath.Combine(generatedFolderName);
            generationPath.RecreateFolder();

            var pageNames = fullPagePath.GetNamesWithEnding($"{pageSuffix}.xaml");
            var viewModelNames = fullViewModelPath.GetNamesWithEnding($"{viewModelSuffix}.cs");

            var injections = CodeUtils.GenerateTransientInjections(
                                                pageNames.Union(viewModelNames));

            var methods = new List<string>();
            methods.Add(CodeUtils.GenerateInjectionMethod(injections));

            var usings = new[]
            {
                $"{mobileProjectName}.{pagesPath}",
                $"{mobileProjectName}.{viewModelPath}",
            };

            string utilCode = CodeUtils.GenerateUtilClass($"{mobileProjectName}.{generatedFolderName}", 
                                                          methods,
                                                          usings);
            string genFilePath = generationPath.Combine("GenerationUtils.g.cs");

            foreach (var pageName in pageNames)
            {
                var viewModelName = viewModelNames.SingleOrDefault(v => v == $"{pageName[..^pageSuffix.Length]}{viewModelSuffix}");
                if (viewModelName is not null)
                {
                    var pageCode = CodeUtils.GeneratePartialPage($"{mobileProjectName}.{pagesPath}",
                                                                 usings,
                                                                 pageName,
                                                                 viewModelName,
                                                                 _pageEventDatas);
                    await File.WriteAllTextAsync(generationPath.Combine($"{pageName}.g.cs"), pageCode);
                }
            }

            await File.WriteAllTextAsync(genFilePath, utilCode);

        }

    }
}
