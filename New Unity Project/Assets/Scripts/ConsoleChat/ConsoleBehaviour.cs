using UnityEngine;

namespace ConsoleChat
{

    public class ConsoleBehaviour : MonoBehaviour
    {
        [SerializeField] private string command_prefix = "/";
        [SerializeField] private IConsoleCommand[] command_pattern = null;


        private static ConsoleBehaviour instance = null;

        public static ConsoleBehaviour Instance { get { return instance; } }
        public Console Console { get; private set; } = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }


            Console = new Console(command_prefix, command_pattern);
        }
    }
}