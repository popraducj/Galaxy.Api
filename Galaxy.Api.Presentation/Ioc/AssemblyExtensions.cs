using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Galaxy.Api.Presentation.Ioc
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetTypesForPath(this Assembly assembly, string path)
        {
            return (from t in assembly.GetTypes()
                where t.IsClass &&
                      t.IsNotAbstractNorNested() &&
                      t.IsPathValid(path)
                select t).ToList();
        }
        
        public static List<Type> GetEnumsForPath(this Assembly assembly, string path)
        {
            return (from t in assembly.GetTypes()
                where t.IsEnum &&
                      t.IsNotAbstractNorNested() &&
                      t.IsPathValid(path) &&
                      t.IsPublic
                select t).ToList();
        }
    }
}