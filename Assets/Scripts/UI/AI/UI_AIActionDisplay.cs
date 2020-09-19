using UnityEngine;

using TMPro;

using Survival2D.Entities.AI;

namespace Survival2D.UI.AI
{
    public class UI_AIActionDisplay : MonoBehaviour
    {
        [SerializeField] private UI_BarDisplay bar_display = null;
        [SerializeField] private TMP_Text name_display = null;


        private void Awake()
        {
#if UNITY_EDITOR
            if (bar_display == null)
            {
                Debug.LogWarning($"{nameof(bar_display)} not found in behaviour {typeof(UI_AIActionDisplay)} in object {gameObject.GetFullName()}");
            }
            if (name_display == null)
            {
                Debug.LogWarning($"{nameof(name_display)} not found in behaviour {typeof(UI_AIActionDisplay)} in object {gameObject.GetFullName()}");
            }
#endif

            bar_display.InitializeBar(0f, 1f);
        }

        public void UpdateActionDisplay(ActionType type , float action_value)
        {
            name_display.text = type.ToString();
            bar_display.UpdateBar(action_value);
        }
    }
}