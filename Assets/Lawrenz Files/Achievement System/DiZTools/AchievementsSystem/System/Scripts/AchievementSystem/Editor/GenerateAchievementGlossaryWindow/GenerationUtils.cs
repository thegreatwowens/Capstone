using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public static class GenerationUtils
    {
        //Achievements glossary assembly private key
        private const string ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY_PREFIX = "DiZTools_AchievementsSystem_";
        public const string ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY = ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY_PREFIX + "AchievementsDataGlossary";

        private const string PRIVATE_KEY_CLASS_NAME = "PrivateKey";

        #region ASSEMBLIES GENERATION

        public static (AssemblyName, AssemblyBuilder, ModuleBuilder) GenerateAssembly(string assemblyName, string assemblyPath)
        {
            // Get the current application domain for the current thread.
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // Create a dynamic assembly in the current application domain, and allow it to be executed and saved to disk.
            AssemblyName aName = new AssemblyName(assemblyName);
            AssemblyBuilder ab = currentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave, Path.Combine(Application.dataPath, assemblyPath));

            AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);

            // Define a dynamic module in assembly. For a single-module assembly, the module has the same name as the assembly.
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

            return (aName, ab, mb);
        }

        public static void SaveAssembly(AssemblyName aName, AssemblyBuilder ab)
        {
            // Save the assembly.
            ab.Save(aName.Name + ".dll");
        }

        public static void GenerateClass(ModuleBuilder mb, string className, List<string> variableValues)
        {
            // Define a runtime class with specified name and attributes.
            TypeBuilder tb = mb.DefineType(className, TypeAttributes.Public);

            // Add fields to the class, with the specified attribute and type.
            for (int i = 0; i < variableValues.Count; i++)
            {
                tb.DefineField(variableValues[i].Replace(" ", "_"), typeof(String), FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal).SetConstant(variableValues[i]);
            }

            // Create the type.
            tb.CreateType();
        }

        public static void GeneratePrivateKey(ModuleBuilder mb, string privateKey)
        {
            // Add private class for private key
            TypeBuilder tb = mb.DefineType(PRIVATE_KEY_CLASS_NAME, TypeAttributes.NotPublic);

            tb.DefineField(privateKey, typeof(string), FieldAttributes.Assembly).SetConstant(privateKey);

            tb.CreateType();
        }

        #endregion

        #region ASSEMBLIES GETTERS

        public static Assembly GetAssemblyWithPrivateKey(string privateKey)
        {
            //Make an array for the list of assemblies.
            Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assems.Length; i++)
            {
                if (assems[i].GetTypes().Length != 0 && assems[i].GetType(GenerationUtils.PRIVATE_KEY_CLASS_NAME)?.GetField(privateKey, BindingFlags.NonPublic | BindingFlags.Instance) != null)
                {
                    return assems[i];
                }
            }

            return null;
        }

        public static List<Assembly> GetAssembliesWithPrivateKey(string privateKey)
        {
            //Make an array for the list of assemblies.
            Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies();

            List<Assembly> customGlossariesAssemblies = new List<Assembly>();

            for (int i = 0; i < assems.Length; i++)
            {
                if (assems[i].GetTypes().Length != 0 && assems[i].GetType(GenerationUtils.PRIVATE_KEY_CLASS_NAME)?.GetField(privateKey, BindingFlags.NonPublic | BindingFlags.Instance) != null)
                {
                    customGlossariesAssemblies.Add(assems[i]);
                }
            }

            return customGlossariesAssemblies;
        }

        #endregion
    }
}
