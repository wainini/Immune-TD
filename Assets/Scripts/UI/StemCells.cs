using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StemCells : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStemCellsChange += UpdateStemCells;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStemCellsChange -= UpdateStemCells;
    }

    private void UpdateStemCells(int amount)
    {
        text.text = "Stem Cells: " + amount;
    }
}
