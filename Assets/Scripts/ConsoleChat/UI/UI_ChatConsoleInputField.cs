using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace ConsoleChat.UI
{
    public class UI_ChatConsoleInputField : MonoBehaviour
    {
        [SerializeField] private Canvas ui_chat_canvas = null;
        [SerializeField] private TMP_InputField input_field = null;

        [SerializeField] private UI_TabCompletition auto_tab = null;
        [SerializeField] private UI_ChatQueue chat_box = null;

        private Console console = null;

        public bool IsToogled { get; private set; }
        public bool IsAutoToogled { get; private set; }

        private void Awake()
        {
#if UNITY_EDITOR
            if (ui_chat_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_chat_canvas)} is not assigned to {typeof(UI_ChatConsoleInputField)} of {name}");
            }

            if (input_field == null)
            {
                Debug.LogWarning($"{nameof(input_field)} is not assigned to {typeof(UI_ChatConsoleInputField)} of {name}");
            }

            if (chat_box == null)
            {
                Debug.LogWarning($"{nameof(chat_box)} is not assigned to {typeof(UI_ChatConsoleInputField)} of {name}");
            }
#endif 

            IsToogled = false;
            IsAutoToogled = false;
        }

        private void Start()
        {
            console = ConsoleBehaviour.Instance.Console;

            ui_chat_canvas.gameObject.SetActive(false);
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
            IsToogled = !ui_chat_canvas.gameObject.activeSelf;

            if (!IsToogled)
            {
                input_field.DeactivateInputField();
                IsAutoToogled = false;
            }

            ui_chat_canvas.gameObject.SetActive(IsToogled);

            if (IsToogled)
            {
                input_field.ActivateInputField();
            }
        }

        public void ProcessInput(string input)
        {
            // if the string is empty doesnt do anything
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
                bool result = false;
                string[] tab_completition_names = null;
                string name_to_replace = string.Empty;

                // Generate the tab completition, if it can
                // Checks first if there is a command
                if (console.IsCommandValid(input_field.text))
                {
                    console.ParseCommand(input_field.text, out var command_name, out var args);

                    if (args.Length == 0)
                    {
                        var command_generator = new CommandNamesGenerator(console);
                        result = command_generator.GetTabNames(command_name, out var command_completition_names);
                        tab_completition_names = command_completition_names;
                        name_to_replace = command_name;
                    }
                    else
                    {
                        foreach (var command in console.GetCommandArray())
                        {
                            result = command.TabCompletition.TryGetTabCompletitionNames(command_name, args, out var completition_names, out var arg_to_replace);
                            if (result)
                            {
                                tab_completition_names = completition_names;
                                name_to_replace = arg_to_replace;
                                break;
                            }
                        }
                    }
                }

                // checks if no tab names found to replace
                if (!result) return;


                auto_tab.Inicialize(tab_completition_names, name_to_replace);
                auto_tab.gameObject.SetActive(true);
                IsAutoToogled = true;
                input_field.DeactivateInputField();


                // set the position of the tab in the input field
                var pos = GetUpperCharPosition(auto_tab.WordSearched);
                pos = ui_chat_canvas.transform.InverseTransformPoint(pos);
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