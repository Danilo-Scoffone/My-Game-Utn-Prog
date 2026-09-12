using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] Movement movement;
    void Start()
    {
        movement.OnDamage += UpdateBar;
    }

    public void UpdateBar(float health)
    {
        float MaxHealth = movement.MaxHealth;
        healthBar.fillAmount = health / MaxHealth;
    }
}
