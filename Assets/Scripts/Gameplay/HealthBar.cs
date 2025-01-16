using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthImage;
    [SerializeField] private GameObject barObject;

    void OnEnable()
    {
        unit.OnDamageTaken += HandleDamageTaken;

        UpdateBar();
    }

    void OnDisable()
    {
        unit.OnDamageTaken -= HandleDamageTaken;
    }

    private void HandleDamageTaken(int previousAmount, int newAmount)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        float healthProgress = unit.CurrentHealth / unit.MaxHealth;
        healthImage.fillAmount = healthProgress;

        DisplayOrHideBar(healthProgress);
    }

    private void DisplayOrHideBar(float progress)
    {
        barObject.SetActive(progress > 0 && progress < 1);
    }
}
