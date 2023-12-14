using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject healthBar;

    private void OnEnable()
    {
        unit.OnHPChange += UpdateHealthBar;
        healthBar.SetActive(false);        
    }

    private void OnDisable()
    {
        unit.OnHPChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (!healthBar.activeSelf)
        {
            healthBar.SetActive(true);
        }
        barImage.fillAmount = (float)currentHP / maxHP;
    }
}
