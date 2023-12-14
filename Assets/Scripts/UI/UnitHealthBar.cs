using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image barImage;

    private void OnEnable()
    {
        unit.OnHPChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        unit.OnHPChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHP, int maxHP)
    {
        barImage.fillAmount = (float)currentHP / maxHP;
    }
}
