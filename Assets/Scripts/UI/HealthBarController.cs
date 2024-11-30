using Managers;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;  // Reference to the fill image (red part)
    [SerializeField] private int maxHealth;

    private void Awake() {
        PlayerEventManager.OnHealthUpdate += UpdateHealthBar;
    }

    private void OnDestroy() {
        PlayerEventManager.OnHealthUpdate -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int healthAmount)
    {
        // This should be a value between 0 and 1 representing the percentage of health remaining
        Debug.Log(healthAmount);
        float healthPercent = (float)healthAmount / maxHealth;
        healthFillImage.fillAmount = healthPercent;
    }
}
