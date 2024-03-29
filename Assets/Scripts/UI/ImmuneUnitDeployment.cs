using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//later should be change to read player's loadout and generate UnitButton(s) based on those loadouts
public class ImmuneUnitDeployment : MonoBehaviour
{
    [SerializeField] private UnitButton unitButtonPrefab;
    [SerializeField] private GameObject unitButtonsGroup;

    //this list should be change to be from some other manager
    [SerializeField] private List<GameObject> unitsInLoadout;

    private Unit UndeployedUnit;
    private Camera mainCam;

    private void Awake()
    {
        InitializeUnitButtons();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if((GameManager.Instance.CurrentCursorState == CursorState.SelectPlot 
            || GameManager.Instance.CurrentCursorState == CursorState.SelectWound)
            && UndeployedUnit != null)
        {
            UndeployedUnitFollowCursor();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DeployCurrentUnit();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                AbortUnitDeployment();
            }
        }
    }

    private void InitializeUnitButtons()
    {
        foreach (GameObject unit in unitsInLoadout)
        {
            UnitButton unitButton = Instantiate(unitButtonPrefab, unitButtonsGroup.transform);

            unitButton.InitializeUnitButton(unit, this);
        }
    }

    public void InstantiateUnit(GameObject unitToSpawn)
    {
        if (GameManager.Instance.CurrentCursorState == CursorState.SelectPlot) return;
        Unit unit = Instantiate(unitToSpawn).GetComponentInChildren<Unit>();
        
        if(unit is Platelet)
        {
            EnterPlateletDeployment(unit);
            return;
        }

        unit.DisableCollider();
        EnterUnitDeployment(unit);
    }

    private void UndeployedUnitFollowCursor()
    {
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += mainCam.nearClipPlane;
        UndeployedUnit.transform.position = mousePosition;    
    }

    private void DeployCurrentUnit()
    {
        SelectablePlot plot = GameManager.Instance.plotManager.CurrentSelectedPlot;
        if (plot == null || plot.IsOccupied) return;

        Vector2 deployPosition = plot.transform.position;
        UndeployedUnit.transform.position = deployPosition;
        UndeployedUnit.EnableCollider();

        ImmuneCell immuneCell = (ImmuneCell)UndeployedUnit;
        GameManager.Instance.UseStemCells(immuneCell.GetCellData().Cost);

        UndeployedUnit.SetPlot(plot);
        plot.SetUnit(UndeployedUnit);

        ExitUnitDeployment();
    }

    private void AbortUnitDeployment()
    {
        Destroy(UndeployedUnit.gameObject);
        ExitUnitDeployment();
    }
    private void EnterUnitDeployment(Unit unit)
    {
        GameManager.Instance.SetCurrentCursorState(CursorState.SelectPlot);
        UndeployedUnit = unit;
    }

    private void ExitUnitDeployment()
    {
        GameManager.Instance.SetCurrentCursorStateWithDelay(CursorState.Default, 0.5f);
        UndeployedUnit = null;
    }

    private void EnterPlateletDeployment(Unit unit)
    {
        GameManager.Instance.SetCurrentCursorState(CursorState.SelectWound);
        UndeployedUnit = unit;
    }

}
