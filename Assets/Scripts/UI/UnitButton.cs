using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image unitImage;
    private ImmuneCellData unitData;

    public void InitializeUnitButton(GameObject unitToSpawn, ImmuneUnitDeployment handler)
    {
        Button buttonComponent = GetComponent<Button>();

        buttonComponent.onClick.AddListener(() =>
        {
            if(unitData.Cost <= GameManager.Instance.StemCells)
                handler.InstantiateUnit(unitToSpawn);
        });

        unitData = unitToSpawn.GetComponentInChildren<ImmuneCell>().GetCellData();
        costText.text = unitData.Cost.ToString();
        unitImage.sprite = unitData.Image;
    }
}
