using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

namespace ConsoleChat.UI
{
    public static class AutoTabNamesGeneratorHolder
    {
        private static List<ITabNamesGenerator> autoTabGenerator_container;
        private static bool IsInicialized { get; set; } = false;

        private static void InicializeGenerators()
        {
            if (IsInicialized) return;

            // get all the concrete inplementations of the auto names generators
            var generator_types = Assembly.GetAssembly(typeof(ITabNamesGenerator)).GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ITabNamesGenerator)));

            autoTabGenerator_container = new List<ITabNamesGenerator>();

            foreach (var type in generator_types)
            {
                var generator = Activator.CreateInstance(type) as ITabNamesGenerator;
                autoTabGenerator_container.Add(generator);
            }

            Debug.Log(generator_types.ToArray().Length);
            IsInicialized = true;
        }

        public static bool GetGeneratedNames(string input_text, out string[] generated_names, out string word_searched)
        {
            InicializeGenerators();

            foreach (var generator in autoTabGenerator_container)
            {

            }

            generated_names = null;
            word_searched = string.Empty;
            return false;
        }
    }
}