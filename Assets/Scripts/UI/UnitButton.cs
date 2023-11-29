using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public void InitializeUnitButton(Unit unitToSpawn, UnitButtonHandler handler)
    {
        Button buttonComponent = GetComponent<Button>();

        buttonComponent.onClick.AddListener(() =>
        {
            handler.DeployUnit(unitToSpawn);
        });
    }
}
