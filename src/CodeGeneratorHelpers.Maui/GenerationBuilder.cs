using Maui.CodeGeneratorHelpers.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CodeGeneratorHelpers
{
    public class GenerationBuilder
    {

        string viewModelPath = "ViewModels";
        string pagesPath = "Pages";
        string generatedFolderName = "Generated";

        string viewModelSuffix = "ViewModel";
        string pageSuffix = "Page";

        string mobileAppLocation = null;
        string mobileProjectName = null;

        IEnumerable<string> executionLocations = Array.Empty<string>();

        public static GenerationBuilder WithNewInstance() => new();

        /// <summary>
        /// Possible project paths generator could be executed from
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public GenerationBuilder WithExecutionLocations(params string[] locations)
        {
            executionLocations = locations;
            return this;
        }

        public GenerationBuilder WithGeneratedFolderName(string folderName)
        {
            generatedFolderName = folderName;
            return this;
        }


        public GenerationBuilder WithMobileProjectName(string name)
        {
            mobileProjectName = name;
            mobileAppLocation ??= name;
            return this;
        }


        public GenerationBuilder WithViewModelPath(string path)
        {
            viewModelPath = path;
            return this;
        }

        public GenerationBuilder WithPagesPath(string path)
        {
            pagesPath = path;
            return this;
        }

        public GenerationBuilder WithViewModelSuffix(string suffix)
        {
            viewModelSuffix = suffix;
            return this;
        }

        public GenerationBuilder WithPageSuffix(string suffix)
        {
            pageSuffix = suffix;
            return this;
        }

        public GenerationBuilder WithMobileAppLocation(string location)
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
            string generationPath = fullViewModelPath.Combine(generatedFolderName);
            generationPath.RecreateFolder();

            var pageNames = fullPagePath.GetNamesWithEnding(".xaml");
            var viewModelNames = fullViewModelPath.GetNamesWithEnding(".cs");

            var injections = CodeUtils.GenerateTransientInjections(
                                                pageNames.Union(viewModelNames));


            string utilCode = CodeUtils.GenerateUtilClass($"{mobileProjectName}.{generatedFolderName}", null);
            string genFilePath = "GenerationUtil.g.cs".Combine(generationPath);
            await File.WriteAllTextAsync(genFilePath, utilCode);

        }

    }
}
