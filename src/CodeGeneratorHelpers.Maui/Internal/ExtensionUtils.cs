﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maui.CodeGeneratorHelpers.Internal
{
    internal static class ExtensionUtils
    {

        internal static string ToFullRootPath(this string folder, IEnumerable<string> possiblePaths)
        {
            var rootDir = Directory.GetCurrentDirectory()
                         .Split(possiblePaths.ToArray(), StringSplitOptions.None)
                         .First();

            return Path.Combine(rootDir, folder);
        }

        internal static string Combine(this string path1, string path2) => Path.Combine(path1, path2);

        //internal static string StripFileName(this string fullName)
        //{
        //    var info = new FileInfo(fullName);
        //    return info.Name[..^info.Extension.Length];
        //}

        //internal static string TrimPage(this string pageName)
        //{
        //    if (pageName.EndsWith("Page"))
        //        return pageName[..^4];
        //    else return pageName;
        //}

        //internal static string TrimViewModel(this string vmName)
        //{
        //    if (vmName.EndsWith("ViewModel"))
        //        return vmName[..^9];
        //    else return vmName;
        //}

    }
}