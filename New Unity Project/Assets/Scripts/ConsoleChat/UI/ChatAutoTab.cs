using UnityEngine;
using System.Collections.Generic;

using TMPro;

namespace ConsoleChat.UI
{
    public class ChatAutoTab : MonoBehaviour
    {
        [SerializeField] private GameObject autoTab_name_prefab = null;
        [SerializeField] private Transform name_container_display = null;

        private Queue<TMP_Text> names_container = null;
        private TMP_Text current_selected = null;

        public string WordSearched { get; private set; } = string.Empty;

        public bool Inicialize(string[] names_array, string name_searched)
        {
            if (names_array == null || names_array.Length == 0) return false;

            if (names_container != null) DestroyNames();
            names_container = new Queue<TMP_Text>();


            foreach (var name in names_array)
            {
                GameObject instance = Instantiate(autoTab_name_prefab, name_container_display);
                if (instance.TryGetComponent(out TMP_Text text_display))
                {
                    text_display.text = name;
                    names_container.Enqueue(text_display);
                }
            }

            WordSearched = name_searched;

            return true;
        }

        public void CycleSelected()
        {
            if (current_selected != null)
            {
                current_selected.color = Color.black;
            }

            current_selected = names_container.Dequeue();
            current_selected.color = Color.red;
            names_container.Enqueue(current_selected);
        }

        public string GetNameSelected()
        {
            gameObject.SetActive(false);
            return current_selected.text;
        }

        private void DestroyNames()
        {
            foreach (var text_display in names_container)
            {
                Destroy(text_display.gameObject);
            }
        }


    }
}