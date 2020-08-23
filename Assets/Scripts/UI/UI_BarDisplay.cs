using UnityEngine;
using UnityEngine.UI;

public class UI_BarDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider_behaviour = null;

    private void Awake()
    {
#if UNITY_EDITOR
        if (slider_behaviour == null)
        {
            Debug.LogWarning($"{nameof(slider_behaviour)} not found in behaviour {typeof(UI_BarDisplay)} in object {gameObject.GetFullName()}");
        }
#endif
    }

    public void InitializeBar(float min_value, float max_value)
    {
        slider_behaviour.minValue = min_value;
        slider_behaviour.maxValue = max_value;
    }

    public void InitializeBar(float min_value, float max_value, float current_value)
    {
        InitializeBar(min_value, max_value);
        UpdateBar(current_value);
    }

    public void UpdateBar(float new_value)
    {
        slider_behaviour.value = new_value;
    }

}
