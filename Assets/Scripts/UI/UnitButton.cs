using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public void InitializeUnitButton(Unit unitToSpawn, ImmuneUnitDeployment handler)
    {
        Button buttonComponent = GetComponent<Button>();

        buttonComponent.onClick.AddListener(() =>
        {
            handler.InstantiateUnit(unitToSpawn);
        });
    }
}
