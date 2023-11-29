using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//later should be change to read player's loadout and generate UnitButton(s) based on those loadouts
public class UnitButtonHandler : MonoBehaviour
{
    [SerializeField] private UnitButton unitButtonPrefab;
    [SerializeField] private GameObject unitButtonsGroup;

    //this list should be change to be from some other manager
    [SerializeField] private List<Unit> unitsInLoadout;

    private void Awake()
    {
        foreach(Unit unit in unitsInLoadout)
        {
            UnitButton unitButton = Instantiate(unitButtonPrefab, unitButtonsGroup.transform);
            unitButton.InitializeUnitButton(unit, this);
        }
    }

    public void DeployUnit(Unit unitToSpawn)
    {
        GameManager.Instance.SetCurrentCursorState(CursorState.SelectPlot);
        Unit unit = Instantiate(unitToSpawn);
        unit.IsFollowingCursor = true;
    }
}
