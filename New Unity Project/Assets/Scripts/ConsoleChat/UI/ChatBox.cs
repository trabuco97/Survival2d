using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace ConsoleChat.UI
{
    public class ChatBox : MonoBehaviour
    {
        private class MessageInfo
        {
            public int num_lines;
        }

        [SerializeField] private uint max_messages = 1;
        [SerializeField] private TMP_Text chat_display = null;

        private Queue<MessageInfo> message_queue = new Queue<MessageInfo>();

        private static ChatBox instance;
        public static ChatBox Instance { get { return instance; } }

        private void Awake()
        {
            if (chat_display == null)
            {
                Debug.LogWarning($"{nameof(chat_display)} is not assigned to {typeof(ChatBox)} of {name}");
            }

            instance = this;
            chat_display.text = string.Empty;
        }


        public void AddMessage(string message)
        {
            int numLines = message.Split('\n').Length;
            message_queue.Enqueue(new MessageInfo { num_lines = numLines });

            if (message_queue.Count > max_messages)
            {
                var message_info = message_queue.Dequeue();
                chat_display.text = string.Join("\n", chat_display.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).Skip(message_info.num_lines));
            }

            chat_display.text += $"{message}\n";
        }
    }
}