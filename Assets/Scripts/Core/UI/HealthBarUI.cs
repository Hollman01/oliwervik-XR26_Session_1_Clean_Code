using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
