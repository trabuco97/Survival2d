using System.IO;
using UnityEngine;

namespace ConsoleChat
{
    public class ConsoleFileReader : MonoBehaviour
    {
        private const string DEFAULT_CONSOLEMACRO_PATH = "Data/STARTING_MACROS/";
        [SerializeField] private string file_macro_reader = DEFAULT_CONSOLEMACRO_PATH;
        [SerializeField] private ConsoleBehaviour console_behaviour = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (console_behaviour == null)
            {
                Debug.LogWarning($"{nameof(console_behaviour)} is not assigned to {nameof(ConsoleFileReader)} of {gameObject.GetFullName()}");
            }
#endif
        }

        [ContextMenu("Execute_Macros")]
        public void ExecuteMacros()
        {
            string[] filePaths = Directory.GetFiles($"{Application.dataPath}/{file_macro_reader}", "*.csl");

            StreamReader reader;
            string command = null;
            foreach (var path in filePaths)
            {
                reader = new StreamReader(path);

                while (!reader.EndOfStream)
                {
                    command = reader.ReadLine();
                    if (string.IsNullOrEmpty(command)) continue;

                    command = $"/{command}";
                    var info = console_behaviour.Console.ProcessCommand(command);
#if UNITY_EDITOR
                    if (info.result != Console.CommandProcess.Success)
                    {
                        Debug.LogError($"ERROR/: {path} file contains incorrect commands");
                    }
#endif
                }

                reader.Close();
            }
        }
    }
}