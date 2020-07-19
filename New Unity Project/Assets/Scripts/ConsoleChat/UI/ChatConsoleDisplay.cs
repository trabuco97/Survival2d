using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace ConsoleChat.UI
{
    public class ChatConsoleDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas ui_console_canvas = null;
        [SerializeField] private TMP_InputField input_field = null;

        [SerializeField] private ChatAutoTab auto_tab = null;
        [SerializeField] private ChatBox chat_box = null;

        private ConsoleDeveloperManager console;

        private static ChatConsoleDisplay instance;
        public static ChatConsoleDisplay Instance { get { return instance; } }

        public bool IsToogled { get; private set; }
        public bool IsAutoToogled { get; private set; }

        private void Awake()
        {
            if (ui_console_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_console_canvas)} is not assigned to {typeof(ChatConsoleDisplay)} of {name}");
            }

            if (input_field == null)
            {
                Debug.LogWarning($"{nameof(input_field)} is not assigned to {typeof(ChatConsoleDisplay)} of {name}");
            }

            if (chat_box == null)
            {
                Debug.LogWarning($"{nameof(chat_box)} is not assigned to {typeof(ChatConsoleDisplay)} of {name}");
            }

            instance = this;

            IsToogled = false;
            IsAutoToogled = false;
        }

        private void Start()
        {
            console = ConsoleDeveloperManager.instance;

            ui_console_canvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (IsAutoToogled && input_field.isFocused)
            {
                IsAutoToogled = false;
                auto_tab.gameObject.SetActive(false);
            }
        }

        public void ToogleDisplay()
        {
            IsToogled = !ui_console_canvas.gameObject.activeSelf;

            if (!IsToogled)
            {
                input_field.DeactivateInputField();
                IsAutoToogled = false;
            }

            ui_console_canvas.gameObject.SetActive(IsToogled);

            if (IsToogled)
            {
                input_field.ActivateInputField();
            }
        }

        public void ProcessInput(string input)
        {
            // checks if the message isnt empty
            if (!input.Any(x => char.IsLetter(x)))
            {
                input_field.ActivateInputField();
                return;
            }

            if (console.IsCommandValid(input))
            {
                var info = console.ProcessCommand(input);
                input = info.message;
            }

            chat_box.AddMessage(input);
            input_field.text = string.Empty;

            input_field.ActivateInputField();
        }

        public void ProcessInput()
        {
            ProcessInput(input_field.text);
        }

        public void ToggleAutoTab()
        {
            if (!IsAutoToogled)
            {

                // firs
                // create instance of the tab checker, depending on the command or situation
                // evaluate the input
                // if is correct, then passes an array of string to auto complete
                var command_checker = new CommandNamesGenerator();
                if (command_checker.GetAutoTabNames(input_field.text, out var names, out var word))
                {
                    auto_tab.Inicialize(names, word);
                }
                else
                {
                    bool has_command = false;
                    foreach (var command in console.Commands)
                    {
                        if (command.CheckAutoTabName(input_field.text, out var names_command, out var word_coomand))
                        {
                            if (auto_tab.Inicialize(names_command, word_coomand))
                            {
                                has_command = true;
                                break;
                            }
                        }
                    }

                    if (!has_command) return;

                }


                auto_tab.gameObject.SetActive(true);
                IsAutoToogled = true;
                input_field.DeactivateInputField();


                // set the position of the tab in the input field
                var pos = GetUpperCharPosition(auto_tab.WordSearched);
                pos = ui_console_canvas.transform.InverseTransformPoint(pos);
                auto_tab.transform.localPosition = pos;
            }


            auto_tab.CycleSelected();
        }

        public void AcceptAutoTab()
        {
            IsAutoToogled = false;
            ClearWord(auto_tab.WordSearched);

            input_field.text += auto_tab.GetNameSelected();
            input_field.caretPosition = input_field.text.Length;
            input_field.ActivateInputField();
        }


        private bool ClearWord(string incomplete_word)
        {
            if (incomplete_word == string.Empty) return true;

            input_field.text = input_field.text.Replace(incomplete_word, "");

            //int word_count_index = incomplete_word.Length - 1;
            //int start = input_field.text.Length - 1;

            //for (int i = start; i >= 0; i--)
            //{
            //    if (input_field.text[i] == incomplete_word[word_count_index])
            //    {
            //        word_count_index--;
            //    }
            //    else
            //    {
            //        word_count_index = incomplete_word.Length - 1;
            //        start = i;
            //    }

            //    if (word_count_index == - 1)
            //    {
            //        input_field.text = input_field.text.Remove(start - incomplete_word.Length - 1, incomplete_word.Length);

            //        return true;
            //    }

            //}

            return false;
        }

        private Vector3 GetUpperCharPosition(string word_replaced)
        {
            var text_info = input_field.textComponent.textInfo;
            var pos_int = input_field.text.Length - word_replaced.Length;

            var topleft = text_info.characterInfo[pos_int].topLeft;
            var mesh = input_field.textComponent.transform;
            var world_topleft = mesh.TransformPoint(topleft);

            return world_topleft;
        }

    }
}