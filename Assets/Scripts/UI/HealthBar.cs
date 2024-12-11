using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _healthBarUI;

    private void Awake()
    {
        _healthBarUI = GetComponent<Image>();
        _healthBarUI.fillAmount = 1.0f;
    }

    private void OnEnable()
    {
        HealthController.OnUpdateHealth += UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (maxHealth <= 0)
        {
            Debug.Log("MaxHealth is zero!");
            return;
        }

        _healthBarUI.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
    }

    private void OnDisable()
    {
        HealthController.OnUpdateHealth -= UpdateHealthBar;
    }
}
